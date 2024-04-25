using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using System;
using System.IO;

[SerializeField]
public class TargetLocation
{
    public float latitude;
    public float longtitude;
    public float tolerance = 0.001f;
}

public class SelectedTask : UIController
{
    private SelectedTaskView selectedTaskPageView;
    private const float DEGREETOMETERS = 111; //in km

    private ActiveTask task;
    private TaskPage taskPage;
    public GameObject cameraPanelObj;
    public GameObject qrCameraPanelObj;
    public GameObject quizPanelObj;
    public GameObject multipleChoicePanelObj;
    public Button retakePhotoButton;
    public Button keepPhotoButton;

    public GameObject successTaskPage;

    private byte[] cachedData;
    private Texture2D cachedImageTexture;

    protected override void Awake()
    {
        base.Awake();
        selectedTaskPageView = (SelectedTaskView)view;
        if (!Input.location.isEnabledByUser)
            Debug.Log("Location not enabled on device or app does not have permission to access location");
        PermissionCallbacks pc = new PermissionCallbacks();
        pc.PermissionDenied += (s) =>
        {

        };
        pc.PermissionGranted += (s) =>
        {
            Input.location.Start();
        };

        Permission.RequestUserPermission(Permission.FineLocation, pc);
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Input.location.Start();
        }

        if (!Directory.Exists(Utils.GetGalleryPath()))
        {
            Directory.CreateDirectory(Utils.GetGalleryPath());
        }

