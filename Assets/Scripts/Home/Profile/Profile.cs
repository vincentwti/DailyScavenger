using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Profile : UIController
{
    [SerializeField] private string pageName;
    public ProfileView profileView;

    protected override void Awake()
    {
        base.Awake();
        PageStateController.RegisterPage(Page.PageState.PROFILE, null, null);
    }

    private void Start()
    {
        Reset();
    }

    protected override void Init()
    {
        base.Init();
        profileView = (ProfileView)view;
        profileView.SetShowAllButtonEvent(ShowMyGallery);
        Debug.Log("profile init");
    }

    public void Reset()
    {
        StartCoroutine(LoadPictures());
    }

    public override void Show()
    {
        Debug.Log("Show Profile");
        base.Show();
        //HomePage.Instance.SetHeader(pageName, null, "schedule", "navigation");
        //HomePage.Instance.SetHeader(pageName, null,
        //    new ButtonInfo { buttonSprite = HomePage.Instance.GetHeader().GetSprite(Consts.NOTIF), onButtonClicked = HomePage.Instance.OpenNotification },
        //    new ButtonInfo { buttonSprite = HomePage.Instance.GetHeader().GetSprite(Consts.SEARCH), onButtonClicked = HomePage.Instance.OpenSearch });
    }

    private void ShowMyGallery()
    {
        profileView.ShowMyGallery();
    }

    //Load Local Gallery
    //private void LoadPictures()
    //{
    //    string[] fileNames = Directory.GetFiles(Utils.GetGalleryPath());
    //    int index = 0;
    //    Debug.Log("profileview : " + profileView);
    //    profileView.ClearPictures();
    //    foreach (string fileName in fileNames)
    //    {
    //        byte[] data = File.ReadAllBytes(fileName);
    //        Texture2D texture = new Texture2D(2, 2);
    //        texture.LoadImage(data);
    //        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    //        profileView.SetPicture(index, sprite);
    //        index += 1;
    //        if (index > 3) break;
    //    }
    //}

    private IEnumerator LoadPictures()
    {
        GalleryPage galleryPage = profileView.galleryPage;
        List<GalleryInfo> galleryInfoList = galleryPage.GetGalleryInfoList();
        IEnumerable<GalleryInfo> temp = galleryInfoList.Where(x => x.username == UserData.Instance.user.username);
        galleryInfoList = temp.ToList();
        for (int i = 0; i < galleryInfoList.Count; i++)
        {
            if (i < profileView.pictures.Length)
            {
                GalleryInfo info = galleryInfoList[i];
                yield return HomePage.Instance.GetSpriteFromServerAsync(info.url, (sprite) =>
                {
                    profileView.SetPicture(i, sprite);
                });
            }
            else
            {
                break;
            }
        }
    }
}
