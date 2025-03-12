using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PlayerUIManager : MonoBehaviour
{
    [Header("Name Text Fields")]
    public TextMeshProUGUI[] nameFields; // Array for player name fields (P1Name, P2Name, ...)

    [Header("Table Money Text Fields")]
    public TextMeshProUGUI[] tableMoneyFields; // Array for player table money fields (P1Coins, P2Coins, ...)

    private List<Player> players;

    public void InitializeUI(List<Player> players)
    {
        this.players = players;

        if (players.Count > nameFields.Length || players.Count > tableMoneyFields.Length)
        {
            Debug.LogError("Not enough UI fields for the number of players.");
            return;
        }

        // Update each UI element based on player data
        for (int i = 0; i < players.Count; i++)
        {
            nameFields[i].text = players[i].name;  // Set the player name in the UI
            tableMoneyFields[i].text = players[i].tableMoney.ToString();  // Set the player's table money in the UI
        }
    }

    public void UpdatePlayerTableMoney(int playerIndex, long newTableMoneyAmount)
    {
        if (playerIndex >= 0 && playerIndex < tableMoneyFields.Length)
        {
            tableMoneyFields[playerIndex].text = newTableMoneyAmount.ToString();
            players[playerIndex].tableMoney = newTableMoneyAmount; // Update the player's table money amount
        }
        else
        {
            Debug.LogError("Player index is out of bounds.");
        }
    }
}
