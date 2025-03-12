using UnityEngine;
using System.Collections.Generic;

public static class CreateTable
{
    // Method to create a table with players, deduct cash, and set minimum bid
    public static Table InitializeTableWithPlayers(int tableID, int minBid)
    {
        // Initialize players using CreatePlayers helper and convert to List<Player>
        List<Player> players = new List<Player>(CreatePlayers.InitializePlayers());

        // Create a new table with the specified minimum bid
        Table table = new Table(tableID, players, minBid);

        // Deduct 2,000 from each player's balance and assign it as their table money
        foreach (Player player in players)
        {
            if (player.money >= 2000)
            {
                player.AdjustMoney(-2000);         // Deduct 2,000 from player’s balance
                player.AdjustTableMoney(2000);     // Set player’s tableMoney to 2,000
            }
            else
            {
                Debug.LogWarning($"Player {player.name} doesn't have enough money to join the table.");
                player.AdjustTableMoney(player.money);  // Assign remaining money if less than 2,000
                player.AdjustMoney(-player.money);      // Set player’s main money to zero
            }
        }

        return table;
    }
}
