﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawCardsSingle : MonoBehaviour
{
    [SerializeField] private GameObject PlayerDeck;
    [SerializeField] private GameObject OpponentDeck;
    [SerializeField] private GameObject PlayerHand;
    [SerializeField] private GameObject OpponentHand;
    [SerializeField] private GameObject Playerfield;
    [SerializeField] private GameObject Opponentfield;
    [SerializeField] private Dropdown Dropdown;
    [SerializeField] private GameObject GameMaster;
    private const int DEALTCARDAMOUNT = 5;

    public void OnClick()
    {
        Playerfield.SetActive(true);
        Opponentfield.SetActive(true);
        PlayerHand.SetActive(true);
        OpponentHand.SetActive(true);
        List<GameObject> cards = new List<GameObject>();

        GameObject.Find("SP_Game Master").GetComponent<GameMaster>().InitializeSeed();
        GameObject.Find("SP_Game Master").GetComponent<GameMaster>().SetCards(Dropdown.value);
        PlayerDeck.GetComponent<SP_Deck>().DealCards(DEALTCARDAMOUNT, PlayerHand);
        Debug.Log("Dealt Player's Cards");
        PlayerHand.GetComponent<SP_CardPile>().ShowBase();
        OpponentDeck.GetComponent<SP_Deck>().DealCards(DEALTCARDAMOUNT, OpponentHand);
        Debug.Log("Dealt Opponent's Cards");
        OpponentHand.GetComponent<SP_CardPile>().ShowBack();
        GameObject.Find("SP_Game Master").GetComponent<GameMaster>().cardsDelt = true;
        GameObject.Find("SP_Game Master").GetComponent<GameMaster>().EnableBoard();
        GameMaster.GetComponent<GameMaster>().ContinueGamePrep();
        
        Debug.Log("Set delt to true");

        //gameObject.SetActive(false);
        Debug.Log("deactivated deal button");
    }

    public int NumberOfCards()
    {
        return DEALTCARDAMOUNT;
    }

}
