using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomePage : UIController
{
    private WelcomePageView welcomePageView;

    protected override void Awake()
    {
        base.Awake();
        welcomePageView = (WelcomePageView)view;
        welcomePageView.SetSignInEvent(SignIn);
        welcomePageView.SetSignUpEvent(SignUp);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("login"))
        {
            string loginInfo = PlayerPrefs.GetString("login");
            string[] splitted = loginInfo.Split("|");
            string email = splitted[0];
            string password = splitted[1];
            StartCoroutine(API.RequestLogin(email, password, OnLoginSucceed));
        }
    }

    private void OnLoginSucceed()
    {
        Hide();
        HomePage.Instance.Show();
    }

    private void SignIn()
    {
        Hide();
        HomePage.Instance.ShowSignInPage(true);
    }

    private void SignUp()
    {
        Hide();
        HomePage.Instance.ShowRegisterPage(true);
    }
}
