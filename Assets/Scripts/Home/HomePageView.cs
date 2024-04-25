using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.IO;

[Serializable]
public class DefaultButton
{
    public string name;
    public Sprite sprite;
}


[Serializable]
public class LocationItemList
{
    public LocationItem[] locationList;
}

[Serializable]
public class LocationItem
{
    public string id;
    public string name;
    public MenuItem[] subMenus;
}

[Serializable]
public class ImageUrl
{
    public string id;
    public string url;
}

[Serializable]
public class MenuItemList
{
    public MenuItem[] menuList;
    public ImageUrl[] imageUrlList;
    public string backButtonSpriteUrl;
    public TitleList[] titleList;
}

[Serializable]
public class MenuItem
{
    public string id;
    public string name;
    public string spriteUrl;
    public string functionName;
    public MenuItem[] subMenus;
    public bool backButton;


    public Sprite sprite;
}

[Serializable]
public class TitleList
{
    public string id;
    public string title;
}


[Serializable]
public class Header
{
    public Button backButton; 
    public TMP_Text titleText;
    public Image titleImage;

    public List<DefaultButton> defaultSpriteList;

    private List<MenuItem> menuList;

    public Sprite GetSprite(string spriteName)
    {
        Sprite sprite = defaultSpriteList.Where(x => x.name == spriteName).Select(x => x.sprite).FirstOrDefault();
        return sprite;
    }
}

public class HomePageView : UIScreen
{
    public GameObject homePanelObj;
    public GameObject huntEventObj;
    public SwipeableArea swipeableArea;
    public GalleryPage galleryPage;

    [SerializeField] private TaskPage task;
    [SerializeField] private LeaderboardPage leaderboard;
    [SerializeField] private PhotoStreamPage photostream;
    [SerializeField] private Profile profile;
    [SerializeField] private ScannerPage scanner;
    [SerializeField] private WelcomePage welcomePage;
    [SerializeField] private SignInPage signInPage;
    [SerializeField] private RegisterPage registerPage;
    [SerializeField] private SettingsPage settingsPage;

    [SerializeField] private Toggle taskToggle;
    [SerializeField] private Toggle leaderboardToggle;
    [SerializeField] private Toggle photostreamToggle;
    [SerializeField] private Toggle profileToggle;

    public Header header;

    public MenuItem[] menus;
    public LocationItem[] locations;

    private Dictionary<string, Sprite> itemSpriteDict = new Dictionary<string, Sprite>();
    private Dictionary<string, string> itemActionDict = new Dictionary<string, string>();

    public static HomePageView Instance { get; private set; }

    private void Start()
    {
        Instance = this;
        //huntEventButton.onClick.AddListener(ShowHuntEvent);

    }

    public void RegisterSwipeLeft(Action action)
    {
        swipeableArea.onSlideLeft = action;
    }

    public void RegisterSwipeRight(Action action)
    {
        swipeableArea.onSlideRight = action;
    }

    public void ShowHuntEvent()
    {
        huntEventObj.SetActive(true);
        HideHomePage();
    }

    public void HideHuntEvent()
    {
        huntEventObj.SetActive(false);
    }

    public void ShowHomePage()
    {
        homePanelObj.SetActive(true);
        HideHuntEvent();
    }

    public void HideHomePage()
    {
        homePanelObj.SetActive(false);
    }

    private void LoadLocationJson()
    {
        //API.GetMenuFromLocalJson<LocationItemList>("locations", x => StartCoroutine(OnSuccess(x)));
       // IEnumerator OnSuccess(LocationItemList locationList)
        {
            //locations = locationList.locationList;

            //foreach (LocationItem item in locations)
            //{
            //    if (item.subMenus != null)
            //    {
            //        foreach (MenuItem subItem in item.subMenus)
            //        {
            //            if (!itemActionDict.ContainsKey(subItem.id))
            //            {
            //                itemActionDict.Add(subItem.id, subItem.functionName);
            //                Debug.Log("add function : " + subItem.id + " " + subItem.functionName);
            //            }
            //        }
            //    }
            //}

            //foreach (ImageUrl imageUrl in itemList.imageUrlList)
            //{
            //    yield return Utils.GetSprite(imageUrl.url, Consts.PICTURES_FOLDER_NAME + Consts.IMAGE_NAME, imageUrl.id, OnSuccess);
            //    void OnSuccess(Sprite sprite)
            //    {
            //        Debug.Log("success add : " + imageUrl.id);
            //        itemSpriteDict.Add(imageUrl.id, sprite);
            //    }
            //}

            //foreach (KeyValuePair<string, Toggle> kvp in toggleDict)
            //{
            //    MenuItemUI itemUI = kvp.Value.GetComponent<MenuItemUI>();
            //    itemUI?.Set(itemSpriteDict[kvp.Key]);
            //}
        }
    }

