using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[Serializable]
public class ActiveTask
{
    public string taskId;
    public string taskName;
    public string actionName;
    public string taskDetail;
    public int score;
    public string detail;
    public bool isCompleted;
}

[Serializable]
public class TaskIcon
{
    public string actionName;
    public string colorString;
    public Sprite sprite;
}

[Serializable]
public class DummyTask
{
    public string task_id;
    public string task_name;
    public string task_description;
    public string coordinates;
}


public class TaskPage : UIController
{
    public List<TaskIcon> taskIconList = new List<TaskIcon>();
    public List<TaskIcon> iconList = new List<TaskIcon>();
    public SelectedTask selectedTaskPage;
    public GameObject taskPrefab;
    public Transform taskParent;


    private TaskPageView taskPageView;

    private List<ActiveTask> taskList = new List<ActiveTask>();
    private List<DummyTask> dummyTaskList = new List<DummyTask>();

    protected override void Awake()
    {
        base.Awake();
        taskPageView = (TaskPageView)view;
        SetIcon();
        SetTaskIcon();
    }

    private void Start()
    {
        LoadTaskList();
    }

    public override void Show()
    {
        base.Show();
        //HomePage.Instance.SetHeader("Task", null, "schedule", "navigation");
    }

    public Sprite GetTaskActionSprite(string actionName)
    {
        TaskIcon result = taskIconList.Where(x => x.actionName == actionName).FirstOrDefault();
        if (result == null)
            return null;
        return result.sprite;
    }

    public Sprite GetIconSprite(string actionName)
    {
        TaskIcon result = iconList.Where(x => x.actionName == actionName).FirstOrDefault();
        if (result == null)
            return null;
        return result.sprite;
    }

    public string GetTaskColor(string actionName)
    {
        TaskIcon result = taskIconList.Where(x => x.actionName == actionName).FirstOrDefault();
        if (result == null)
            return null;
        return result.colorString;
    }

    //private void LoadTaskList()
    //{
    //    RequestServer.Instance.LoadDummyTaskList((result) =>
    //    {
    //        dummyTaskList = result;
    //        Debug.LogWarning("count : " + dummyTaskList.Count);

    //        foreach (DummyTask task in dummyTaskList)
    //        {
    //            string[] coorditates = task.coordinates.Split(',');
    //            Debug.Log("latitude : " + coorditates[0] + " " + "longtitude : " + coorditates[1]);
    //            taskList.Add(new ActiveTask { taskId = task.task_id, taskDetail = task.task_name, taskName = "LOCATION", score = 50, actionName = "GPS", detail = "{ \"latitude\" : " + coorditates[0] + ", \"longtitude\" : " + coorditates[1] + " , \"tolerance\" : 0.004 }" });
    //        }

    //        for (int i = 0; i < taskList.Count; i++)
    //        {
    //            GameObject item = Instantiate(taskPrefab, taskParent, false);
    //            TaskItem taskItem = item.GetComponent<TaskItem>();
    //            taskItem.Init(this, selectedTaskPage, taskList[i]);
    //        }
    //    });
    //}

