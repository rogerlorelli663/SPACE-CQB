using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_PileCounter : MonoBehaviour
{
    [SerializeField] SP_CardPile PlayerHand;
    [SerializeField] SP_CardPile PlayerField;
    [SerializeField] SP_CardPile OpponentHand;
    [SerializeField] SP_CardPile OpponentField;
    [SerializeField] Modifiers Modifiers;

    //public SP_CardPile UnitField;
    private int cardsInPile;
    private int pileTotal;

    // Start is called before the first frame update
    void Start()
    {
        cardsInPile = 0;
        pileTotal = 0;
    }

    private bool UpdateCheck()
    {
        if(cardsInPile != PlayerField.GetNumberOfCards())
        {
            cardsInPile = PlayerField.GetNumberOfCards();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdateCounter()
    {
        int[] cardsPower = new int[cardsInPile];
        List<GameObject> cards = PlayerField.GetCardsInCardPile();
        int index = 0;
        int sum = 0;
        foreach(GameObject card in cards)
        {
            cardsPower[index] = card.GetComponent<CQBCard>().GetCurrentPower();
            index++;
        }
        for(int i = 0; i < index; i++)
        {
            sum += cardsPower[i];
        }
        this.pileTotal = sum;
    }

    public int GetPileTotal()
    {
        if(UpdateCheck())
        {
            UpdateCounter();
        }
        return this.pileTotal;
    }
}
