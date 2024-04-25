using DanielLochner.Assets.SimpleScrollSnap;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomePageView : UIScreen
{
    private float width;
    public SimpleScrollSnap scrollSnap;
    public Transform pageParent;
    public List<RectTransform> panels = new List<RectTransform>();

    public Button signInButton;
    public Button signUpButton;

    void Start()
    {
        width = ((RectTransform)transform).rect.size.x;
        foreach (Transform child in pageParent)
        {
            panels.Add((RectTransform)child);
            ((RectTransform)child).sizeDelta = new Vector2(width, ((RectTransform)pageParent).rect.size.y);
        }
        scrollSnap.Size = new Vector2(width, ((RectTransform)pageParent).rect.y);
        //scrollSnap.Setup();
    }

    public void SetSignInEvent(Action action)
    {
        signInButton.onClick.AddListener(action.Invoke);
    }

    public void SetSignUpEvent(Action action)
    {
        signUpButton.onClick.AddListener(action.Invoke);
    }
}
