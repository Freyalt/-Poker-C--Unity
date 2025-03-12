using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();

    private void Awake()
    {
        cardList.Add(new Card("2", "C"));
        cardList.Add(new Card("3", "C"));
        cardList.Add(new Card("4", "C"));
        cardList.Add(new Card("5", "C"));
        cardList.Add(new Card("6", "C"));
        cardList.Add(new Card("7", "C"));
        cardList.Add(new Card("8", "C"));
        cardList.Add(new Card("9", "C"));
        cardList.Add(new Card("10", "C"));
        cardList.Add(new Card("J", "C"));
        cardList.Add(new Card("Q", "C"));
        cardList.Add(new Card("K", "C"));
        cardList.Add(new Card("A", "C"));

        cardList.Add(new Card("2", "B"));
        cardList.Add(new Card("3", "B"));
        cardList.Add(new Card("4", "B"));
        cardList.Add(new Card("5", "B"));
        cardList.Add(new Card("6", "B"));
        cardList.Add(new Card("7", "B"));
        cardList.Add(new Card("8", "B"));
        cardList.Add(new Card("9", "B"));
        cardList.Add(new Card("10", "B"));
        cardList.Add(new Card("J", "B"));
        cardList.Add(new Card("Q", "B"));
        cardList.Add(new Card("K", "B"));
        cardList.Add(new Card("A", "B"));

        cardList.Add(new Card("2", "G"));
        cardList.Add(new Card("3", "G"));
        cardList.Add(new Card("4", "G"));
        cardList.Add(new Card("5", "G"));
        cardList.Add(new Card("6", "G"));
        cardList.Add(new Card("7", "G"));
        cardList.Add(new Card("8", "G"));
        cardList.Add(new Card("9", "G"));
        cardList.Add(new Card("10", "G"));
        cardList.Add(new Card("J", "G"));
        cardList.Add(new Card("Q", "G"));
        cardList.Add(new Card("K", "G"));
        cardList.Add(new Card("A", "G"));

        cardList.Add(new Card("2", "V"));
        cardList.Add(new Card("3", "V"));
        cardList.Add(new Card("4", "V"));
        cardList.Add(new Card("5", "V"));
        cardList.Add(new Card("6", "V"));
        cardList.Add(new Card("7", "V"));
        cardList.Add(new Card("8", "V"));
        cardList.Add(new Card("9", "V"));
        cardList.Add(new Card("10", "V"));
        cardList.Add(new Card("J", "V"));
        cardList.Add(new Card("Q", "V"));
        cardList.Add(new Card("K", "V"));
        cardList.Add(new Card("A", "V"));
    }
}
