using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCardBackIntoHand : MonoBehaviour
{
    private CardSelector cardSelector;

    void Start()
    {
        cardSelector = GetComponent<CardSelector>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject handPile = GetPlayerHandPile();
            if(handPile != null)
            {
                AddCardBackIntoHand(handPile);
                cardSelector.DeleteSelectedCardInstance();
            }
        }
    }

    private GameObject GetPlayerHandPile()
    {
        SP_CardPile[] cardPiles = FindObjectsOfType<SP_CardPile>();
        foreach(SP_CardPile cardPile in cardPiles)
        {
            if(cardPile.GetCardPileType() == SP_CardPile.CardPileType.HAND_PILE)
            {
                return cardPile.gameObject;
            }
        }
        return null;
    }

    private void AddCardBackIntoHand(GameObject handPile)
    {
        cardSelector.AnimateCardToCardPile(handPile);
    }
}
