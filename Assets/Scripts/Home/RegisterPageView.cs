using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class RegisterPageView : UIScreen
{
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private Toggle agreeTncToggle;
    [SerializeField] private Button signUpButton;

    public void SetUsernameValueChangeEvent(Action<string> action)
    {
        usernameInputField.onValueChanged.AddListener(action.Invoke);
    }

    public void SetPasswordValueChangeEvent(Action<string> action)
    {
        passwordInputField.onValueChanged.AddListener(action.Invoke);
    }

    public void SetEmailValueChangeEvent(Action<string> action)
    {
        emailInputField.onValueChanged.AddListener(action.Invoke);
    }

    public void SetOnAgreeTnCToggle(Action<bool> action)
    {
        agreeTncToggle.onValueChanged.AddListener(action.Invoke);
    }

    public void OnSignUpButtonClicked(Action action)
    {
        signUpButton.onClick.AddListener(action.Invoke);
    }

    public void UpdateUserNameInputText(string value)
    {
        usernameInputField.text = value;
    }

    public string GetUsernameInput()
    {
        return usernameInputField.text;
    }

    public string GetPasswordInput()
    {
        return passwordInputField.text;
    }

    public string GetEmailInput()
    {
        return emailInputField.text;
    }

    public bool GetAgreeValue()
    {
        return agreeTncToggle.isOn;
    }
}
