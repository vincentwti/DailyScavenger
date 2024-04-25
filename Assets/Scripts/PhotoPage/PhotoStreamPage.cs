using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhotoStreamPage : UIController
{
    private PhotoStreamPageView photoStreamPageView;

    private void Start()
    {
        Reset();
    }

    protected override void Init()
    {
        base.Init();
        photoStreamPageView = (PhotoStreamPageView)view;
        photoStreamPageView.SetShowAllButtonEvent(ShowGallery);
    }

    public void Reset()
    {
        StartCoroutine(LoadPictures());
    }

    private void ShowGallery()
    {
        photoStreamPageView.ShowGallery();
    }

    private IEnumerator LoadPictures()
    {
        GalleryPage galleryPage = photoStreamPageView.allPhotosPage;
        List<GalleryInfo> galleryInfoList = galleryPage.GetGalleryInfoList();
        for (int i = 0; i < galleryInfoList.Count; i++)
        {
            if (i < photoStreamPageView.pictures.Length)
            {
                GalleryInfo info = galleryInfoList[i];
                yield return HomePage.Instance.GetSpriteFromServerAsync(info.url, (sprite) =>
                {
                    photoStreamPageView.SetPicture(i, sprite);
                });
            }
            else
            {
                break;
            }
        }
    }
}
