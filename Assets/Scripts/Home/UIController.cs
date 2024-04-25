using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    protected UIScreen view;
    protected bool isInit = false;

    protected virtual void Awake()
    {
        Init();
        Debug.Log("awake : " + transform.name);
    }

    protected virtual void Init()
    {
        if (!isInit)
        {
            view = GetComponent<UIScreen>();
            view.SetBackButtonEvent(Hide);
            Debug.Log("view : " + view.name);
            isInit = true;
        }
    }

    public virtual void Show()
    {
        Debug.Log("show : " + transform.name, this);
        Debug.Log("view : " + view, this);
        Init();
        view.Show();
    }

    public virtual void Hide()
    {
        if (view)
            view.Hide();
    }
}
