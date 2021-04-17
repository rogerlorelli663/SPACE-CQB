using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private SP_Deck PlayerDeck;
    [SerializeField] private SP_Deck OpponentDeck;
    [SerializeField] private Player_Behavior RoundWinner;
    [SerializeField] private Player_Behavior Opponent;
    [SerializeField] private Player_Behavior Player;
    [SerializeField] private GameStateSingle player;
    [SerializeField] private GameStateSingle opponent;
    [SerializeField] private SP_PlayerCounter playerCounter;
    [SerializeField] private SP_PlayerCounter opponentCounter;
    [SerializeField] private GameObject OutcomeObject;
    [SerializeField] private Outcome Outcome;
    [SerializeField] private AccountCharacteristics Account;
    [SerializeField] private OpponentCollection opponentCollection;
    [SerializeField] private GameObject CurrentTurnToken;
    [SerializeField] private GameObject TurnButton;
    [SerializeField] public bool cardsDelt = false;
    [SerializeField] private int player_cards;
    [SerializeField] private int Num_to_Win = 2;
    [SerializeField] private int Max_Games = 3;
    [SerializeField] private int games_played = 0;
    [SerializeField] private List<GameObject> Boards;
    [SerializeField] private GameObject SelectedBoard;
    [SerializeField] private GameObject DeckSelection;
    [SerializeField] private GameObject PreGameTokenDisplay;
    [SerializeField] private GameObject GameTokenDisplay;



    private void Start()
    {
        Account = GameObject.Find("ActiveAccount").GetComponent<AccountCharacteristics>();
        Outcome = OutcomeObject.GetComponent<Outcome>();
        player_cards = GameObject.Find("SP_DealCards").GetComponent<DrawCardsSingle>().NumberOfCards();

        

        SelectedBoard = SelectBoard();
        SelectedBoard.GetComponent<Board>().Activate();
        GameObject token = SelectedBoard.GetComponent<Board>().ActivateDescriptor();
        token.transform.SetParent(PreGameTokenDisplay.transform);
        token.transform.localScale = new Vector3(1f, 1f, 1f);

        DisableBoard();
    }

    public void NextTurn()
    {
        if (cardsDelt)
        {
            if (opponent.isPlayersTurn())
            {
                if (Opponent.GetHand().GetNumberOfCards() > 0)
                {
                    Opponent.GetComponent<Player_Behavior>().make_a_move();
                }
                else
                {
                    Opponent.GetPlayerState().SetPassing();
                }
                //DetermineNewStates();
            }
            /*else if (player.isPlayersTurn())
            {
                if (player_cards == 0)
                {
                    Player.GetPlayerState().SetPassing();
                    //DetermineNewStates();
                }
                if (player_cards != Player.GetHand().GetNumberOfCards())
                {
                    player_cards = Player.GetHand().GetNumberOfCards();
                    //DetermineNewStates();
                }
            }*/
            else if (player.isPlayersPassing() && opponent.isPlayersPassing())
            {

                playerCounter.Counter();
                opponentCounter.Counter();
                string outcome = DetermineRoundWinner();
                if (Player.GetScore() == Num_to_Win || Opponent.GetScore() == Num_to_Win || games_played == Max_Games)
                {
                    outcome = DetermineGameWinner();
                    cardsDelt = false;
                }
                Outcome.Display(outcome);
                Debug.Log(outcome);
                RollForInitiative();
                Invoke("ResetBoard", 2);
                if(cardsDelt == false)
                {
                    Invoke("MainMenuReturn", 2);
                }
                else if(RoundWinner != null)
                {
                    RoundWinner.GetDeck().DealCards(1, RoundWinner.GetHand().gameObject);
                    if(RoundWinner == Opponent)
                    {
                        Opponent.GetHand().ShowBack();
                    }
                    else
                    {
                        player_cards = Player.GetHand().GetNumberOfCards();
                    }
                }
                return;
                //Application.Quit();
            }
        }
        DetermineNewStates();
        playerCounter.Counter();
        opponentCounter.Counter();
    }

