using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class HuntPage : UIController
{
    private HuntPageView huntPageView;

    public RectTransform headerTransform;
    public VerticalLayoutGroup layoutGroup;
    public float headerTweenDuration = 0.2f;

    public bool isCheckScrollContentPos = true;
    private bool isHeaderShown = true;

    protected override void Awake()
    {
        base.Awake();
        huntPageView = (HuntPageView)view;
    }

    private void Start()
    {
        huntPageView.SetScrollRectContentDragEvent(OnScrollRectContentDragged);
    }

    private void OnScrollRectContentDragged(PointerEventData eventData, RectTransform rectTransform)
    {
        if (isCheckScrollContentPos)
        {
            if (!isHeaderShown && eventData.delta.y < 0)
            {
                MoveDownHeader(rectTransform);
            }
            else if (isHeaderShown && eventData.delta.y > 0)
            {
                MoveUpHeader(rectTransform);
            }
        }
    }

    private void MoveUpHeader(RectTransform target)
    {
        isCheckScrollContentPos = false;
        huntPageView.scrollRect.StopMovement();
        headerTransform.DOAnchorPosY(headerTransform.sizeDelta.y, headerTweenDuration).OnComplete(() => OnHeaderAnimationFinished(false)).Play();
        DOTween.To(x => layoutGroup.padding.top = Mathf.RoundToInt(x), headerTransform.sizeDelta.y, 0, headerTweenDuration).OnUpdate(RefreshLayout).Play();
        //target.DOAnchorPosY(0, headerTweenDuration).Play();
    }

    private void MoveDownHeader(RectTransform target)
    {
        isCheckScrollContentPos = false;
        huntPageView.scrollRect.StopMovement();
        headerTransform.DOAnchorPosY(0f, headerTweenDuration).OnComplete(() => OnHeaderAnimationFinished(true)).Play();
        DOTween.To(x => layoutGroup.padding.top = Mathf.RoundToInt(x), 0, headerTransform.sizeDelta.y, headerTweenDuration).OnUpdate(RefreshLayout).Play();
        //target.DOAnchorPosY(headerTransform.sizeDelta.y, headerTweenDuration).Play();
    }

    private void RefreshLayout()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)huntPageView.scrollRect.transform);
    }

    private void OnHeaderAnimationFinished(bool isShow)
    {
        isHeaderShown = isShow;
        isCheckScrollContentPos = true;
    }
}
