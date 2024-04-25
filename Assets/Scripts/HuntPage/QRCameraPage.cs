using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QRCameraPage : MonoBehaviour
{
    public SelectedTask activeTask;
    public Button closeButton;

    private void Start()
    {
        closeButton.onClick.AddListener(activeTask.CloseQRCamera);
    }
}
