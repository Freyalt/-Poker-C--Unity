using System.Collections.Generic;
using UnityEngine;

public static class CreatePlayers
{
    // Method to create 6 players with default tableMoney initialized
    public static List<Player> InitializePlayers()
    {
        return new List<Player>
        {
            new Player(0, "Jon", 20000, -1, 0),
            new Player(1, "Ana", 20000, -1, 0),
            new Player(2, "Kebabas", 20000, -1, 0),
            new Player(3, "Ivanas", 20000, -1, 0),
            new Player(4, "Mona", 20000, -1, 0),
            new Player(5, "Jula", 20000, -1, 0)
        };
    }
}
