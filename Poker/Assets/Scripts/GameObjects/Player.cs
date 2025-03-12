using UnityEngine;

public class Player
{
    public int id;              // Unique identifier for the player
    public string name;         // Player's name
    public long money;          // Player's total money balance
    public long tableMoney;     // Money that player brings to the table
    public int tableId;         // Identifier for the seat at the table

    // Constructor to initialize a player
    public Player(int id, string name, long money, long tableMoney, int tableId)
    {
        this.id = id;
        this.name = name;
        this.money = money;
        this.tableMoney = tableMoney;
        this.tableId = tableId;
    }

    // Method to adjust player's total money
    public void AdjustMoney(long amount)
    {
        money += amount;
    }

    // Method to adjust player's table money
    public void AdjustTableMoney(long amount)
    {
        tableMoney += amount;
    }

    // Method to represent player details
    public override string ToString()
    {
        return $"Player {id}: {name}, Total Money: {money}, Table Money: {tableMoney}, Table ID: {tableId}";
    }
}
