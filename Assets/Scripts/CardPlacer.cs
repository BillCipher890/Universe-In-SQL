using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlacer : MonoBehaviour
{
    [SerializeField] private CardData[] startCardData;
    [SerializeField] private GameObject card;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        CardPlace();
        BoxColliderReplace();

        Debug.Log(startCardData[0].CardName);
        Debug.Log(startCardData[0].Type);
        for(int i = 0; i < startCardData[0].callingCardData.Length; i++)
            Debug.Log(startCardData[0].callingCardData[i].CardName);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CardPlace()
    {
        transform.position = mainCamera.transform.position;

        var currentCard = Instantiate(card, mainCamera.transform.position + 3 * mainCamera.transform.forward - new Vector3(-0.5f, 1, 1), mainCamera.transform.rotation, transform);
        currentCard.GetComponent<CardController>().currentCardType = CardController.CardType.Action;
        currentCard.GetComponent<CardController>().text = "SELECT";

        currentCard = Instantiate(card, mainCamera.transform.position + 3 * mainCamera.transform.forward - new Vector3(0f, 1, 1), mainCamera.transform.rotation, transform);
        currentCard.GetComponent<CardController>().currentCardType = CardController.CardType.Table;
        currentCard.GetComponent<CardController>().text = "Table";

        currentCard = Instantiate(card, mainCamera.transform.position + 3 * mainCamera.transform.forward - new Vector3(0.5f, 1, 1), mainCamera.transform.rotation, transform);
        currentCard.GetComponent<CardController>().currentCardType = CardController.CardType.Element;
        currentCard.GetComponent<CardController>().text = "Element";
    }

    void BoxColliderReplace()
    {
        transform.GetComponent<BoxCollider>().center = 3 * mainCamera.transform.forward - new Vector3(0, -1, -1);
    }
}
