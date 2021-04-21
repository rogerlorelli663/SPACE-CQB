using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHighlight : MonoBehaviour
{

    private Vector2 originalScale;
    public Vector2 highlightedScale = new Vector2(1, 1);
    [SerializeField] int ShiftOffset = 40;

    private GameObject highlightedCard;

    void Start()
    {
        highlightedCard = null;
    }

    
    void Update()
    {
        GameObject hitCard = GetHighlightedCard();
        if (hitCard != null && highlightedCard == null && hitCard.transform.parent.GetComponent<SP_CardPile>() != null)
        {
            originalScale = hitCard.transform.localScale;
            highlightedCard = hitCard;
            //Debug.Log(hitCard.transform.localScale);
            highlightedCard.transform.localScale = new Vector3(1,1);
            //Debug.Log(hitCard.transform.localScale);
            highlightedCard.transform.position = new Vector3(highlightedCard.transform.position.x, highlightedCard.transform.position.y + 35, highlightedCard.transform.position.z - 1);
            ShiftSiblings(ShiftOffset, hitCard);
        }
        else if (hitCard != highlightedCard && highlightedCard != null)
        {
            highlightedCard.transform.localScale = originalScale;
            highlightedCard.transform.position = new Vector3(highlightedCard.transform.position.x, highlightedCard.transform.position.y - 35, highlightedCard.transform.position.z + 1);
            ShiftSiblings(-ShiftOffset, highlightedCard);
            highlightedCard = null;
        }
    }

    private GameObject GetHighlightedCard()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        RaycastHit2D[] objectsOnMousePosition = Physics2D.RaycastAll(mousePosition, Vector2.zero);
        GameObject PlayerHand = GameObject.Find("SP_Player Hand Field");
        foreach (RaycastHit2D objectHit in objectsOnMousePosition)
        {
            if (objectHit.collider.gameObject.GetComponent<CQBCard>() != null && PlayerHand != null && objectHit.collider.gameObject.transform.IsChildOf(PlayerHand.transform))
            {
                return objectHit.collider.gameObject;
            }
        }
        return null;
    }

    private void ShiftSiblings(int direction, GameObject hit_card)
    {
        GameObject parent_pile = hit_card.transform.parent.gameObject;
        GameObject sibling_card;
        int sibling_index = hit_card.transform.GetSiblingIndex();

        for(int i = sibling_index + 1; i < parent_pile.transform.childCount; i++)
        {
            sibling_card = parent_pile.transform.GetChild(i).gameObject;
            //Debug.Log(sibling_card);
            sibling_card.transform.position = new Vector3(sibling_card.transform.position.x + direction, sibling_card.transform.position.y, sibling_card.transform.position.z);
        }
    }

}
