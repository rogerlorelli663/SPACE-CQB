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
    [SerializeField] private GameObject playedCard;
    [SerializeField] private Player_Behavior CurrentPlayer;
    [SerializeField] private Player_Behavior NextPlayer;



    private void Start()
    {
        Account = GameObject.Find("ActiveAccount").GetComponent<AccountCharacteristics>();
        Outcome = OutcomeObject.GetComponent<Outcome>();
        player_cards = GameObject.Find("SP_DealCards").GetComponent<DrawCardsSingle>().NumberOfCards();

        

        SelectedBoard = SelectBoard();
        SelectedBoard.GetComponent<Board>().Activate();
        SelectedBoard.GetComponent<Board>().PrintModifier();
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
                    playedCard = Opponent.GetComponent<Player_Behavior>().make_a_move();

                }
                else
                {
                    Opponent.GetPlayerState().SetPassing();
                }
            }
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
                if (cardsDelt == false)
                {
                    Invoke("MainMenuReturn", 2);
                }
                else if (RoundWinner != null)
                {
                    RoundWinner.GetDeck().DealCards(1, RoundWinner.GetHand().gameObject);
                    ApplyModifiers(RoundWinner);
                    if (RoundWinner == Opponent)
                    {
                        Opponent.GetHand().ShowBack();
                    }
                    else
                    {
                        player_cards = Player.GetHand().GetNumberOfCards();
                    }
                }
                return;
            }
        }
        if(playedCard != null)
        {
            ApplyCardModifiers(playedCard, CurrentPlayer, NextPlayer);
            playedCard = null;
        }
        DetermineNewStates();
        playerCounter.Counter();
        opponentCounter.Counter();
    }

    private void Update()
    {
        if (cardsDelt)
        {
            if (opponent.isPlayersTurn())
            {
                if (Opponent.GetHand().GetNumberOfCards() > 0)
                {
                    playedCard = Opponent.GetComponent<Player_Behavior>().make_a_move();

                }
                else
                {
                    Opponent.GetPlayerState().SetPassing();
                }
            }
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
                if (cardsDelt == false)
                {
                    Invoke("MainMenuReturn", 2);
                }
                else if (RoundWinner != null)
                {
                    RoundWinner.GetDeck().DealCards(1, RoundWinner.GetHand().gameObject);
                    ApplyModifiers(RoundWinner);
                    if (RoundWinner == Opponent)
                    {
                        Opponent.GetHand().ShowBack();
                    }
                    else
                    {
                        player_cards = Player.GetHand().GetNumberOfCards();
                    }
                }
                return;
            }
        }
        if (playedCard != null)
        {
            ApplyCardModifiers(playedCard, CurrentPlayer, NextPlayer);
            playedCard = null;
        }
        DetermineNewStates();
        playerCounter.Counter();
        opponentCounter.Counter();
    }

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
            CurrentPlayer = Opponent;
            NextPlayer = Player;
        }
        else if(!Player.GetPlayerState().isPlayersPassing() && Opponent.GetPlayerState().isPlayersPassing())
        {
            Player.GetPlayerState().SetBattleState(1);
            CheckCardCount(Player);
            CurrentTurnToken.GetComponent<TurnToken>().ActivatePlayerTurn();
            CurrentPlayer = Player;
            NextPlayer = Opponent;
        }
        else if(!Player.GetPlayerState().isPlayersTurn() && Opponent.GetPlayerState().isPlayersTurn())
        {
            Opponent.GetPlayerState().SetBattleState(0);
            Player.GetPlayerState().SetBattleState(1);
            CheckCardCount(Player);
            CurrentTurnToken.GetComponent<TurnToken>().ActivatePlayerTurn();
            CurrentPlayer = Player;
            NextPlayer = Opponent;

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
                CurrentPlayer = Opponent;
                NextPlayer = Player;
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
            CurrentPlayer = Player;
            NextPlayer = Opponent;
            Opponent.GetPlayerState().SetBattleState(0);
        }
        else
        {
            Player.GetPlayerState().SetBattleState(0);
            Opponent.GetPlayerState().SetBattleState(1);
            CurrentTurnToken.GetComponent<TurnToken>().ActivateOpponentTurn();
            CurrentPlayer = Opponent;
            NextPlayer = Player;
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
        Debug.Log("Applying Modifier to Player");
        ApplyModifiers(Player);
        Debug.Log("Applying Modifier to Opponent");
        ApplyModifiers(Opponent);
    }

    public void ProcessEnviroModifier(GameObject card)
    {
        Modifiers.EnviroModifier enviroModifier = SelectedBoard.GetComponent<Board>().GetModifier();
        
        switch(enviroModifier)
        {
            case Modifiers.EnviroModifier.Blackhole:
                ModifierCharacteristics.Blackhole(card);
                card.GetComponent<CQBCard>().SetEnviro();
                break;
            case Modifiers.EnviroModifier.PowerCap:
                ModifierCharacteristics.PowerCap(card);
                card.GetComponent<CQBCard>().SetEnviro();
                break;
            case Modifiers.EnviroModifier.SuperNova:
                ModifierCharacteristics.SuperNova(card);
                card.GetComponent<CQBCard>().SetEnviro();
                break;
            case Modifiers.EnviroModifier.ECM:
                ModifierCharacteristics.ECM(card);
                card.GetComponent<CQBCard>().SetEnviro();
                break;
        }
    }

    private void ApplyModifiers(Player_Behavior combatant)
    {
        List<GameObject> cards = combatant.GetHand().GetCardsInCardPile();
        foreach (GameObject card in cards)
        {
            if(!card.GetComponent<CQBCard>().Is_Modified_Enviro())
                ProcessEnviroModifier(card);
        }
    }

    private void ApplyCardModifiers(GameObject card, Player_Behavior player, Player_Behavior opponent)
    {
        Modifiers.EnviroModifier enviroModifier = SelectedBoard.GetComponent<Board>().GetModifier();
        if (enviroModifier == Modifiers.EnviroModifier.NoCapital && card.GetComponent<CQBCard>().GetUnitType() == CQBCard.UnitType.CAPITAL)
        {
            card.transform.SetParent(player.GetDiscard().transform);
        }
        else
        {
            if (card.GetComponent<CQBCard>().HasAbility() && !card.GetComponent<CQBCard>().Suppressed())
            {
                Modifiers.CardModifiers ability = card.GetComponent<CQBCard>().GetAbility();
                switch (ability)
                {
                    case Modifiers.CardModifiers.BattleBuddiesAssault:
                        ModifierCharacteristics.BattleBuddiesAssault(card, player);
                        break;
                    case Modifiers.CardModifiers.BattleBuddiesIon:
                        ModifierCharacteristics.BattleBuddiesIon(card, player);
                        break;
                    case Modifiers.CardModifiers.HunterPack:
                        break;
                    case Modifiers.CardModifiers.Anti_Strike:
                        break;
                    case Modifiers.CardModifiers.Bomber:
                        break;
                    case Modifiers.CardModifiers.Quick_Deploy:
                        break;
                }
            }
        }
        
    }

    public void SetPlayedCard(GameObject card)
    {
        playedCard = card;
    }
}
