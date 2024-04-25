using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GalleryItem : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text scoreText;
    public TMP_Text captionText;
    public TMP_Text likesText;
    public Button likeButton;
    public Button commentButton;
    public Button moreButton;

    public Image photoImage;
    public VideoPlayer videoPlayer;

    private GalleryInfo info;

    private void OnEnable()
    {
        
    }

    public void Init(GalleryInfo info)
    {
        this.info = info;
        usernameText.text = info.username;
        scoreText.text = info.score;
        captionText.text = info.caption;
        
        likesText.text = info.likes;

        likeButton.onClick.AddListener(OnLikeButtonClicked);
        commentButton.onClick.AddListener(OnCommentButtonClicked);
        moreButton.onClick.AddListener(OnMoreButtonClicked);

        likeButton.GetComponentInChildren<TMP_Text>().text = info.isLiked ? "Liked" : "Like";
        likeButton.GetComponentInChildren<TMP_Text>().color = likeButton.transform.GetChild(0).GetComponentInChildren<Image>().color = info.isLiked ? Color.white : HomePage.Instance.GetColor("DARK_GREY");
        likeButton.GetComponent<Image>().color = HomePage.Instance.GetColor(info.isLiked ? "SCARLET" : "LIGHT_GREY");
        if (info.isVideo)
        {
            photoImage.gameObject.SetActive(false);
            videoPlayer.gameObject.SetActive(true);
            videoPlayer.url = info.url;
        }
        else
        {
            Debug.Log("url : " + info.url + HomePage.Instance);
            photoImage.gameObject.SetActive(true);
            videoPlayer.gameObject.SetActive(false);
            HomePage.Instance.GetSpriteFromServer(info.url, (sprite) =>
            {
                photoImage.sprite = sprite;
            });
            //StartCoroutine(Utils.GetSpriteFromServer(info.url, (sprite) =>
            //{
            //    photoImage.sprite = sprite;
            //}));
        }
    }

    private void OnLikeButtonClicked()
    {
        
    }

    private void OnCommentButtonClicked()
    {
        
    }

    private void OnMoreButtonClicked()
    {
        
    }
}
