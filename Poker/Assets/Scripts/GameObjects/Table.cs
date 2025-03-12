using System.Collections.Generic;
using UnityEngine;

public class Table
{
    public int tableID;                      // Unique identifier for the table
    public List<Player> players;             // List of Player objects for flexibility
    public int bid;                          // Minimum bid amount at the table

    // Seat assignments for players
    public int seat1;
    public int seat2;
    public int seat3;
    public int seat4;
    public int seat5;
    public int seat6;

    // Constructor to initialize the table with an ID, player list, and bid amount
    public Table(int tableID, List<Player> players, int bid)
    {
        this.tableID = tableID;
        this.players = players;
        this.bid = bid;

        // Assign initial money for each player from their balance
        InitializeTableMoney();

        // Initialize seats with player IDs or default to -1 if unassigned
        AssignSeats();
    }

    // Initialize each player's table money from their starting balance
    private void InitializeTableMoney()
    {
        foreach (var player in players)
        {
            player.tableMoney = 0; // Set initial table money to 0 for each player
        }
    }

    // Assign players to seats; for this example, we assign the first 6 players to seats
    private void AssignSeats()
    {
        seat1 = players.Count > 0 ? players[0].id : -1;
        seat2 = players.Count > 1 ? players[1].id : -1;
        seat3 = players.Count > 2 ? players[2].id : -1;
        seat4 = players.Count > 3 ? players[3].id : -1;
        seat5 = players.Count > 4 ? players[4].id : -1;
        seat6 = players.Count > 5 ? players[5].id : -1;
    }

    // Method for a player to place a bet
    public void PlaceBet(int playerIndex, long amount)
    {
        if (playerIndex < 0 || playerIndex >= players.Count)
        {
            Debug.LogError("Invalid player index.");
            return;
        }

        Player player = players[playerIndex];

        if (amount > player.money)
        {
            Debug.LogError($"{player.name} does not have enough money to place this bet.");
            return;
        }

        // Subtract the bet from the player's money and add to the player's tableMoney
        player.AdjustMoney(-amount);
        player.AdjustTableMoney(amount);

        Debug.Log($"{player.name} placed a bet of {amount}. Remaining money: {player.money}. Money on table: {player.tableMoney}");
    }

    // Method to display table and player information for debugging
    public void DisplayTableInfo()
    {
        Debug.Log($"Table ID: {tableID}, Minimum Bid Amount: {bid}");
        Debug.Log($"Players at Table:");
        foreach (var player in players)
        {
            Debug.Log($"{player.name} (ID: {player.id}) - Money on Table: {player.tableMoney}, Total Money: {player.money}");
        }

        Debug.Log($"Seats:");
        Debug.Log($"Seat 1: Player ID {seat1}");
        Debug.Log($"Seat 2: Player ID {seat2}");
        Debug.Log($"Seat 3: Player ID {seat3}");
        Debug.Log($"Seat 4: Player ID {seat4}");
        Debug.Log($"Seat 5: Player ID {seat5}");
        Debug.Log($"Seat 6: Player ID {seat6}");
    }
}
