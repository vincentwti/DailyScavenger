using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPage : UIController
{
    public SelectedTask activeTaskPage;

    protected override void Init()
    {
        base.Init();
        view.SetBackButtonEvent(Back);
    }

    private void Back()
    {
        activeTaskPage.Hide();
        HomePage.Instance.ResetProfile();
    }
}
