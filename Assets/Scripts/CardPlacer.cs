using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlacer : MonoBehaviour
{
    [SerializeField] private CardData[] startCardData;
    [SerializeField] private GameObject cardPrefub;

    void Start()
    {
        Camera mainCamera = Camera.main;
        transform.position = mainCamera.transform.position;

        CardPlace(startCardData, mainCamera, cardPrefub, transform);
        BoxColliderReplace(mainCamera);
    }

    void Update()
    {

    }

    public void CardPlace(CardData[] cards, Camera mainCamera, GameObject card, Transform parrent)
    {
        for(int i = 0; i < cards.Length; i++)
        {
            var currentCard = Instantiate(card, 
                mainCamera.transform.position + 3 * mainCamera.transform.forward - new Vector3((1 - cards.Length + 2 * i) / 4f, 1, 1),
                mainCamera.transform.rotation, parrent);

            currentCard.name = cards[i].CardName;
            currentCard.GetComponent<CardController>().engName = cards[i].CardName;
            currentCard.GetComponent<CardController>().rusName = cards[i].RusCardName;
            if (cards[i].CardName == "FROM" || cards[i].CardName == "WHERE" || (cards[i].CardName == ";" && cards[i + 1].CardName == "WHERE"))
            {
                currentCard.GetComponent<CardController>().currentCardType = CardController.CardType.Deactivated;
            }
            else
            {
                currentCard.GetComponent<CardController>().currentCardType = cards[i].Type;
            }
            currentCard.GetComponent<CardController>().cardData = cards[i];
        }
    }

    void BoxColliderReplace(Camera mainCamera)
    {
        transform.GetComponent<BoxCollider>().center = 3 * mainCamera.transform.forward - new Vector3(0, -1, -1);
    }
}
