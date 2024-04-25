using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    public TMP_InputField answerInputField;

    private void OnEnable()
    {
        answerInputField.text = "";
    }

    public bool CheckAnswer(string answer)
    {
        if (answerInputField.text.ToLower() == answer.ToLower())
        {
            return true;
        }
        return false;
    }
}
