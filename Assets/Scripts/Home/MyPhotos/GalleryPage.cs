using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class GalleryInfo
{
    public string username;
    public string score;
    public bool isVideo;
    public string url;
    public string caption;
    public string likes;
    public bool isLiked;
}

public class GalleryPage : UIController
{
    public Transform galleryItemParent;
    public GameObject galleryItemPrefab;
    public bool showAll;

    private List<GalleryInfo> galleryInfoList;

    protected override void Awake()
    {
        base.Awake();
        galleryInfoList = new List<GalleryInfo>();
        galleryInfoList.Add(new GalleryInfo { username = "tester", score = "50", url = "https://i.pinimg.com/originals/49/78/4b/49784b23c29e872274c4d60e535bb6e0.jpg", caption = "Take a picture with pizza", likes = "You and 3 others", isLiked = true, isVideo = false });
        galleryInfoList.Add(new GalleryInfo { username = "tester", score = "60", url = "https://awsimages.detik.net.id/community/media/visual/2023/03/31/fotoinet-wefie-ai.jpeg?w=600&q=90", caption = "Take a picture with your buddies", likes = "You and 3 others", isLiked = true, isVideo = false });
        galleryInfoList.Add(new GalleryInfo { username = "tester", score = "50", url = "https://phinemo.com/wp-content/uploads/2017/07/19932248_1267873570007316_2474424143483764736_n.jpg", caption = "Take a picture with National Monument", likes = "You and 3 others", isLiked = true, isVideo = false });
        galleryInfoList.Add(new GalleryInfo { username = "Albert", score = "80", url = "https://kenh14cdn.com/203336854389633024/2022/10/28/photo-5-1666968727779637879492.jpeg", caption = "Take a picture in tiger den", likes = "You and 3 others", isLiked = true, isVideo = false });
        galleryInfoList.Add(new GalleryInfo { username = "Siti", score = "90", url = "https://i.pinimg.com/736x/42/31/16/4231167f5dd98ab0d9b47877931b9768.jpg", caption = "Take a picture in gorilla cage", likes = "You and 3 others", isLiked = true, isVideo = false });
        galleryInfoList.Add(new GalleryInfo { username = "Siti", score = "40", url = "https://www.allkpop.com/upload/2023/05/content/311308/1685552900-untitled-1.jpg", caption = "Take a picture with spiritual being", likes = "You and 3 others", isLiked = true, isVideo = false });
        galleryInfoList.Add(new GalleryInfo { username = "Albert", score = "90", url = "https://upload.wikimedia.org/wikipedia/commons/0/0b/NewJeans_Minji_OLENS_2.jpg", caption = "Take a selfie", likes = "You and 3 others", isLiked = true });
        galleryInfoList.Add(new GalleryInfo { username = "Agus", score = "100", url = "https://image.kpopmap.com/2023/06/NewJeans-MinJi-W-Korea.jpg", caption = "Take a picture of you slap a person", likes = "You and 3 others", isLiked = true, isVideo = false });
        galleryInfoList.Add(new GalleryInfo { username = "Agus", score = "40", url = "https://media.suara.com/pictures/653x366/2022/08/08/69951-danielle-newjeans-twittercompicsnewjeans.jpg", caption = "Take a picture with a crocodile", likes = "You and 3 others", isLiked = true, isVideo = false });
        galleryInfoList.Add(new GalleryInfo { username = "Agus", score = "60", url = "https://www.billboard.com/wp-content/uploads/2023/05/danielle-newjeans-2023-billboard-1548.jpg", caption = "Shoot a person on the head", likes = "You and 3 others", isLiked = true, isVideo = false });
        galleryInfoList.Add(new GalleryInfo { username = "Agus", score = "70", url = "https://netstorage-tuko.akamaized.net/images/38d535bcf1cec251.jpg?imwidth=900", caption = "Take a picture with your mother", likes = "You and 3 others", isLiked = true, isVideo = false });
        galleryInfoList.Add(new GalleryInfo { username = "tester", score = "90", url = "https://cdn.rri.co.id/berita/19/images/1708584759238-W/no0ryklh3jgoe0g.jpeg", caption = "Take a picture in front of Candi Borobudur", likes = "You and 3 others", isLiked = true, isVideo = false });
        galleryInfoList.Add(new GalleryInfo { username = "Siti", score = "50", url = "https://asset-a.grid.id/crop/0x0:0x0/945x630/photo/grid/original/62798_gfriend.jpg", caption = "Take a picture with a kitten", likes = "You and 3 others", isLiked = true, isVideo = false });
        galleryInfoList.Add(new GalleryInfo { username = "Albert", score = "100", url = "https://asset-a.grid.id/crop/0x0:0x0/945x630/photo/2018/05/01/3797748831.jpg", caption = "Take a picture with a kitten", likes = "You and 3 others", isLiked = true, isVideo = false });

    }

    private void Start()
    {
        if (showAll)
            ShowGalleryList();
    }

    public void ShowGalleryList()
    {
        for (int i = 0; i < galleryInfoList.Count; i++)
        {
            SpawnItem(galleryInfoList[i]);
        }
    }

    public void ShowGalleryList(string username)
    {
        for (int i = 0; i < galleryInfoList.Count; i++)
        {
            if (galleryInfoList[i].username == username)
                SpawnItem(galleryInfoList[i]);
        }
    }

    private void SpawnItem(GalleryInfo info)
    {
        GameObject item = Instantiate(galleryItemPrefab, galleryItemParent, false);
        GalleryItem galleryItem = item.GetComponent<GalleryItem>();
        galleryItem.Init(info);
    }

    public List<GalleryInfo> GetGalleryInfoList()
    {
        return galleryInfoList;
    }
}
