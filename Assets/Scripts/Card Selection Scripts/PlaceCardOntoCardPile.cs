using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlaceCardOntoCardPile : MonoBehaviour
{
    // private PlayerBehavior PlayerBehavior;
    [SerializeField] private GameObject GameMaster;
    private CardSelector cardSelector;
    private SP_CardPile.CardPileType cardType;
    private GameObject card;
    void Start()
    {
        cardSelector = GetComponent<CardSelector>();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject cardPile = GetClickedCardPile();
            if (cardPile != null && cardSelector.IsCardPlaceableAndInCardSelector() && !cardPile.tag.Contains("Enemy Melee") && !cardPile.tag.Contains("Enemy Range") && !cardPile.tag.Contains("Enemy Siege"))
            {
                card = cardSelector.GetOriginalCard();
                cardType = card.GetComponent<CQBCard>().GetCardType();
                if (cardType == cardPile.GetComponent<SP_CardPile>().GetCardPileType())
                {
                    //NetworkIdentity networkIdentity = NetworkClient.connection.identity;
                    //PlayerBehavior = networkIdentity.GetComponent<PlayerBehavior>();
                    AddCardToCardPile(cardPile);
                    //PlayerBehavior.PlayCard(card);
                    cardSelector.DeleteSelectedCardInstance();
                }
            }
        }
    }

    private GameObject GetClickedCardPile()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        RaycastHit2D[] objectsOnMousePosition = Physics2D.RaycastAll(mousePosition, Vector2.zero);

        foreach (RaycastHit2D objectHit in objectsOnMousePosition)
        {
            if (objectHit.collider.gameObject.GetComponent<SP_CardPile>() != null)
            {
                return objectHit.collider.gameObject;
            }
        }
        return null;
    }

    private void AddCardToCardPile(GameObject cardPile) 
    {
        cardSelector.AnimateCardToCardPile(cardPile);
        cardPile.GetComponent<SP_CardPile>().TransferCardToCardPile(cardSelector.GetOriginalCard());
        GameMaster.GetComponent<GameMaster>().SetPlayedCard(cardSelector.GetOriginalCard());
    }
}
