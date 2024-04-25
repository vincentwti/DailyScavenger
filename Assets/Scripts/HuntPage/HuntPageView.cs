using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HuntPageView : UIScreen
{
    public ScrollRect scrollRect;
    private CustomScrollRect customScrollRect;

    private void Awake()
    {
        customScrollRect = scrollRect.GetComponent<CustomScrollRect>();
    }

    public void SetScrollRectContentDragEvent(Action<PointerEventData, RectTransform> action)
    {
        customScrollRect.onContentDragged += action;
    }
}
