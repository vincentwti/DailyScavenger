using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(Scrollbar))]
public class CustomScrollbar : MonoBehaviour
{
    public bool hideOnIdle = true;

    private CanvasGroup canvasGroup;
    private Tweener tween;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        GetComponent<Scrollbar>().onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(float value)
    {
        if (hideOnIdle)
        {
            Hide();
        }
    }

    private void Hide()
    {
        if (tween != null)
        {
            tween.Pause();
        }
        canvasGroup.alpha = 1f;
        tween = canvasGroup.DOFade(0f, 0.2f).SetDelay(0.2f).Play();
    }
}
