using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceAnswerItem : MonoBehaviour
{
    public TMP_Text answerText;

    private int index;
    private Toggle toggle;
    private MultipleChoiceController controller;

    public void Init(MultipleChoiceController controller, int index, string content)
    {
        this.controller = controller;
        this.index = index;
        if (!toggle) toggle = GetComponent<Toggle>();
        answerText.text = content;
        toggle.onValueChanged.RemoveAllListeners();
        toggle.onValueChanged.AddListener(OnItemSelected);
    }

    private void OnItemSelected(bool value)
    {
        if (value) controller.SelectAnswer(index); 
    }
}
