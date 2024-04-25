using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Optimind
{
    public class DeviceCamera : MonoBehaviour
    {
        public QRCodeDecodeController qrDecodeController;

        public static DeviceCamera Instance { get; private set; }

        private NativeCamera.Permission permission;

        private void Start()
        {
            Instance = this;

            permission = NativeCamera.CheckPermission(true);
            if (NativeCamera.DeviceHasCamera())
            {
                if (permission != NativeCamera.Permission.Granted)
                {
                    NativeCamera.RequestPermissionAsync((permission) => { this.permission = permission; }, true);
                }
            }


        }

        public void ShowQRCamera(Action<string> onQRScanFinished)
        {
            qrDecodeController.onQRScanFinished.RemoveAllListeners();
            qrDecodeController.onQRScanFinished.AddListener(onQRScanFinished.Invoke);
            qrDecodeController.Reset();
            qrDecodeController.StartWork();
        }

        public void StopQRCamera()
        {
            qrDecodeController.StopWork();
        }

        public void ShowCamera(Action<string> onComplete, Action onFailed)
        {
            if (permission == NativeCamera.Permission.Granted)
            {
                Debug.Log("open camera");
                NativeCamera.TakePicture(OnCameraCaptured, 1, true, NativeCamera.PreferredCamera.Rear);
            }
            else
            {
                Debug.LogError("Failed to open camera");
                onFailed?.Invoke();
            }

            void OnCameraCaptured(string result)
            {
                Debug.Log("camera captured : " + result);
                onComplete?.Invoke(result);
            }
        }



        public void RecordVideo(Action<string> onComplete, Action onFailed)
        {
            if (permission == NativeCamera.Permission.Granted)
            {
                Debug.Log("open camera");
                NativeCamera.RecordVideo(OnCameraRecorded, NativeCamera.Quality.Default, 60, 0, NativeCamera.PreferredCamera.Rear);
            }
            else
            {
                Debug.LogError("Failed to open camera");
                onFailed?.Invoke();
            }

            void OnCameraRecorded(string result)
            {
                onComplete?.Invoke(result);
            }
        }
    }
}
