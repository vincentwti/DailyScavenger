using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventItem : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public Button clickButton;

    private EventInfo info;
    private HomePage controller;

    public void Init(HomePage controller, EventInfo info)
    {
        this.controller = controller;
        this.info = info;
        nameText.text = info.event_name;
        descriptionText.text = info.event_description;
        clickButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        if (!controller.IsShowMenu)
            controller.ShowHuntEvent();
    }
}
