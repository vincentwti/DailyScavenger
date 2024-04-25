using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;


[Serializable]
public class User
{
    public string id;
    public string name;
    public string username;
    public string email;
    public string completedTaskList;

    public User(string id, string name, string username, string email, string completedTaskList)
    {
        this.id = id;
        this.name = name;
        this.username = username;
        this.email = email;
        this.completedTaskList = completedTaskList;
    }
}

public class UserData : MonoBehaviour
{
    public int Score { get; private set; }
    public User user;

    public static UserData Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        user = new User(Guid.NewGuid().ToString(), "Seyren", "tester", "tester@gmail.com", "");
    }

    public void AddScore(int score)
    {
        Score += score;
    }

    public void LoadUserData()
    {
        
    }

    private IEnumerator RequestUserData(string url, Action onRequestSucceed, Action<string> onRequestFailed)
    {
        //http request
        UnityWebRequest uwr = new UnityWebRequest(url);
        yield return uwr.SendWebRequest();
        try
        {
            if (uwr.isDone)
            {

            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            
        }
    }
}
