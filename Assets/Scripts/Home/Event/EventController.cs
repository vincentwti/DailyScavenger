using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class EventInfo
{
    public string event_id;
    public string event_name;
    public string event_description;
    public string event_start_date;
    public string event_end_date;
}

public class EventController : MonoBehaviour
{
    public List<EventInfo> eventList = new List<EventInfo>();

    public Transform eventParent;
    public GameObject eventPrefab;

    public void LoadEventList(HomePage homePage)
    {
        RequestServer.Instance.LoadEventList((eventList) => {
            foreach (EventInfo info in eventList)
            {
                GameObject item = Instantiate(eventPrefab, eventParent, false);
                item.GetComponent<EventItem>().Init(homePage, info);
            }
        });
    }
}
