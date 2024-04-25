﻿using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Nobi.UiRoundedCorners;
//using AppState;

[Serializable]
public class DynamicButton
{
    public Button button;
    public TMP_Text buttonText;
    public Image buttonImage;
    public ImageWithIndependentRoundedCorners roundedCorner;
}

public class PopupMessage : Popup
{  
    public TMP_Text titleText;
    public TMP_Text contentText;
    public DynamicButton button1;
    public DynamicButton button2;
    public AudioClip buttonSfx;
    public LayoutGroup layoutGroup;
    public Image blockerImage;
    public GameObject buttonSeparator;

    public string Id { get; private set; }

    private void Awake()
    {
        playOnActive = true;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        //PopupMessageState.onExitStateEvent += Remove;
    }

    private void OnDisable()
    {
        //PopupMessageState.onExitStateEvent -= Remove;
    }

    public virtual void Show(string id, string title, string content, ButtonInfo buttonInfo)
    {
        Id = id;
        if (titleText) titleText.text = title;
        if (contentText) contentText.text = content;
        button1.button.gameObject.SetActive(true);
        button2.button.gameObject.SetActive(false);
        button1.button.onClick.RemoveAllListeners();

        button1.buttonText.text = buttonInfo?.content;
        button1.roundedCorner.r = new Vector4(0, 0, 40, 40);

        if (buttonInfo == null)
        {
            buttonInfo = PopupManager.Instance.defaultButton1;
        }

        button1.button.onClick.AddListener(() => {
            //StateManager.Instance.BackToPrevState();
            buttonInfo.onButtonClicked?.Invoke();
            //EventManager.Instance.PlaySFX(buttonSfx);
            Remove();
        });
        buttonSeparator.SetActive(false);
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)layoutGroup.transform);
        //StateManager.Instance.SwitchState(StateManager.Instance.popupMessageState);
    }
    
    public virtual void Show(string id, string title, string content, ButtonInfo buttonInfo1, ButtonInfo buttonInfo2)
    {
        Id = id;
        if (titleText) titleText.text = title;
        if (contentText) contentText.text = content;
        button1.button.gameObject.SetActive(true);
        button2.button.gameObject.SetActive(true); 
        button1.button.onClick.RemoveAllListeners();
        button2.button.onClick.RemoveAllListeners();

        button1.buttonText.text = buttonInfo1?.content;
        button2.buttonText.text = buttonInfo2?.content;
        button1.roundedCorner.r = new Vector4(0, 0, 0, 40);
        button2.roundedCorner.r = new Vector4(0, 0, 40, 0);

        if (buttonInfo1 == null)
        {
            buttonInfo1 = PopupManager.Instance.defaultButton1;
        }

        if (buttonInfo2 == null)
        {
            buttonInfo2 = PopupManager.Instance.defaultButton2;
        }

        button1.button.onClick.AddListener(() =>
        {
            //EventManager.Instance.PlaySFX(buttonSfx);
            buttonInfo1.onButtonClicked?.Invoke();
            Remove();
        });
        
        button2.button.onClick.AddListener(() =>
        {
            //StateManager.Instance.BackToPrevState();
            //EventManager.Instance.PlaySFX(buttonSfx);
            buttonInfo2.onButtonClicked?.Invoke();
            Remove();
        });
        buttonSeparator.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)layoutGroup.transform);
        //StateManager.Instance.SwitchState(StateManager.Instance.popupMessageState);
    }

    protected void Remove()
    {
        Debug.LogError("remove");
        FadeOut(OnComplete);
        
        void OnComplete()
        {
            PopupManager.Instance.RemovePopup(Id, this);
        }
    }

    private void FadeOut(Action onComplete)
    {
        PlayAnimationBackward(onComplete);
    }

    //private void Remove(AppState.State state)
    //{
    //    Debug.Log("remove popup : " + state.ToString());
    //    PopupManager.instance.RemovePopup(id, gameObject);
    //    StateManager.Instance.SwitchStateWithoutCallback(state);
    //}
}
