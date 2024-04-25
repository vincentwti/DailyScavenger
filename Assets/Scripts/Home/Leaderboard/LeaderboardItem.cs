using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardItem : MonoBehaviour
{
    public TMP_Text rankText;
    public TMP_Text nameText;
    public TMP_Text scoreText;
    public Button button;

    private Leaderboard info;
    private LeaderboardPage controller;
    private CanvasGroup canvasGroup;

    public void Init(LeaderboardPage controller, Leaderboard info, bool clickable)
    {
        this.info = info;
        this.controller = controller;
        rankText.text = info.rank;
        nameText.text = info.name;
        scoreText.text = info.score.ToString();
        if (clickable)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnItemClicked);
        }
        else 
        {
            button.interactable = false;
        }
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = clickable;
    }

    public void OnItemClicked()
    {
        Debug.LogError("Show Gallery");
        HomePage.Instance.ShowGallery("Siti");
    }
}
