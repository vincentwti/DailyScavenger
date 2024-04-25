using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPage : UIController
{
    private SettingsPageView settingsPageView;

    protected override void Init()
    {
        base.Init();
        settingsPageView = (SettingsPageView)view;
        settingsPageView.SetLogoutButtonEvent(Logout);
    }

    public void Logout()
    {
        StartCoroutine(API.RequestLogout(OnLogoutSucceed));
    }

    private void OnLogoutSucceed()
    {
        PlayerPrefs.DeleteKey("login");
        HomePage.Instance.ShowWelcomePage(true);
        Hide();
    }
}
