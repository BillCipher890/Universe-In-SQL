using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static CardPlacer;
using System;

public class CardMover : MonoBehaviour
{
    [SerializeField] private int speed;
    private int currentSpeed;
    private bool isMoving;
    private Vector3 startPosition;

    private Plane dragPlane;
    private Vector3 offset;
    private Camera mainCamera;

    private bool inTrigger;

    private void Awake()
    {
        mainCamera = Camera.main;
        isMoving = false;
        inTrigger = false;
        currentSpeed = speed;
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnMouseDown()
    {
        if (gameObject.GetComponent<CardController>().currentCardType != CardController.CardType.Deactivated)
        {
            isMoving = true;

            //Создание плоскости, вдоль которой двигается карточка
            dragPlane = new Plane(mainCamera.transform.forward, transform.position);
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);

            float planeDistance;
            dragPlane.Raycast(cameraRay, out planeDistance);
            offset = transform.position - cameraRay.GetPoint(planeDistance);
        }
    }

    private void OnMouseDrag()
    {
        if (gameObject.GetComponent<CardController>().currentCardType != CardController.CardType.Deactivated)
        {
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);

            float planeDistance;
            dragPlane.Raycast(cameraRay, out planeDistance);
            transform.position = cameraRay.GetPoint(planeDistance) + offset;
        }
    }

    private void OnMouseUp()
    {
        isMoving = false;
        if (inTrigger)
        {
            CardPlacer cp = transform.parent.GetComponent<CardPlacer>();

            FillRequest();

            ActivateActionCards();

            var cardController = transform.GetComponent<CardController>();

            if (cardController.currentCardType == CardController.CardType.Action ||
                cardController.currentCardType == CardController.CardType.Condition)
            {
                for(int i = 0; i < transform.parent.childCount; i++)
                {
                    if (transform.parent.GetChild(i).name != gameObject.name)
                        transform.parent.GetChild(i).gameObject.SetActive(false);
                }
            }

            var cardData = gameObject.GetComponent<CardController>().cardData;

            cp.CardPlace(cardData.callingCardData, mainCamera, gameObject, transform.parent);

            SendCardName(cardData.CardName);
            
            if(cardData.CardName == ";")
            {
                EventManagerLevel1.sendSemicolonUsed();
            }

            gameObject.SetActive(false);
        }
    }

    private void SendCardName(string name)
    {
        var goArray = gameObject.scene.GetRootGameObjects();
        for(int i = 0; i < goArray.Length; i++)
        {
            if(goArray[i].name == "LevelController")
            {
                goArray[i].GetComponent<LevelData>().usedCardName = name;
            }
        }
    }

    private void ActivateActionCards()
    {
        if (gameObject.GetComponent<CardController>().currentCardType != CardController.CardType.Action)
        {
            for (int i = 0; i < transform.parent.childCount; i++)
            {
                if (transform.parent.GetChild(i).GetComponent<CardController>().cardData.Type == CardController.CardType.Action)
                {
                    transform.parent.GetChild(i).GetComponent<CardController>().currentCardType = CardController.CardType.Action;
                }
            }
        }
    }

    private void FillRequest()
    {
        TMP_Text Request = GameObject.Find("Request").GetComponent<TMP_Text>();
        if (gameObject.name == "SELECT")
        {
            Request.text = "";
        }
        Request.text += gameObject.name + " ";
    }

    private void Update()
    {
        if (!isMoving && !(startPosition - transform.position == new Vector3(0, 0, 0)))
        {
            Vector3 moveVector = startPosition - transform.position;
            transform.Translate(Vector3.Normalize(moveVector) * currentSpeed * Time.deltaTime);
            if(Vector3.Distance(startPosition, transform.position) < 0.25)
            {
                currentSpeed = speed / 2;
            }
            if(Mathf.Abs(startPosition.x - transform.position.x) + Mathf.Abs(startPosition.y - transform.position.y) < 0.002)
            {
                transform.position = startPosition;
                currentSpeed = speed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        inTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }
}
