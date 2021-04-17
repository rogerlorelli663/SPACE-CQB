using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckSelection : MonoBehaviour
{
    [SerializeField] private Manage_Card_Collection Manage_Deck1;
    [SerializeField] private Manage_Card_Collection Manage_Deck2;
    [SerializeField] private Manage_Card_Collection Manage_Deck3;
    [SerializeField] private AccountCharacteristics Account;
    [SerializeField] private GameCardsBehavior AllCards;
    [SerializeField] private GameObject TokenField;

    private void Start()
    {
        Account = GameObject.Find("ActiveAccount").GetComponent<AccountCharacteristics>();
        //TESTING
        Account.LoadExistingPlayer(Account.GetPlayerName());
        //
        GameObject card;
        Manage_Card_Collection Collection;
        for (int i = 0; i < 3; i++)
        {
            List<string> tempCollection = Account.GetDeck(i);
            Collection = GetDeck(i);
            foreach (string cardname in tempCollection)
            {
                card = AllCards.CreateCard(cardname);
                card.GetComponent<CQBCard>().ActivatePlayable();
                card.GetComponent<CQBCard>().ActivateBase();
                Collection.AddCard(card);
                card.transform.localScale = new Vector3(.6f, .6f);
            }
        }
        Change_Deck(0);

    }

    public void Deactivate()
    {
        if(gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    public void Activate()
    {
        if(!gameObject.activeSelf)
            gameObject.SetActive(true);
    }

    private Manage_Card_Collection GetDeck(int deck_number)
    {
        Manage_Card_Collection reference;
        switch(deck_number)
        {
            case 0:
                reference = Manage_Deck1;
                break;
            case 1:
                reference = Manage_Deck2;
                break;
            case 2:
                reference = Manage_Deck3;
                break;
            default:
                reference = Manage_Deck1;
                break;

        }
        return reference;
    }

    public void Change_Deck(int val)
    {
        switch (val)
        {
            case 0:
                if (!Manage_Deck1.gameObject.activeSelf)
                {
                    Manage_Deck1.gameObject.SetActive(true);
                }
                if (Manage_Deck2.gameObject.activeSelf)
                {
                    Manage_Deck2.gameObject.SetActive(false);
                }
                if (Manage_Deck3.gameObject.activeSelf)
                {
                    Manage_Deck3.gameObject.SetActive(false);
                }
                break;
            case 1:
                if (Manage_Deck1.gameObject.activeSelf)
                {
                    Manage_Deck1.gameObject.SetActive(false);
                }
                if (!Manage_Deck2.gameObject.activeSelf)
                {
                    Manage_Deck2.gameObject.SetActive(true);
                }
                if (Manage_Deck3.gameObject.activeSelf)
                {
                    Manage_Deck3.gameObject.SetActive(false);
                }
                break;
            case 2:
                if (Manage_Deck1.gameObject.activeSelf)
                {
                    Manage_Deck1.gameObject.SetActive(false);
                }
                if (Manage_Deck2.gameObject.activeSelf)
                {
                    Manage_Deck2.gameObject.SetActive(false);
                }
                if (!Manage_Deck3.gameObject.activeSelf)
                {
                    Manage_Deck3.gameObject.SetActive(true);
                }
                break;
            default:
                if (!Manage_Deck1.gameObject.activeSelf)
                {
                    Manage_Deck1.gameObject.SetActive(true);
                }
                if (Manage_Deck2.gameObject.activeSelf)
                {
                    Manage_Deck2.gameObject.SetActive(false);
                }
                if (Manage_Deck3.gameObject.activeSelf)
                {
                    Manage_Deck3.gameObject.SetActive(false);
                }
                break;
        }
    }

    public List<GameObject> GetTokens()
    {
        List<GameObject> tokens = new List<GameObject>();
        for(int i = 0; i < TokenField.transform.childCount; i++)
        {
            tokens.Add(TokenField.transform.GetChild(i).gameObject);
        }
        return tokens;
    }
}
