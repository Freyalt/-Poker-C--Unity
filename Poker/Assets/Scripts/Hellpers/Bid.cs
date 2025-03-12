using System.Collections.Generic;
using UnityEngine;

public class Bid : MonoBehaviour
{
    private Table gameTable;
    private string currentPhase;
    public List<BidEntry> currentBids;
    public int minimumBid;

    // Initialize the bidding system
    public void InitializeBidding(Table table, string phase)
    {
        this.gameTable = table;
        this.currentPhase = phase;
        minimumBid = gameTable.bid;

        currentBids = new List<BidEntry>();
        Debug.Log($"Bidding initialized for phase: {phase}");
        StartBiddingRound();
    }

    private void StartBiddingRound()
    {
        for (int i = 0; i < gameTable.players.Count; i++)
        {
            int bidAmount = CalculateBidAmount(i);
            PlaceBid(gameTable.players[i].id, bidAmount);
        }
    }

    private int CalculateBidAmount(int playerIndex)
    {
        if (playerIndex == 0) return minimumBid / 2;
        if (playerIndex == 1) return minimumBid;
        return minimumBid;
    }

    private void PlaceBid(int playerID, int amount)
    {
        currentBids.Add(new BidEntry(playerID, amount));
        Debug.Log($"Player {playerID} placed a bid of {amount}");
    }

    public void DisplayBids()
    {
        foreach (var bid in currentBids)
        {
            Debug.Log($"Player {bid.playerID} bid: {bid.amount}");
        }
    }
}

[System.Serializable]
public class BidEntry
{
    public int playerID;
    public int amount;

    public BidEntry(int playerID, int amount)
    {
        this.playerID = playerID;
        this.amount = amount;
    }
}