    private void LoadTaskList()
    {
        taskList.Add(new ActiveTask { taskId = "T001", taskName = "LOCATION", taskDetail = "Go to International Financial Center", score = 50, actionName = "GPS", detail = "{ \"longtitude\" : 106.8206216, \"latitude\" : -6.2117639, \"tolerance\" : 0.001 }" });
        taskList.Add(new ActiveTask { taskId = "T002", taskName = "LOCATION", taskDetail = "Go to Chillax Sudirman", score = 50, actionName = "GPS", detail = "{ \"longtitude\" : 106.8229830880426, \"latitude\" : -6.2107812561485085, \"tolerance\" : 0.00001 }" });
        taskList.Add(new ActiveTask { taskId = "T003", taskName = "LOCATION", taskDetail = "Go to National Monument", score = 80, actionName = "GPS", detail = "{ \"longtitude\" : 106.7981286, \"latitude\" : -6.1721615, \"tolerance\" : 0.001 }" });
        taskList.Add(new ActiveTask { taskId = "T004", taskName = "LOCATION", taskDetail = "Go to Gelora Bung Karno Stadium", score = 90, actionName = "GPS", detail = "{ \"longtitude\" : 106.760573, \"latitude\" : -6.2185619, \"tolerance\" : 0.001 }" });
        taskList.Add(new ActiveTask { taskId = "T005", taskName = "LOCATION", taskDetail = "Go to Kota Tua", score = 80, actionName = "GPS", detail = "{ \"longtitude\" : 106.8126273, \"latitude\" : -6.1349917, \"tolerance\" : 0.001 }" });
        taskList.Add(new ActiveTask { taskId = "T006", taskName = "SCAN QR CODE", taskDetail = "Scan the QR code on Daily Mission's Facebook page", score = 100, actionName = "ScanQR" });
        taskList.Add(new ActiveTask { taskId = "T007", taskName = "QUESTION & ANSWER", taskDetail = "What's the best app? (Answer: daily mission)", score = 100, actionName = "Quiz", detail = "daily mission" });
        taskList.Add(new ActiveTask { taskId = "T008", taskName = "SNAP A PHOTO", taskDetail = "Take a picture of desk calendar", score = 30, actionName = "Snap" });
        taskList.Add(new ActiveTask { taskId = "T009", taskName = "RECORD VIDEO", taskDetail = "Record your moment at city street", score = 30, actionName = "RecordVideo" });
        taskList.Add(new ActiveTask { taskId = "T010", taskName = "FILL THE QUESTIONNAIRE", taskDetail = "Best AR/VR Studio", score = 30, actionName = "Choice", detail = "{ \"options\" : [ \"Optimind\", \"Optimum\", \"Optimal\", \"Optimis\" ], \"answer\" : 0 }" });

        //taskList.Add(new ActiveTask { taskId = "T004", taskName = "Like this app on Facebook", actionName = "FB" });
        //taskList.Add(new ActiveTask { taskId = "T005", taskName = "Answer a quiz", actionName = "Quiz" });

        for (int i = 0; i < taskList.Count; i++)
        {
            GameObject item = Instantiate(taskPrefab, taskParent, false);
            TaskItem taskItem = item.GetComponent<TaskItem>();
            taskItem.Init(this, selectedTaskPage, taskList[i]);
        }
    }

    private IEnumerator RefreshTaskList()
    {
        foreach (Transform child in taskParent)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < taskList.Count; i++)
        {
            if (!taskList[i].isCompleted)
            {
                GameObject item = Instantiate(taskPrefab, taskParent, false);
                TaskItem taskItem = item.GetComponent<TaskItem>();
                taskItem.Init(this, selectedTaskPage, taskList[i]);
            }
        }

        yield return null;
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
    }

    private void SetTaskIcon()
    {
        taskIconList.Add(new TaskIcon { actionName = "Snap", colorString = "#FE5E48", sprite = Resources.Load<Sprite>("Textures/Icons/Camera Icon") });
        taskIconList.Add(new TaskIcon { actionName = "ScanQR", colorString = "#E5B044", sprite = Resources.Load<Sprite>("Textures/Icons/Barcode icon") });
        taskIconList.Add(new TaskIcon { actionName = "RecordVideo", colorString = "#A85BC3", sprite = Resources.Load<Sprite>("Textures/Icons/Record icon") });
        taskIconList.Add(new TaskIcon { actionName = "GPS", colorString = "#63C8D0", sprite = Resources.Load<Sprite>("Textures/Icons/Location icon") });
        taskIconList.Add(new TaskIcon { actionName = "Quiz", colorString = "#53D58D", sprite = Resources.Load<Sprite>("Textures/Icons/Message icon") });
        taskIconList.Add(new TaskIcon { actionName = "Choice", colorString = "#00C09B", sprite = Resources.Load<Sprite>("Textures/Icons/List icon") });
    }

    private void SetIcon()
    {
        iconList.Add(new TaskIcon { actionName = "Snap", colorString = "#FE5E48", sprite = Resources.Load<Sprite>("Textures/Icons/camera") });
        iconList.Add(new TaskIcon { actionName = "ScanQR", colorString = "#E5B044", sprite = Resources.Load<Sprite>("Textures/Icons/qr code") });
        iconList.Add(new TaskIcon { actionName = "RecordVideo", colorString = "#A85BC3", sprite = Resources.Load<Sprite>("Textures/Icons/Record icon") });
        iconList.Add(new TaskIcon { actionName = "GPS", colorString = "#63C8D0", sprite = Resources.Load<Sprite>("Textures/Icons/gps") });
        iconList.Add(new TaskIcon { actionName = "Quiz", colorString = "#53D58D", sprite = Resources.Load<Sprite>("Textures/Icons/Message icon") });
        iconList.Add(new TaskIcon { actionName = "Choice", colorString = "#00C09B", sprite = Resources.Load<Sprite>("Textures/Icons/List icon") });
    }

    public void CompleteTask(ActiveTask task)
    {
        int index = taskList.FindIndex(0, taskList.Count, x => x.taskId == task.taskId);
        taskList[index].isCompleted = true;
        StartCoroutine(RefreshTaskList());
    }
}
