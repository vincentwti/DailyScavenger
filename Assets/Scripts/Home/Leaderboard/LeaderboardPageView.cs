using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardPageView : UIScreen
{
    public Button showAllButton;
    public LeaderboardPage allLeaderboardPage;

    public void SetShowAllButtonEvent(Action action)
    {
        if (showAllButton)
            showAllButton.onClick.AddListener(action.Invoke);
    }

    public void ShowAllLeaderboard()
    {
        allLeaderboardPage.Show();
    }
}
