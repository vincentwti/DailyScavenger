using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hyperlink : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text text;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (text)
        {
            int index = TMP_TextUtilities.FindIntersectingLink(text, eventData.position, null);
            if (index >= 0)
            {
                Debug.LogWarning("link : " + text.textInfo.linkInfo[index].GetLinkID());
                Application.OpenURL(text.textInfo.linkInfo[index].GetLinkID());
            }
        }
    }
}
