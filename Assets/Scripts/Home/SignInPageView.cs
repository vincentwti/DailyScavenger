using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SignInPageView : UIScreen
{
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private Button forgotPasswordButton;
    [SerializeField] private Button signInButton;

    public void SetForgotPasswordButtonEvent(Action action)
    {
        forgotPasswordButton.onClick.AddListener(action.Invoke);
    }

    public void SetSignInButtonEvent(Action action)
    {
        signInButton.onClick.AddListener(action.Invoke);
    }

    public string GetEmailInput()
    {
        return emailInputField.text;
    }

    public string GetPasswordInput()
    {
        return passwordInputField.text;
    }
}
