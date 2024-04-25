using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Leaderboard
{
    public string rank;
    public string name;
    public int score;
}

public class LeaderboardPage : UIController
{
    public int showLimit = 5;
    public bool clickable = false;
    public Transform leaderboardParent;
    public GameObject leaderboardItemPrefab;

    private List<GameObject> leaderboardItemList = new List<GameObject>();
    private List<Leaderboard> leaderboardList;

    private LeaderboardPageView leaderboardPageView;

    private void Start()
    {
        leaderboardList = new List<Leaderboard>();
        leaderboardList.Add(new Leaderboard { rank = "1", name = GetRandomName(), score = 2000 });
        leaderboardList.Add(new Leaderboard { rank = "2", name = GetRandomName(), score = 1800 });
        leaderboardList.Add(new Leaderboard { rank = "3", name = GetRandomName(), score = 1700 });
        leaderboardList.Add(new Leaderboard { rank = "4", name = GetRandomName(), score = 1500 });
        leaderboardList.Add(new Leaderboard { rank = "5", name = GetRandomName(), score = 1250 });
        leaderboardList.Add(new Leaderboard { rank = "6", name = GetRandomName(), score = 1200 });
        leaderboardList.Add(new Leaderboard { rank = "7", name = GetRandomName(), score = 1100 });
        leaderboardList.Add(new Leaderboard { rank = "8", name = GetRandomName(), score = 1050 });
        leaderboardList.Add(new Leaderboard { rank = "9", name = GetRandomName(), score = 1000 });
        leaderboardList.Add(new Leaderboard { rank = "10", name = GetRandomName(), score = 900 });
        leaderboardList.Add(new Leaderboard { rank = "11", name = GetRandomName(), score = 880 });
        leaderboardList.Add(new Leaderboard { rank = "12", name = GetRandomName(), score = 720 });
        leaderboardList.Add(new Leaderboard { rank = "13", name = GetRandomName(), score = 600 });
        leaderboardList.Add(new Leaderboard { rank = "14", name = GetRandomName(), score = 550 });
        leaderboardList.Add(new Leaderboard { rank = "15", name = GetRandomName(), score = 520 });
        leaderboardList.Add(new Leaderboard { rank = "16", name = GetRandomName(), score = 400 });
        leaderboardList.Add(new Leaderboard { rank = "17", name = GetRandomName(), score = 360 });
        leaderboardList.Add(new Leaderboard { rank = "18", name = GetRandomName(), score = 300 });
        leaderboardList.Add(new Leaderboard { rank = "19", name = GetRandomName(), score = 240 });
        leaderboardList.Add(new Leaderboard { rank = "20", name = GetRandomName(), score = 120 });

        Init(leaderboardList);
    }

    protected override void Init()
    {
        base.Init();

        leaderboardPageView = (LeaderboardPageView)view;
        leaderboardPageView.SetShowAllButtonEvent(ShowAllLeaderboard);
        Debug.LogError("set show all event");
    }

    private void ShowAllLeaderboard()
    {
        Debug.Log("ShowAllLeaderboard");
        leaderboardPageView.ShowAllLeaderboard();
    }

    private string GetRandomName()
    {
        List<string> nameList = new List<string>
        {
            "Andy", "Anton", "Anwar", "Adi", "Agus",
            "Adrian", "Andrian", "Arya", "Bella", "Berto",
            "Bernard", "Betty", "Billy", "Chris", "Clara",
            "Chloe", "Charles", "Catherine", "Caroline", "Carolina",
            "Cindy", "Dessy", "Deasy", "Dono", "Dian",
            "Doro", "Dani", "Eve", "Eva", "Ellen",
            "Edy", "Edward", "Elly", "Eka", "Eci",
            "Ela", "Endah", "Eca", "Fani", "Fian",
            "Fandi", "Farhan", "Firman", "Faulkner", "Fransisca",
            "Fransisco", "Frandy", "George", "Georgia", "Gerry",
            "Gion", "Gideon", "Hans", "Herman", "Harry",
            "Hilda", "Hansen", "Handi", "Hadi", "Hengky",
            "Ida", "Isabella", "Isabelle", "Intan", "Indri",
            "Indra", "Ivan", "Indriyani", "Ivonne", "Ivy",
            "Ika", "Ian", "Igor", "Jimmy", "Jemi",
            "Juni", "June", "July", "Juiliana", "Jasmine",
            "John", "Johny", "Juan", "Jenny", "Jane",
            "Jodi", "Jaka", "Joko", "Jessica", "Jeremy",
            "Kevin", "Karen", "Kane", "Ken", "Kennedy",
            "Kalvin", "Krystal", "Katrynn", "Kusuma", "Martha",
            "Maretha", "Mira", "Millen", "Michael", "Michelle",
            "Margaretha", "Marsha", "Muklis", "Mandra", "Marry",
            "Maria", "Nancy", "Ningsih", "Nabila", "Nazuardi",
            "Nugraha", "Oscar", "Olive", "Olivia", "Oktavia",
            "Okta", "Owen", "Patro", "Patrick", "Patricia",
            "Phillip", "Philia", "Phillipus", "Peter", "Paul",
            "Paula", "Queeny", "Rio", "Rendi", "Rangga",
            "Robert", "Rosella", "Raka", "Shella", "Stella",
            "Simon", "Sinta", "Sindy", "Siska", "Siti",
            "Thomas", "Tia", "Tika", "Tiwi", "Theresia",
            "Udin", "Ula", "Vinny", "Vina", "Vonny",
            "Vindy", "Vivi", "Vivian", "Vandy", "Willy",
            "William", "Wanto", "Wandy", "Windy", "Xavier",
            "Yogi", "Yenny", "Yahya", "Yeni", "Yuli",
            "Yuliana", "Yasmin", "Zacharia", "Zulkifli"
        };

        return nameList[Random.Range(0, nameList.Count)];
    }

    public void Init(List<Leaderboard> leaderboardList)
    {
        for (int i = 0; i < leaderboardItemList.Count; i++)
        {
            leaderboardItemList[i].SetActive(false);
        }

        for (int i = 0; i < leaderboardList.Count; i++)
        {
            if (i < showLimit || showLimit == -1)
            {
                GameObject item = GetAnswerItem();
                item.SetActive(true);
                item.GetComponent<LeaderboardItem>().Init(this, leaderboardList[i], clickable);
                leaderboardItemList.Add(item);
            }
        }
    }

    private GameObject SpawnAnswerItem()
    {
        return Instantiate(leaderboardItemPrefab, leaderboardParent, false);
    }

    private GameObject GetAnswerItem()
    {
        for (int i = 0; i < leaderboardItemList.Count; i++)
        {
            if (!leaderboardItemList[i].activeSelf)
            {
                return leaderboardItemList[i];
            }
        }

        return SpawnAnswerItem();
    }
}