/*    private void Update()
    {
        if(cardsDelt)
        {
            if (opponent.isPlayersTurn())
            {
                Opponent.GetComponent<Player_Behavior>().make_a_move();
                DetermineNewStates(Opponent, Player);
            }
            else if (player.isPlayersTurn())
            {
                if (player_cards == 0)
                {
                    Player.GetPlayerState().SetPassing();
                    DetermineNewStates(Opponent,Player);
                }
                if (player_cards != Player.GetHand().GetNumberOfCards())
                {
                    player_cards = Player.GetHand().GetNumberOfCards();
                    DetermineNewStates(Player, Opponent);
                }
            }
            else if (player.isPlayersPassing() && opponent.isPlayersPassing())
            {

                playerCounter.Counter();
                opponentCounter.Counter();
                string outcome = DetermineRoundWinner();
                Player.ClearField();
                Opponent.ClearField();
                playerCounter.Counter();
                opponentCounter.Counter();

                if (Player.GetScore() == Num_to_Win || Opponent.GetScore() == Num_to_Win)
                {
                    outcome = DetermineGameWinner();
                    cardsDelt = false;
                }

                Outcome.Display(outcome);
                Debug.Log(outcome);
                RollForInitiative();
                return;
                //Application.Quit();
            }
            playerCounter.Counter();
            opponentCounter.Counter();
        }
    }
*/
    private string DetermineRoundWinner()
    {
        int player = playerCounter.GetCurrentPoints();
        int opponent = opponentCounter.GetCurrentPoints();
        RoundTokenPile pile;
        string outcome = "";
        if (player > opponent)
        {
            outcome = "Player Won The Round!";
            pile = Opponent.GetTokens();
            pile.DisableToken();
            Player.IncreaseScore();
            RoundWinner = Player;
            //PlayerDeck.DealCards(1, Player.GetHand().gameObject);
        }
        else if(player < opponent)
        {
            outcome = "Opponent Won The Round!";
            pile = Player.GetTokens();
            pile.DisableToken();
            Opponent.IncreaseScore();
            RoundWinner = Opponent;
            //OpponentDeck.DealCards(1, Opponent.GetHand().gameObject);
        }
        else
        {
            outcome = "DRAW";
            pile = Player.GetTokens();
            pile.DisableToken();
            pile = Opponent.GetTokens();
            pile.DisableToken();
            RoundWinner = null;
        }
        games_played++;
        return outcome;
    }

    private void DetermineNewStates()
    {
        if(Player.GetPlayerState().isPlayersPassing() && !Opponent.GetPlayerState().isPlayersPassing())
        {
            Opponent.GetPlayerState().SetBattleState(1);
            CheckCardCount(Opponent);
            CurrentTurnToken.GetComponent<TurnToken>().ActivateOpponentTurn();
        }
        else if(!Player.GetPlayerState().isPlayersPassing() && Opponent.GetPlayerState().isPlayersPassing())
        {
            Player.GetPlayerState().SetBattleState(1);
            CheckCardCount(Player);
            CurrentTurnToken.GetComponent<TurnToken>().ActivatePlayerTurn();
        }
        else if(!Player.GetPlayerState().isPlayersTurn() && Opponent.GetPlayerState().isPlayersTurn())
        {
            Opponent.GetPlayerState().SetBattleState(0);
            Player.GetPlayerState().SetBattleState(1);
            CheckCardCount(Player);
            CurrentTurnToken.GetComponent<TurnToken>().ActivatePlayerTurn();
        }
        else if(Player.GetPlayerState().isPlayersTurn() && !Opponent.GetPlayerState().isPlayersTurn())
        {
            int current_card_count = Player.GetHand().GetNumberOfCards();
            if (player_cards != current_card_count)
            {
                player_cards = current_card_count;
                Player.GetPlayerState().SetBattleState(0);
                Opponent.GetPlayerState().SetBattleState(1);
                CheckCardCount(Opponent);
                CurrentTurnToken.GetComponent<TurnToken>().ActivateOpponentTurn();
            }
        }
    }

    private string DetermineGameWinner()
    {
        int player = Player.GetScore();
        int opponent = Opponent.GetScore();
        string outcome = "";
        if (player > opponent)
        {
            outcome = "Player Won The Game!";
        }
        else if (player < opponent)
        {
            outcome = "Opponent Won The Game!";
        }
        else
        {
            outcome = "DRAW";
        }
        return outcome;
    }

    private void RollForInitiative()
    {
        int toss = Random.Range(1, 100);
        if (toss % 2 == 0)
        {
            Player.GetPlayerState().SetBattleState(1);
            CurrentTurnToken.GetComponent<TurnToken>().ActivatePlayerTurn();
            Opponent.GetPlayerState().SetBattleState(0);
        }
        else
        {
            Player.GetPlayerState().SetBattleState(0);
            Opponent.GetPlayerState().SetBattleState(1);
            CurrentTurnToken.GetComponent<TurnToken>().ActivateOpponentTurn();
        }
    }

    private void ResetBoard()
    {
        Player.ClearField();
        Opponent.ClearField();
        playerCounter.Counter();
        opponentCounter.Counter();
        Player.ResetPassing();
        Opponent.ResetPassing();
    }

    public void EnableBoard()
    {
        Player.EnablePass();
        Opponent.EnablePass();
        Player.EnableCrystals();
        Opponent.EnableCrystals();
        playerCounter.gameObject.SetActive(true);
        opponentCounter.gameObject.SetActive(true);
        TurnButton.SetActive(true);
        CurrentTurnToken.SetActive(true);
    }

    private void DisableBoard()
    {
        Player.DisablePass();
        Opponent.DisablePass();
        Player.DisableCrystals();
        Opponent.DisableCrystals();
        playerCounter.gameObject.SetActive(false);
        opponentCounter.gameObject.SetActive(false);
        TurnButton.SetActive(false);
        CurrentTurnToken.SetActive(false);
    }

    private void MainMenuReturn()
    {
        SelectedBoard.GetComponent<Board>().Deactivate();
        SceneManager.LoadScene("MainMenu1");
    }

    private GameObject SelectBoard()
    {
        int boundry = Boards.Count * 10;
        int toss = Random.Range(0, boundry);
        int index = toss / 10;
        return Boards[index];
    }

    private void CheckCardCount(Player_Behavior participant)
    {
        if(participant.GetHand().GetNumberOfCards() == 0)
        {
            participant.GetPlayerState().SetPassing();
        }
    }

    public void InitializeSeed()
    {
        int seed = (int)(Time.realtimeSinceStartup * 100);
        Debug.Log("Seed: " + seed);
        Random.InitState(seed);
    }

    public void SetCards(int value)
    {
        RollForInitiative();

        PlayerDeck.CreateNewDeck(Account.GetDeck(value));
        PlayerDeck.ShuffleDeck();

        OpponentDeck.CreateNewDeck(opponentCollection.GetDeck(0));
        OpponentDeck.ShuffleDeck();
        OpponentDeck.ShowBack();
    }

    public void ContinueGamePrep()
    {
        List<GameObject> tokens = DeckSelection.GetComponent<DeckSelection>().GetTokens();
        DeckSelection.GetComponent<DeckSelection>().Deactivate();

        foreach(GameObject token in tokens)
        {
            token.transform.SetParent(GameTokenDisplay.transform,false);
        }
    }

}
