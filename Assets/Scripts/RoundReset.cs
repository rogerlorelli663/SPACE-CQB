using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundReset : MonoBehaviour
{
    void Reset()
    {

        GameObject Outcome = GameObject.Find("Outcome");
        CardPile Player = GameObject.Find("Player Melee Field").GetComponentInChildren<CardPile>();
        CardPile Enemy = GameObject.Find("Enemy Melee Field").GetComponentInChildren<CardPile>();

        List<GameObject> cardpile = Player.GetCardsInCardPile();
        foreach(GameObject card in cardpile)
        {
            
        }


        
    }

}
