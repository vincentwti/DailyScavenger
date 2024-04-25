using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScreen : MonoBehaviour
{
    [SerializeField] protected GameObject container;
    [SerializeField] private Button backButton;

    public void SetBackButtonEvent(Action action)
    {
        Debug.Log("add back button event");
        if (backButton) backButton.onClick.AddListener(() => { action.Invoke(); Debug.Log("back button clicked"); }) ;

        //if (backButton) backButton.onClick.AddListener(action.Invoke);
    }

    public virtual void Show()
    {
        container.SetActive(true);
    }

    public virtual void Hide()
    {
        container.SetActive(false);
    }
}
