using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CardController;

[CreateAssetMenu(menuName = "Card", fileName = "New Card")]
public class CardData : ScriptableObject
{
    [Tooltip("Name")]
    [SerializeField] private string cardName;
    public string CardName
    {
        get { return cardName; }
        protected set { }
    }

    [Tooltip("Type")]
    [SerializeField] private CardType type;
    public CardType Type
    {
        get { return type; }
        protected set { }
    }

    [Tooltip("CallingCards")]
    [SerializeField] public CardData[] callingCardData;
}
