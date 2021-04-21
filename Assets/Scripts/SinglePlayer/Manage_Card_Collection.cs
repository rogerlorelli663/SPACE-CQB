using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Manage_Card_Collection : MonoBehaviour
{
    [SerializeField] private List<GameObject> Kushan_cards;
    [SerializeField] private List<GameObject> Taiidan_cards;

    [SerializeField] private List<string> list;

    public void Start()
    {
        this.Kushan_cards = new List<GameObject>();
        this.Taiidan_cards = new List<GameObject>();
    }

    public void AddCard(GameObject card)
    {
        card.transform.SetParent(this.gameObject.transform);
        card.name = card.name.Replace("(Clone)", "");

    }

    public void AddInstance(GameObject card)
    {
        Instantiate(card, gameObject.transform);
    }

    public List<GameObject> TransferCards(GameObject new_parent, CQBCard.FactionType faction)
    {
        List<GameObject> cards = new List<GameObject>();
        GameObject card;
        GameObject new_Card;
        for(int i = 0; i < transform.childCount; i++)
        {
            card = transform.GetChild(i).gameObject;
            if(card.GetComponent<CQBCard>().GetFaction() == faction)
            {
                new_Card = Instantiate(card, new_parent.transform, false);
                new_Card.name = new_Card.name.Replace("(Clone)", "");
                new_Card.transform.localScale = new Vector3(1f, 1f);
                new_Card.transform.localPosition = new Vector3(new_Card.transform.localPosition.x, new_Card.transform.localPosition.x, 0);
                new_Card.GetComponent<CQBCard>().ActivateManagement();
                cards.Add(new_Card);
            }
        }
        return cards;
    }

    public int GetDeckPower()
    {
        int totalpower = 0;
        GameObject card;
        for (int i = 0; i < transform.childCount; i++)
        {
            card = transform.GetChild(i).gameObject;
            totalpower += card.GetComponent<CQBCard>().GetBasePower();
        }
        return totalpower;
    }

    public int GetNumType(CQBCard.CardType type)
    {
        int cardcount = 0;
        GameObject card;
        for (int i = 0; i < transform.childCount; i++)
        {
            card = transform.GetChild(i).gameObject;
            if(card.GetComponent<CQBCard>().GetType() == type)
                cardcount += 1;
        }
        return cardcount;
    }


    private List<GameObject> GetCards()
    {
        List<GameObject> cards = new List<GameObject>();
        GameObject card;
        for (int i = 0; i < transform.childCount; i++)
        {
            card = transform.GetChild(i).gameObject;
            cards.Add(card);
        }
        return cards;
    }

    public List<string> GetList()
    {
        UpdateList();
        return this.list;
    }

    public void Reorganize()
    {
        GameObject[] cards = GetCards().ToArray();
        GameObject[] cardsOrdered = cards.OrderBy(go => go.name).ToArray();
        for (int i = 0; i < cardsOrdered.Length; i++)
        {
            cardsOrdered[i].transform.SetSiblingIndex(i);
        }
    }

    public void ListTransfer(List<string> list, Transform newparent)
    {
        GameObject card;
        this.list = list;
        foreach(string name in list)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                card = transform.GetChild(i).gameObject;
                if(card.name.CompareTo(name) == 0)
                {
                    card.transform.SetParent(newparent);
                    break;
                }
            }
        }
        UpdateList();
    }

    public void UpdateList()
    {
        List<string> cards = new List<string>();
        GameObject card;
        for (int i = 0; i < transform.childCount; i++)
        {
            card = transform.GetChild(i).gameObject;
            cards.Add(card.name);
        }
        this.list = cards;
    }
}
