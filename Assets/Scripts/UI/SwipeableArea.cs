using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeableArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float menuSlideTolerance = 5f;

    private Vector2 firstTouchPos;

    public Action onSlideLeft;
    public Action onSlideRight;

    private void SlideLeft()
    {
        Debug.Log("move left");
        onSlideLeft?.Invoke();
    }

    private void SlideRight()
    {
        Debug.Log("move right");
        onSlideRight?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("release : " + eventData.position);

        if (eventData.position.x > firstTouchPos.x + menuSlideTolerance)
        {
            SlideRight();
        }
        else if (eventData.position.x < firstTouchPos.x - menuSlideTolerance)
        {
            SlideLeft();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("touch : " + eventData.position);
        firstTouchPos = eventData.position;
    }
}
