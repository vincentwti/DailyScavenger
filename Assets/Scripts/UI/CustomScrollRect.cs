using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(ScrollRect))]
public class CustomScrollRect : MonoBehaviour, IDragHandler
{
    private RectTransform targetRectTransform;
    private ScrollRect scrollRect;

    public Vector2 defaultPosition;
    public Action<PointerEventData, RectTransform> onContentDragged;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void OnEnable()
    {
        scrollRect.content.anchoredPosition = defaultPosition; 
    }

    private void Start()
    {
        ScrollRect scrollRect = GetComponent<ScrollRect>();
        targetRectTransform = scrollRect.content;
    }

    public void OnDrag(PointerEventData eventData)
    {
        onContentDragged?.Invoke(eventData, targetRectTransform);
    }
}
