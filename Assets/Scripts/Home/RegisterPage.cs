using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Net.Mail;
using System;
using System.Text.RegularExpressions;
using UnityEngine.Networking;

public class RegisterPage : UIController
{
    private const int USERNAME_MIN = 5;
    private const int USERNAME_MAX = 20;
    private const int PASSWORD_MIN = 8;
    private const int PASSWORD_MAX = 30;

    private RegisterPageView registerPageView;

    private string password;

    protected override void Awake()
    {
        base.Awake();
        registerPageView = (RegisterPageView)view;
        registerPageView.OnSignUpButtonClicked(Validate);
        //registerPageView.SetUsernameValueChangeEvent(OnUsernameInputValueChanged);
        //registerPageView.SetPasswordValueChangeEvent(OnPasswordInputValueChanged);
        //registerPageView.SetEmailValueChangeEvent(OnEmailInputValueChanged);

        view.SetBackButtonEvent(() => HomePage.Instance.ShowWelcomePage(true));
    }

    public void OnEmailInputValueChanged(string value)
    {
       
    }

    public void OnUsernameInputValueChanged(string value)
    {
       
    }

    public void OnPasswordInputValueChanged(string value)
    {

    }

    private bool ValidateEmail()
    {
        try
        {
            MailAddress mailAddress = new MailAddress(registerPageView.GetEmailInput());
            return true;
        }
        catch (Exception exception)
        {
            PopupManager.Instance.ShowPopupMessage("err", "Email Validation",
               "Email format is not valid",
               new ButtonInfo { content = "Ok" });
            Debug.LogError("err : " + exception.Message);
        }
        return false;
    }

    private bool ValidateUsername()
    {
        string value = registerPageView.GetUsernameInput();
        if (value.Length < USERNAME_MIN || value.Length > USERNAME_MAX)
        {
            PopupManager.Instance.ShowPopupMessage("err", "Username Validation", 
                $"Username must be contain letters or numbers only and over than {USERNAME_MIN} characters and less than {USERNAME_MAX} characters.",
                new ButtonInfo { content = "Ok" });
            return false;
        }
        return true;
    }

    private bool ValidatePassword()
    {
        //Regex hasNumber = new Regex(@"[0-9]+");
        Regex hasUpperChar = new Regex(@"[A-Z]+");

        string value = registerPageView.GetPasswordInput();
        if (/*hasNumber.IsMatch(value) && */ hasUpperChar.IsMatch(value) && value.Length >= PASSWORD_MIN && value.Length <= PASSWORD_MAX)
        {
            return true;
        }
        PopupManager.Instance.ShowPopupMessage("err", "Password Validation",
                $"Password must be contain letters, numbers, uppercases and over than {PASSWORD_MIN} characters and less than {PASSWORD_MAX} characters.",
                new ButtonInfo { content = "Ok" });
        return false;
    }

    private bool ValidateAgreement()
    {
        bool value = registerPageView.GetAgreeValue();
        if (!value)
        {
            PopupManager.Instance.ShowPopupMessage("err", "Agree to the Terms and Privacy",
                "Please agree to the Terms and Privacy by tapping the checkbox",
                new ButtonInfo { content = "Ok" });
        }
        return value;
    }

    private void Validate()
    {
        if (ValidateAgreement())
        {
            if (ValidateUsername())
            {
                if (ValidatePassword())
                {
                    if (ValidateEmail())
                    {
                        //StartCoroutine(RequestRegister());
                        StartCoroutine(API.RequestRegister(registerPageView.GetUsernameInput(), registerPageView.GetPasswordInput(), registerPageView.GetEmailInput(), OnRegisterSucceed));
                    }
                }
            }
        }
    }

    private IEnumerator RequestRegister()
    {
        string url = "https://dickyri.net/_optitreasure/register_api.php";
        WWWForm form = new WWWForm();
        form.AddField("username", registerPageView.GetUsernameInput());
        form.AddField("password", registerPageView.GetPasswordInput());
        form.AddField("email", registerPageView.GetEmailInput());
        using (UnityWebRequest uwr = UnityWebRequest.Post(url, form))
        {
            yield return uwr.SendWebRequest();
            if (uwr.result == UnityWebRequest.Result.Success)
            {
                OnRegisterSucceed();
            }
            else
            {
                Debug.LogError("err : " + uwr.error);
                PopupManager.Instance.ShowPopupMessage("err", "Unable to Register User", "Username or email may already be taken. Please try another one.", new ButtonInfo { content = "Ok" });
            }
        }
    }

    private void OnRegisterSucceed()
    {
        Hide();
        HomePage.Instance.Show();
    }
}
