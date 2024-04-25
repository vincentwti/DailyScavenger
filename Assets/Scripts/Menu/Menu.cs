using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button huntsButton;
    public Button settingsButton;

    private void Start()
    {
        huntsButton.onClick.AddListener(OpenHuntsEvent);
        settingsButton.onClick.AddListener(OpenSettings);
    }

    private void OpenHuntsEvent()
    {
        HomePage.Instance.Show();
        HomePage.Instance.ShowSettings(false);
        HomePage.Instance.ToggleShowMenu();
    }

    private void OpenSettings()
    {
        HomePage.Instance.ShowSettings(true);
        HomePage.Instance.ToggleShowMenu();
    }
}
