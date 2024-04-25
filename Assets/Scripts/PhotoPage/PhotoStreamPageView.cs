using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoStreamPageView : UIScreen
{
    public Image[] pictures;
    public Button showAllButton;
    public GalleryPage allPhotosPage;

    public void SetShowAllButtonEvent(Action action)
    {
        showAllButton.onClick.AddListener(action.Invoke);
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

    public void ShowGallery()
    {
        allPhotosPage.Show();
    }
}
