using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MultipleChoice
{
    public List<string> options;
    public int answer;
}

public class MultipleChoiceController : MonoBehaviour
{
    private ToggleGroup toggleGroup;
    public Transform answerParent;
    public GameObject answerPrefab;

    private List<GameObject> answerItemList = new List<GameObject>();
    private int selectedIndex;
    private int correctIndex;

    private void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
    }

    public void Init(List<string> answerList, int correctIndex)
    {
        this.correctIndex = correctIndex;
        selectedIndex = -1;
        for (int i = 0; i < answerItemList.Count; i++)
        {
            answerItemList[i].SetActive(false);
        }

        for (int i = 0; i < answerList.Count; i++)
        {
            GameObject item = GetAnswerItem();
            item.SetActive(true);
            item.GetComponent<MultipleChoiceAnswerItem>().Init(this, i, answerList[i]);
            answerItemList.Add(item);
        }
    }

    public void SelectAnswer(int index)
    {
        selectedIndex = index;
    }

    public bool CheckAnswer()
    {
        return correctIndex == selectedIndex;
    }

    private GameObject SpawnAnswerItem()
    {
        return Instantiate(answerPrefab, answerParent, false);
    }

    private GameObject GetAnswerItem()
    {
        for (int i = 0; i < answerItemList.Count; i++)
        {
            if (!answerItemList[i].activeSelf)
            {
                return answerItemList[i];
            }
        }

        return SpawnAnswerItem();
    }
}
