using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileView : UIScreen
{
    public TMP_Text scoreText;
    public Image[] pictures;
    public Button showAllButton;
    public GalleryPage galleryPage;

    public void SetShowAllButtonEvent(Action action)
    {
        showAllButton.onClick.AddListener(action.Invoke);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    private void Update()
    {
        UpdateScore(UserData.Instance.Score);
    }

    public void SetPicture(int index, Sprite sprite)
    {
        Debug.Log("index : " + index);
        pictures[index].sprite = sprite;
        pictures[index].gameObject.SetActive(true);
    }

    public void ClearPictures()
    {
        Debug.Log("Clear picture");
        foreach (Image image in pictures)
        {
            image.gameObject.SetActive(false);
        }
    }

    public void ShowMyGallery()
    {
        galleryPage.Show();
        galleryPage.ShowGalleryList(UserData.Instance.user.username);
    }
}