    public void ResetProfile()
    {
        profile.Reset();
    }

    public void SetTaskToggle(Action<bool> action)
    {
        taskToggle.onValueChanged.AddListener(action.Invoke);
    }

    public void SetLeaderboardToggle(Action<bool> action)
    {
        leaderboardToggle.onValueChanged.AddListener(action.Invoke);
    }

    public void SetPhotostreamToggle(Action<bool> action)
    {
        photostreamToggle.onValueChanged.AddListener(action.Invoke);
    }

    public void SetProfileToggle(Action<bool> action)
    {
        profileToggle.onValueChanged.AddListener(action.Invoke);
    }

    public void ShowWelcomePage(bool value)
    {
        if (value)
        {
            welcomePage.Show();
        }
        else
            welcomePage.Hide();
    }

    public void ShowSignInPage(bool value)
    {
        if (value)
        {
            signInPage.Show();
        }
        else
            signInPage.Hide();
    }

    public void ShowRegisterPage(bool value)
    {
        if (value)
        {
            registerPage.Show();
        }
        else
            registerPage.Hide();
    }

    public void ShowSettingsPage(bool value)
    {
        if (value)
        {
            settingsPage.Show();
        }
        else
            settingsPage.Hide();
    }

    public void ShowTask(bool value)
    {
        if (value)
        {
            task.Show();
            PageStateController.GoToPage(Page.PageState.TASK);
            ShowLeaderboard(false);
            ShowPhotostream(false);
            ShowProfile(false);
        }
        else
            task.Hide();
    }

    public void ShowLeaderboard(bool value)
    {
        if (value)
        {
            leaderboard.Show();
            PageStateController.GoToPage(Page.PageState.LEADERBOARD);
            ShowTask(false);
            ShowPhotostream(false);
            ShowProfile(false);
        }
        else
            leaderboard.Hide();
    }

    public void ShowPhotostream(bool value)
    {
        if (value)
        {
            photostream.Show();
            PageStateController.GoToPage(Page.PageState.PHOTOSTREAM);
            ShowTask(false);
            ShowLeaderboard(false);
            ShowProfile(false);
        }
        else
            photostream.Hide();
    }

    public void ShowGallery(string username)
    {
        galleryPage.Show();
        galleryPage.ShowGalleryList(username);
    }

    public void ShowProfile(bool value)
    {
        if (value)
        {
            profile.Show();
            PageStateController.GoToPage(Page.PageState.PROFILE);
            ShowTask(false);
            ShowLeaderboard(false);
            ShowPhotostream(false);
        }
        else
            profile.Hide();
    }

    public void SetHeader(string title, Sprite icon, Action onBackButtonClicked)
    {
        header.backButton.gameObject.SetActive(onBackButtonClicked != null);
        if (onBackButtonClicked != null)
        {
            header.backButton.onClick.RemoveAllListeners();
            header.backButton.onClick.AddListener(onBackButtonClicked.Invoke);
        }
        header.titleText.text = title;
        ((RectTransform)header.titleText.transform).anchoredPosition = new Vector2(0, 150f);
        ((RectTransform)header.titleText.transform).DOAnchorPosY(5.4f, 0.2f).SetEase(Ease.OutQuad).Play();
    }

    public Sprite GetSprite(string id)
    {
        if (itemSpriteDict.TryGetValue(id, out Sprite sprite))
        {
            return sprite;
        }
        return null;
    }

    public string GetFunctionName(string id)
    {
        if (itemActionDict.TryGetValue(id, out string functionName))
        {
            return functionName;
        }
        return null;
    }
}
