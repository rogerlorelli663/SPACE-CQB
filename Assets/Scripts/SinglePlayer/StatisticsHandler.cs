using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsHandler : MonoBehaviour
{
    [SerializeField] private Manage_Card_Collection Collection;
    [SerializeField] private Manage_Card_Collection ActiveDeck;
    [SerializeField] private int MAXCARDS = 60;
    [SerializeField] private int MAXSPECIAL = 15; 
    [SerializeField] private Text MaxDeckPop;
    [SerializeField] private Text CurrentDeckPop;
    [SerializeField] private Text TotalDeckPower;
    [SerializeField] private Text CurrentDeckUnit;
    [SerializeField] private Text MaxSpecialCards;
    [SerializeField] private Text CurrentSpecialCards;

    private void Start()
    {
        MaxDeckPop.text = MAXCARDS.ToString();
        MaxSpecialCards.text = MAXSPECIAL.ToString();
    }

    private void Update()
    {
        TotalDeckPower.text = ActiveDeck.GetDeckPower().ToString();
        CurrentSpecialCards.text = ActiveDeck.GetNumType(CQBCard.CardType.EFFECT).ToString();
        CurrentDeckPop.text = ActiveDeck.gameObject.transform.childCount.ToString();
    }

    public void SetActiveDeck(Manage_Card_Collection deck)
    {
        this.ActiveDeck = deck;
    }
}
