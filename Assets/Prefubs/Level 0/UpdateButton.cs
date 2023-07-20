using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateButton : MonoBehaviour
{
    [SerializeField] private GameObject mainButton;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private string[] names;
    [SerializeField] private Material[] materials;
    private int choosenIndex = 0;

    [SerializeField] private Transform updatedCone;

    private bool isChoosing = false;
    private bool task1Completed = false;

    private void Start()
    {
        EventManager.onPlayerCompleteTask1 += Task1CompletedSetTrue;
        mainButton.transform.GetChild(0).GetComponent<TMP_Text>().text = names[choosenIndex];

        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].transform.GetChild(0).GetComponent<TMP_Text>().text = names[i];
        }
    }

    private void OnDestroy()
    {
        EventManager.onPlayerCompleteTask1 -= Task1CompletedSetTrue;
    }

    public void DropDown()
    {
        if (task1Completed)
        {
            if (isChoosing)
            {
                foreach (var button in buttons)
                {
                    button.SetActive(false);
                }

                updatedCone.GetComponent<MeshRenderer>().material = materials[choosenIndex];
                mainButton.GetComponentInChildren<TMP_Text>().text = names[choosenIndex];
                mainButton.SetActive(true);
            }
            else
            {
                mainButton.SetActive(false);
                foreach (var button in buttons)
                {
                    button.SetActive(true);
                }
            }
            isChoosing = !isChoosing;
        }
    }

    public void SetChoosenName0()
    {
        choosenIndex = 0;
    }

    public void SetChoosenName1()
    {
        choosenIndex = 1;
    }

    private void Task1CompletedSetTrue()
    {
        EventManager.onPlayerCompleteTask1 -= Task1CompletedSetTrue;
        Debug.Log("task1Completed");
        task1Completed = true;
    }
}
