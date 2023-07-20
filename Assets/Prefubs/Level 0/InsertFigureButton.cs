using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InsertFigureButton : MonoBehaviour
{
    [SerializeField] private Transform idButton;
    [SerializeField] private GameObject namePanelText;
    [SerializeField] private GameObject formPanelText;
    [SerializeField] private GameObject colourPanelText;

    [SerializeField] private GameObject cube;

    private void Start()
    {
        idButton.GetComponent<Button>().enabled = false;
        EventManager.onPlayerCompleteTask2 += TurnOnButton;
    }

    public void AddButtonClicked()
    {
        idButton.GetChild(0).GetComponent<TMP_Text>().text = "7";
        namePanelText.SetActive(true);
        formPanelText.SetActive(true);
        colourPanelText.SetActive(true);
        cube.SetActive(true);

        idButton.GetComponent<Button>().enabled = false;
    }

    void TurnOnButton()
    {
        EventManager.onPlayerCompleteTask2 -= TurnOnButton;
        idButton.GetComponent<Button>().enabled = true;
    }
}
