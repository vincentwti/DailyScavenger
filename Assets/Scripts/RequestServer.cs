using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class RequestServer : MonoBehaviour
{
    public static RequestServer Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void LoadDummyTaskList(Action<List<DummyTask>> onComplete)
    {
        StartCoroutine(LoadDummyTask(onComplete));
    }

    private IEnumerator LoadDummyTask(Action<List<DummyTask>> onComplete)
    {
        string url = "https://dickyri.net/_optitreasure/list_tasks_api.php?event_id=8";
        UnityWebRequest uwr = new UnityWebRequest(url);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        yield return uwr.SendWebRequest();
        if (uwr.result == UnityWebRequest.Result.Success)
        {
            List<DummyTask> dummyTaskList = JsonConvert.DeserializeObject<List<DummyTask>>(uwr.downloadHandler.text);
            //List<DummyTask> dummyTaskList = JsonUtility.FromJson<List<DummyTask>>(uwr.downloadHandler.text);
            onComplete?.Invoke(dummyTaskList);
        }
        else
        {
            Debug.LogError("err : " + uwr.result);
        }
    }

    public void LoadEventList(Action<List<EventInfo>> onCompelete)
    {
        StartCoroutine(LoadEventInfo(onCompelete));
    }

    private IEnumerator LoadEventInfo(Action<List<EventInfo>> onComplete)
    {
        string url = "https://dickyri.net/_optitreasure/list_events_api.php";
        UnityWebRequest uwr = new UnityWebRequest(url);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        yield return uwr.SendWebRequest();
        if (uwr.result == UnityWebRequest.Result.Success)
        {
            List<EventInfo> eventList = JsonConvert.DeserializeObject<List<EventInfo>>(uwr.downloadHandler.text);
            //List<DummyTask> dummyTaskList = JsonUtility.FromJson<List<DummyTask>>(uwr.downloadHandler.text);
            onComplete?.Invoke(eventList);
        }
        else
        {
            Debug.LogError("err : " + uwr.result);
        }
    }
}
