using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainGameStart : MonoBehaviour
{
    [Header("References")]
    public GetCards getCards;
    public GameObject mainPlayerBid;
    public DealerMarkerManager dealerMarkerManager;
    public PlayerUIManager playerUIManager;
    public AddCardsToTable addCardsToTable;
    public Bid bidManager;

    private int dealerIndex;
    private Table gameTable;

    private enum GamePhase { Start, DealInitialCards, Flop, Turn, River, BiddingRound, AnnounceWinner }
    private GamePhase currentPhase;

    private List<Card>[] playerHands;
    private List<Card> tableCards;
    private const int cardsPerPlayer = 2;

    void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        if (getCards == null || mainPlayerBid == null || dealerMarkerManager == null || playerUIManager == null || addCardsToTable == null || bidManager == null)
        {
            Debug.LogError("One or more required components are not assigned in the Inspector.");
            return;
        }

        gameTable = CreateTable.InitializeTableWithPlayers(1, 100);
        playerUIManager.InitializeUI(gameTable.players);
        bidManager.InitializeBidding(gameTable, "Start");

        dealerIndex = 0;
        StartGame();
    }

    private void StartGame()
    {
        getCards.ResetDeck();
        tableCards = new List<Card>();
        currentPhase = GamePhase.Start;
        NextPhase();
    }

    private void NextPhase()
    {
        switch (currentPhase)
        {
            case GamePhase.Start:
                SetupDealer();
                break;
            case GamePhase.DealInitialCards:
                DealInitialCards();
                break;
            case GamePhase.BiddingRound:
                StartBids();
                break;
            case GamePhase.Flop:
                DealFlop();
                break;
            case GamePhase.Turn:
                DealTurn();
                break;
            case GamePhase.River:
                DealRiver();
                break;
            case GamePhase.AnnounceWinner:
                AnnounceWinner();
                break;
        }
    }

    void SetupDealer()
    {
        Player currentDealer = gameTable.players[dealerIndex];
        dealerMarkerManager.SetDealerMark(dealerIndex);
        dealerIndex = (dealerIndex + 1) % gameTable.players.Count;

        currentPhase = GamePhase.DealInitialCards;
        NextPhase();
    }

    private void DealInitialCards()
    {
        int numberOfPlayers = gameTable.players.Count;
        playerHands = getCards.DealCardsToPlayers(numberOfPlayers, cardsPerPlayer);
        addCardsToTable.DealPlayerCards(playerHands);

        currentPhase = GamePhase.BiddingRound;
        StartBids();
    }

    private void DealFlop()
    {
        List<Card> flopCards = getCards.DealCards(3);
        tableCards.AddRange(flopCards);
        addCardsToTable.DealFlop(flopCards);

        currentPhase = GamePhase.BiddingRound;
        StartBids();
    }

    private void DealTurn()
    {
        List<Card> turnCard = getCards.DealCards(1);
        tableCards.Add(turnCard[0]);
        addCardsToTable.DealSingleCard(turnCard[0], 3);

        currentPhase = GamePhase.BiddingRound;
        StartBids();
    }

    private void DealRiver()
    {
        List<Card> riverCard = getCards.DealCards(1);
        tableCards.Add(riverCard[0]);
        addCardsToTable.DealSingleCard(riverCard[0], 4);

        currentPhase = GamePhase.BiddingRound;
        StartBids();
    }

    private void StartBids()
    {
        string phaseString = currentPhase.ToString();
        bidManager.InitializeBidding(gameTable, phaseString);
        mainPlayerBid.SetActive(true);
        StartCoroutine(BidDelay());
    }

    private IEnumerator BidDelay()
    {
        yield return new WaitForSeconds(2f);
        mainPlayerBid.SetActive(false);

        switch (currentPhase)
        {
            case GamePhase.BiddingRound:
                if (tableCards.Count == 0) currentPhase = GamePhase.Flop;
                else if (tableCards.Count == 3) currentPhase = GamePhase.Turn;
                else if (tableCards.Count == 4) currentPhase = GamePhase.River;
                else currentPhase = GamePhase.AnnounceWinner;
                break;
        }

        NextPhase();
    }

    private void AnnounceWinner()
    {
        gameTable.DisplayTableInfo();
        bidManager.DisplayBids();
    }
}
