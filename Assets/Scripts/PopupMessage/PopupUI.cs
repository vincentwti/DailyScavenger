using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PopupUI : MonoBehaviour
{
    public bool playOnActive = true;

    protected void PlayScaleTween(Transform target, Vector3 from, Vector3 to, float duration, Action onComplete = null)
    {
        target.localScale = from;
        target.DOScale(to, duration).SetEase(Ease.OutQuad).OnComplete(() => onComplete?.Invoke()).Play();
    }

    protected void PlayPositionTween(Transform target, Vector3 from, Vector3 to, float duration, Action onComplete = null)
    {
        ((RectTransform)target).anchoredPosition = from;
        ((RectTransform)target).DOAnchorPos(to, duration).OnComplete(() => onComplete?.Invoke()).SetEase(Ease.OutQuad).Play();
    }

}
