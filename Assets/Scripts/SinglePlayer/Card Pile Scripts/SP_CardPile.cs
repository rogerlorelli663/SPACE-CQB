using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_CardPile : MonoBehaviour
{
    [SerializeField] private CardPileType cardPileType = CardPileType.NO_TYPE;
    public enum CardPileType
    {
        NO_TYPE = 0,
        HAND_PILE = 1,
        DISCARD_PILE = 2,
        DECK_PILE = 3,
        MELEE_FIELD = 4,
        RANGE_FIELD = 5,
        SIEGE_FIELD = 6,
        EFFECT_FIELD = 7,
        MORALE_BOOST_FIELD = 8
    }

    public void TransferCardToCardPile(GameObject card)
    {
        card.transform.SetParent(gameObject.transform);
    }

    public int GetNumberOfCards()
    {
        return transform.childCount;
    }

    public List<GameObject> GetCardsInCardPile()
    {
        List<GameObject> cards = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            cards.Add(transform.GetChild(i).gameObject);
        }
        return cards;
    }

    public CardPileType GetCardPileType()
    {
        return cardPileType;
    }

    public void ClearPile()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    public List<GameObject> GetUnitCards(CQBCard.UnitType unitType)
    {
        List<GameObject> cards = new List<GameObject>();
        GameObject card;
        for (int i = 0; i < transform.childCount; i++)
        {
            card = transform.GetChild(i).gameObject;
            if(card != null && card.GetComponent<CQBCard>().GetUnitType() == unitType)
            {
                cards.Add(card);
            }
        }
        return cards;
    }

    public void AddCardstoPile(List<GameObject> cards)
    {
        foreach(GameObject card in cards)
        {
            card.transform.SetParent(this.gameObject.transform);
        }
    }

    public void ShowBack()
    {
        List<GameObject> cards = GetCardsInCardPile();
        foreach (GameObject card in cards)
        {
            card.GetComponent<CQBCard>().ActivateBack();
        }
    }

    public void ShowBase()
    {
        List<GameObject> cards = GetCardsInCardPile();
        foreach (GameObject card in cards)
        {
            card.GetComponent<CQBCard>().ActivateBase();
        }
    }
}
