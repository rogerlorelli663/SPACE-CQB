using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behavior : MonoBehaviour
{
    private enum Type { PLAYER, OPPONENT }

    [SerializeField] private Type type;
    [SerializeField] private GameObject Hand;
    [SerializeField] private GameObject Playfield;
    [SerializeField] private GameObject card;
    [SerializeField] private GameObject discard;
    [SerializeField] private GameStateSingle PlayerState;
    [SerializeField] private GameObject PlayerTokens;
    [SerializeField] private GameObject Pass;
    [SerializeField] private SP_Deck Deck;
    [SerializeField] private int PassChance;
    [SerializeField] private int roll;
    private int rounds_won;
 
    public void Start()
    {
        Hand.SetActive(false);
        Playfield.SetActive(false);
        rounds_won = 0;
    }
    public GameObject make_a_move()
    {
        card = null;
        int seed = (int)(Time.realtimeSinceStartup * 100);
        Random.InitState(seed);
        roll = Random.Range(1, 101);
        if(roll <= PassChance)
        {
            PlayerState.SetPassing();
        }
        else
        {
            List<GameObject> cards = Hand.GetComponent<SP_CardPile>().GetCardsInCardPile();
            int index = Random.Range(0, cards.Count);

            card = cards[index];

            card.transform.SetParent(Playfield.transform, false);
            card.GetComponent<CQBCard>().ActivatePlayable();
        }
        return card;
    }

    public SP_CardPile GetHand()
    {
        return Hand.GetComponent<SP_CardPile>();
    }

    public GameStateSingle GetPlayerState()
    {
        return PlayerState;
    }

    public RoundTokenPile GetTokens()
    {
        RoundTokenPile pile = PlayerTokens.GetComponent<RoundTokenPile>();
        return pile;
    }

    public int GetRoundsWon()
    {
        return this.rounds_won;
    }

    public void IncreaseScore()
    {
        this.rounds_won++;
    }

    public int GetScore()
    {
        return this.rounds_won;
    }
    
    public void ClearField()
    {
        if (Playfield.transform.childCount > 0)
        {
            Playfield.transform.GetChild(0).SetParent(discard.transform, false);
            ClearField();
        }
    }

    public void ResetPassing()
    {
        Pass.GetComponent<PassingButton>().Reset();
    }

    public SP_Deck GetDeck()
    {
        return Deck;
    }

    public void DisablePass()
    {
        Pass.GetComponent<PassingButton>().Disable();
    }

    public void EnablePass()
    {
        Pass.GetComponent<PassingButton>().Enable();
    }

    public void DisableCrystals()
    {
        if (PlayerTokens.activeSelf)
        {
            PlayerTokens.SetActive(false);
        }
    }

    public void EnableCrystals()
    {
        PlayerTokens.SetActive(true);
    }

    public GameObject GetDiscard()
    {
        return discard;
    }

    public GameObject GetPlayField()
    {
        return Playfield;
    }
}
