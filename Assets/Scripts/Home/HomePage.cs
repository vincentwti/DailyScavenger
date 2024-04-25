using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using System.Linq;

[Serializable]
public class IdSpritePair
{
    public string id;
    public Sprite sprite;
    public Color color;
}

[Serializable]
public class IdColorPair
{
    public string id;
    public Color color;
}

public class HomePage : UIController
{
    private HomePageView homePageView;
    private RectTransform homeRectTransform;

    public List<IdSpritePair> idSpritePairList;
    public List<IdColorPair> idColorPairList;

    public EventController eventController;

    public bool IsShowMenu { get; private set; }
    public static HomePage Instance { get; private set; }

    protected override void Awake()
    {
        Instance = this;
        view = GetComponent<UIScreen>();
        homePageView = (HomePageView)view;
        
        view.SetBackButtonEvent(homePageView.ShowHomePage);

        homePageView.SetTaskToggle(ShowTask);
        homePageView.SetLeaderboardToggle(ShowLeaderboard);
        homePageView.SetPhotostreamToggle(ShowPhotostream);
        homePageView.SetProfileToggle(ShowProfile);

        homeRectTransform = (RectTransform)transform;

        homePageView.RegisterSwipeLeft(SlideLeft);
        homePageView.RegisterSwipeRight(SlideRight);

        homePageView.ShowHomePage();
        eventController.LoadEventList(this);
        //homePageView.SetMap2dToggle(Show2DMap);
        //homePageView.SetLibraryToggle(ShowLibrary);
        //homePageView.SetScannerToggle(ShowScanCamera);
        //homePageView.SetProfileToggle(ShowProfile);
    }

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        //LoadingManager.Instance.HideBottomLoading();
    }

    protected override void Init()
    {

    }

    //public void DoAction(string functionName)
    //{
    //    Invoke(functionName, 0f);
    //}

    private void Update()
    {
        
    }

    private void SlideLeft()
    {
        Debug.Log("move left");
        homeRectTransform.DOAnchorPosX(0, 0.2f).Play();
        IsShowMenu = false;
    }

    private void SlideRight()
    {
        Debug.Log("move right");
        homeRectTransform.DOAnchorPosX(500, 0.2f).Play();
        IsShowMenu = true;
    }

    public void ToggleShowMenu()
    {
        if (IsShowMenu)
        {
            SlideLeft();
        }
        else
        {
            SlideRight();
        }
    }

    public void ResetProfile()
    {
        homePageView.ResetProfile();
    }

    public void ShowWelcomePage(bool value)
    {
        homePageView.ShowWelcomePage(value);
    }

    public void ShowSignInPage(bool value)
    {
        homePageView.ShowSignInPage(value);
    }

    public void ShowRegisterPage(bool value)
    {
        homePageView.ShowRegisterPage(value);
    }

    public void ShowTask(bool value)
    {
        homePageView.ShowTask(value);
    }

    public void ShowLeaderboard(bool value)
    {
        homePageView.ShowLeaderboard(value);
    }

    public void ShowPhotostream(bool value)
    {
        homePageView.ShowPhotostream(value);
    }

    public void ShowProfile(bool value)
    {
        homePageView.ShowProfile(value);
    }

    public void ShowGallery(string username)
    {
        homePageView.ShowGallery(username);
    }

    public void ShowSettings(bool value)
    {
        homePageView.ShowSettingsPage(value);
    }
    public void ShowHuntEvent()
    {
        homePageView.ShowHuntEvent();
    }

    //public void SetHeader(string title, Action onBackButtonClicked, ButtonInfo buttonInfo1, ButtonInfo buttonInfo2)
    //{
    //    homePageView.SetHeader(title, onBackButtonClicked, buttonInfo1, buttonInfo2);
    //}

    public void SetHeader(string title, Sprite icon, Action onBackButtonClicked)
    {
       // homePageView.SetHeader(title, onBackButtonClicked);
    }

    public Header GetHeader()
    {
        return homePageView.header;
    }

    public void OpenNotification()
    {
        
    }

    public void OpenSearch()
    {
        
    }


    /// <summary>
    /// Using HomePage thread to start coroutines
    /// </summary>
    /// <param name="url"></param>
    /// <param name="onCompleted"></param>
    public void GetSpriteFromServer(string url, Action<Sprite> onCompleted)
    {
        StartCoroutine(Utils.GetSpriteFromServer(url, (sprite) =>
        {
            onCompleted?.Invoke(sprite);
        }));
    }

    public IEnumerator GetSpriteFromServerAsync(string url, Action<Sprite> onCompleted)
    {
        yield return Utils.GetSpriteFromServer(url, (sprite) =>
        {
            onCompleted?.Invoke(sprite);
        });
    }

    public Sprite GetSpriteIcon(string id)
    {
        IdSpritePair result = idSpritePairList.Where(x => x.id == id).FirstOrDefault();
        if (result != null)
        {
            return result.sprite;
        }
        return null;
    }

    public bool TryGetSpriteIcon(string id, out Sprite sprite, out Color color)
    {
        sprite = null;
        color = Color.white;
        IdSpritePair result = idSpritePairList.Where(x => x.id == id).FirstOrDefault();
        if (result != null)
        {
            sprite = result.sprite;
            color = result.color;
            return true;
        }
        return false;
    }

    public Color GetColor(string id)
    {
        IdColorPair result = idColorPairList.Where(x => x.id == id).FirstOrDefault();
        if (result != null)
        {
            return result.color;
        }
        return Color.white;
    }
}
