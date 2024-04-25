using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : PopupUI
{
    public enum AnimationType
    {
        NONE,
        SCALE,
        MOTION
    }
    
    public AnimationType openAnimationType;
    public AnimationType closeAnimationType;
    public Vector3 from;
    public Vector3 to;
    public bool useAnimationOnClose;
    public RectTransform panel;
    public Tweening[] tweens;

    private void Awake()
    {
        //playOnActive = true;
    }

    protected virtual void OnEnable()
    {
        if (playOnActive)
        {
            Init();
        }
    }

    //protected override void Init()
    protected void Init()
    {
        if (openAnimationType == AnimationType.MOTION)
        {
            panel.anchoredPosition = from;
            //tween = panel.DOAnchorPos(to, duration);
            PlayPositionTween(panel, from, to, 0.2f);
        }
        else
        {
            panel.localScale = from;
            //tween = panel.DOScale(to, duration);
            PlayScaleTween(panel, from, to, 0.2f);
        }
        //base.Init();
    }

    protected void PlayAnimationBackward(Action onComplete)
    {
        if (useAnimationOnClose)
        {
            for (int i = 0; i < tweens.Length; i++)
            {
                if (i == tweens.Length - 1)
                {
                    tweens[i].PlayBackward(onComplete);
                }
                else
                {
                    tweens[i].PlayBackward(null);
                }
            }

            if (closeAnimationType == AnimationType.MOTION)
            {
                panel.anchoredPosition = to;
                PlayPositionTween(panel, to, from, 0.2f, onComplete);
            }
            else if(closeAnimationType == AnimationType.SCALE)
            {
                panel.localScale = to;
                PlayScaleTween(panel, to, from, 0.2f, onComplete);
            }
        }
    }
}
