using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Management_Scene_Handler : MonoBehaviour
{
    [SerializeField] private AccountCharacteristics Account;
    [SerializeField] private Manage_Card_Collection Collection;
    [SerializeField] private Manage_Card_Collection Manage_Collection;
    [SerializeField] private Manage_Card_Collection Manage_Deck1;
    [SerializeField] private Manage_Card_Collection Manage_Deck2;
    [SerializeField] private Manage_Card_Collection Manage_Deck3;
    [SerializeField] private GameCardsBehavior AllCards;
    [SerializeField] private List<string> deck_list;
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private ManageCardSelector Selector;
    [SerializeField] private StatisticsHandler Stats;

    private void Start()
    {
        //Create all cards within the collection

        Account = GameObject.Find("ActiveAccount").GetComponent<AccountCharacteristics>();

        GameObject card;
        CQBCard.FactionType kushan = CQBCard.FactionType.KUSHAN;
        //CQBCard.FactionType taiidan = CQBCard.FactionType.TAIIDAN;

        Debug.Log("Manage-Kushan");
        Debug.Log(Account.playername);
        Account.PrintCollection(0);

        List<string> tempCollection = Account.GetFactionCollection(kushan);
        foreach(string cardname in tempCollection)
        {
            card = AllCards.CreateCard(cardname);
            Collection.AddCard(card);
        }

        // Default View - All cards

        Collection.TransferCards(Manage_Collection.gameObject, kushan);

        deck_list = Account.GetDeck(0);
        Debug.Log(string.Join(" ", deck_list.ToArray()));
        Manage_Collection.ListTransfer(deck_list, Manage_Deck1.gameObject.transform);
        deck_list = Account.GetDeck(1);
        Manage_Collection.ListTransfer(deck_list, Manage_Deck2.gameObject.transform);
        deck_list = Account.GetDeck(2);
        Manage_Collection.ListTransfer(deck_list, Manage_Deck3.gameObject.transform);

        Manage_Deck2.gameObject.SetActive(false);
        Manage_Deck3.gameObject.SetActive(false);
    }

    public void Change_Deck(int val)
    {
        switch(val)
        {
            case 0:
                if (!Manage_Deck1.gameObject.activeSelf)
                {
                    Manage_Deck1.gameObject.SetActive(true);
                    Selector.SetActiveDeck(Manage_Deck1);
                    Stats.SetActiveDeck(Manage_Deck1);
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
                    Selector.SetActiveDeck(Manage_Deck2);
                    Stats.SetActiveDeck(Manage_Deck2);
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
                    Selector.SetActiveDeck(Manage_Deck3);
                    Stats.SetActiveDeck(Manage_Deck3);
                    Manage_Deck3.gameObject.SetActive(true);
                }
                break;
            default:
                if (!Manage_Deck1.gameObject.activeSelf)
                {
                    Selector.SetActiveDeck(Manage_Deck1);
                    Stats.SetActiveDeck(Manage_Deck1);
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

    public void Ready()
    {
        Save();
        Account.UpdateSaveFile();
        SceneManager.LoadScene("CQBPrototype2");
    }

    public void Save()
    {
        Account.SetDeck(1, Manage_Deck1.GetList());
        Account.SetDeck(2, Manage_Deck2.GetList());
        Account.SetDeck(3, Manage_Deck3.GetList());
    }

}
