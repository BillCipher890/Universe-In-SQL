using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlacerLearn : MonoBehaviour
{
    public GameObject Card;
    public GameObject Wall;

    // Start is called before the first frame update
    void Start()
    {
        var currentCard = Instantiate(Card, new Vector3(0, 0, -5), Quaternion.identity);
        currentCard.GetComponent<CardTuner>().cardType = 1;
        currentCard.GetComponent<CardTuner>().text = "SELECT";

        currentCard = Instantiate(Card, new Vector3(1, 0, -5), Quaternion.identity);
        currentCard.GetComponent<CardTuner>().cardType = 2;
        currentCard.GetComponent<CardTuner>().text = "Table";

        currentCard = Instantiate(Card, new Vector3(-1, 0, -5), Quaternion.identity);
        currentCard.GetComponent<CardTuner>().cardType = 3;
        currentCard.GetComponent<CardTuner>().text = "Id";

        Instantiate(Wall, new Vector3(0, 0, -5.01f), Quaternion.identity);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
