using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_Deck : MonoBehaviour
{
    // Start is called before the first frame update
    private List<GameObject> cards = new List<GameObject>();

    public List<GameObject> GetCards()
    {
        List<GameObject> tempCards = new List<GameObject>();
        for(int i = 0; i < cards.Count; i++)
        {
            tempCards.Add(cards[i]);
        }
        return tempCards;
    }

    public void CreateNewDeck(List<string> cards)
    {
        GameCardsBehavior AllCards = GameObject.Find("SP_GameCards").GetComponent<GameCardsBehavior>();
        GameObject card;
        foreach(string card_name in cards)
        {
            card = AllCards.CreateCard(card_name);
            AddCardToDeck(card);
            card.name = card.name.Replace("(Clone)", "");
            card.GetComponent<CQBCard>().ActivateBack();
            card.transform.localScale = new Vector2(.6f, .6f);
        }
    }

    private void AddCardToDeck(GameObject card)
    {
        card.transform.SetParent(this.transform);
        cards.Add(card);
    }

    public void ShuffleDeck()
    {
        int seed = (int)(Time.realtimeSinceStartup * 100);
        Debug.Log("Seed: " + seed);
        Random.InitState(seed);
        for(int i = 0; i < cards.Count; i++)
        {
            GameObject card = cards[i];
            int randomIndex = Random.Range(0, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = card;
        }
    }

    public void DealCards(int number_of_cards, GameObject newParent)
    {
        cards = GetCards();
        GameObject card;
        int iterations;
        Debug.Log("Cards: " + cards.Count);
        if (number_of_cards >= cards.Count)
        {
            iterations = cards.Count;
        }
        else
        {
            iterations = number_of_cards;
        }
        Debug.Log("Iterations: " + iterations);
        for(int i = 0; i < iterations; i++)
        {
            card = cards[0];
            Debug.Log("Delt: " + card);
            if(cards.Count != 0)
            {
                cards.RemoveAt(0);
            }
            card.transform.SetParent(newParent.transform);
            card.GetComponent<CQBCard>().ActivatePlayable();
            card.GetComponent<CQBCard>().ActivateBase();
        }
    }

    public void ShowBack()
    {
        foreach(GameObject card in cards)
        {
            card.GetComponent<CQBCard>().ActivateBack();
        }
    }
}
