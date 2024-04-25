using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPageView : UIScreen
{
    public Button logoutButton;

    public void SetLogoutButtonEvent(Action action)
    {
        logoutButton.onClick.AddListener(action.Invoke);
    }
}