        retakePhotoButton.onClick.AddListener(OpenCamera);
        keepPhotoButton.onClick.AddListener(SavePhoto);
        selectedTaskPageView.SetCheckTaskButton(CheckTask);
    }

    public void ShowTask(TaskPage taskPage, ActiveTask task)
    {
        this.taskPage = taskPage;
        this.task = task;
        selectedTaskPageView.Init(taskPage, task);
        view.Show();
        if (task.actionName != "Snap" && task.actionName != "RecordVideo")
        {
            cameraPanelObj.gameObject.SetActive(false);
            Debug.Log("deactive");
        }
        quizPanelObj.SetActive(task.actionName == "Quiz");
        multipleChoicePanelObj.SetActive(task.actionName == "Choice");
        if (task.actionName == "Choice")
        {
            MultipleChoice choices = JsonUtility.FromJson<MultipleChoice>(task.detail);
            multipleChoicePanelObj.GetComponent<MultipleChoiceController>().Init(choices.options, choices.answer);
        }
    }

    public void CheckTask()
    {
        Debug.LogError("task action name : " + task.actionName);
        if (task.actionName == "GPS")
        {
            TargetLocation location = JsonUtility.FromJson<TargetLocation>(task.detail);
            StartCoroutine(CheckLocation(location));
        }
        else if (task.actionName == "Snap")
        {
            OpenCamera();
            //StartCoroutine(CaptureScreen());
        }
        else if (task.actionName == "RecordVideo")
        {
            RecordVideo();
        }
        else if (task.actionName == "ScanQR")
        {
            OpenQRCamera();
        }
        else if (task.actionName == "Quiz")
        {
            if (quizPanelObj.GetComponent<QuizController>().CheckAnswer(task.detail))
            {
                Success();
            }
            else
            {
                ToastMessage.Instance.ShowMessage("Wrong answer");
            }
        }
        else if (task.actionName == "Choice")
        {
            if (multipleChoicePanelObj.GetComponent<MultipleChoiceController>().CheckAnswer())
            {
                Success();
            }
            else
            {
                ToastMessage.Instance.ShowMessage("Wrong answer");
            }
        }
    }

    //private IEnumerator CaptureScreen()
    //{
    //    yield return new WaitForEndOfFrame();
    //    //string timeFormat = DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
    //    //ScreenCapture.CaptureScreenshot(timeFormat + ".png");
    //    //Debug.Log("screen captured");
    //    Texture2D texture2d = ScreenCapture.CaptureScreenshotAsTexture();
    //    cameraImage.texture = texture2d;
    //}

    public void OpenCamera()
    {
        //Debug.Log("open camera");
        //cameraImage.gameObject.SetActive(true);
        //WebCamDevice[] camDevices = WebCamTexture.devices;
        //foreach (WebCamDevice camDevice in camDevices)
        //{
        //    Debug.Log("cam found : " + camDevice.name);
        //    if (!camDevice.isFrontFacing)
        //    {
        //        WebCamTexture texture = new WebCamTexture(camDevice.name);
        //        cameraImage.texture = texture;
        //        texture.Play();
        //    }
        //}

        Optimind.DeviceCamera.Instance.ShowCamera((path) => 
        {
            cameraPanelObj.SetActive(false);
            cameraPanelObj.GetComponentInChildren<RawImage>().texture = null;
            if (string.IsNullOrEmpty(path))
            {
                //canceled by user
            }
            else
            {
                byte[] bytes = File.ReadAllBytes(path);
                cachedData = bytes;
                cachedImageTexture = new Texture2D(2, 2);
                cachedImageTexture.LoadImage(bytes);
                cameraPanelObj.SetActive(true);
                cameraPanelObj.GetComponentInChildren<RawImage>().texture = cachedImageTexture;
            }
        }, () => ToastMessage.Instance.ShowMessage("Unable to open camera"));
    }

    public void OpenQRCamera()
    {
        qrCameraPanelObj.SetActive(true);
        Optimind.DeviceCamera.Instance.ShowQRCamera((dataText) => 
        {
            Success();
            CloseQRCamera();
            Debug.Log("scan finished with data : " + dataText);
        });
    }

    public void CloseQRCamera()
    {
        qrCameraPanelObj.SetActive(false);
        cameraPanelObj.GetComponentInChildren<RawImage>().texture = null;
        Optimind.DeviceCamera.Instance.StopQRCamera();
    }

    public void OpenQuiz()
    {
        quizPanelObj.SetActive(true);
    }

    public void OpenMultipleChoice()
    {
        multipleChoicePanelObj.SetActive(true);
    }

    public void CloseQuiz()
    {
        quizPanelObj.SetActive(false);
    }

    private void SavePhoto()
    {
        cameraPanelObj.SetActive(false);
        cameraPanelObj.GetComponentInChildren<RawImage>().texture = null;
        Success();

        string fileName = DateTime.Now.ToString(Consts.PHOTO_FILE_NAME_FORMAT) + ".jpg";

        File.WriteAllBytes(Utils.GetGalleryPath() + fileName, cachedData);
        Destroy(cachedImageTexture);
        cachedImageTexture = null;
    }

    public void RecordVideo()
    {
        Optimind.DeviceCamera.Instance.RecordVideo((path) => {
            Success();
        }, () => ToastMessage.Instance.ShowMessage("Unable to open camera"));
    }


    private void SaveVideoThumbnail()
    {
        
    }

    public IEnumerator CheckLocation(TargetLocation location)
    {
        int maxWait = 20;
        
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        Debug.Log("wait time remaining : " + maxWait);

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device location");
            ToastMessage.Instance.ShowMessage("Failed to get your location");
            yield break;
        }
        else
        {

            Vector2 targetPos = new Vector2(location.longtitude, location.latitude);
            Vector2 currentPos = new Vector2(Input.location.lastData.longitude, Input.location.lastData.latitude);

            Debug.Log("current location : " + currentPos + ",  target location : " + targetPos);
            Debug.LogWarning("distance : " + Vector2.Distance(currentPos, targetPos));

            if (Vector2.Distance(currentPos, targetPos) <= location.tolerance)
            {
                Success();
            }
            else
            {
                ToastMessage.Instance.ShowMessage("You are too far away");
            }

            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            //Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            
        }

        // Stops the location service if there is no need to query location updates continuously.
        Input.location.Stop();
    }

    private void Success()
    {
        UserData.Instance.AddScore(task.score);
        successTaskPage.SetActive(true);
        taskPage.CompleteTask(task);
    }
}
