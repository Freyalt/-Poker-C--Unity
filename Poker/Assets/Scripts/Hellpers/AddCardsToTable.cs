using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddCardsToTable : MonoBehaviour
{
    [Header("Card Prefabs")]
    public GameObject cardPrefabB;
    public GameObject cardPrefabC;
    public GameObject cardPrefabG;
    public GameObject cardPrefabV;

    [Header("Player Card Positions")]
    public Transform[] playerCardPositions; // Positions for P1Card1, P1Card2, etc.

    [Header("Table Card Positions")]
    public Transform[] tableCardPositions; // Positions for T1, T2, T3, T4, T5

    private List<Card>[] playerHands;
    private List<Card> tableCards;

    // Method to deal 2 cards to each player
    public void DealPlayerCards(List<Card>[] dealtCards)
    {
        playerHands = dealtCards;

        for (int i = 0; i < playerHands.Length; i++)
        {
            for (int j = 0; j < playerHands[i].Count; j++)
            {
                Transform cardPosition = playerCardPositions[i * 2 + j];
                CreateCardUI(playerHands[i][j], cardPosition);
            }
        }
    }

    // Method to deal 3 cards to the table (flop)
    public void DealFlop(List<Card> cards)
    {
        tableCards = cards;

        for (int i = 0; i < 3; i++)
        {
            CreateCardUI(tableCards[i], tableCardPositions[i]);
        }
    }

    // Method to deal 1 card to the table (turn or river)
    public void DealSingleCard(Card card, int positionIndex)
    {
        if (positionIndex < tableCardPositions.Length)
        {
            CreateCardUI(card, tableCardPositions[positionIndex]);
        }
        else
        {
            Debug.LogError("Position index out of range for table cards.");
        }
    }

    //instantiate a card prefab and set its value
    private void CreateCardUI(Card card, Transform position)
    {
        GameObject cardPrefab = GetPrefabByType(card.type);
        if (cardPrefab == null)
        {
            Debug.LogError($"No card prefab found for type: {card.type}");
            return;
        }

        GameObject cardObject = Instantiate(cardPrefab, position.position, Quaternion.identity, position);
        if (cardObject == null)
        {
            Debug.LogError("Failed to instantiate card prefab.");
            return;
        }

        // Ensure Canvas and TextMeshProUGUI are set correctly
        Canvas cardCanvas = cardObject.GetComponentInChildren<Canvas>();
        if (cardCanvas != null)
        {
            cardCanvas.overrideSorting = true;
            cardCanvas.sortingOrder = 5;
            cardCanvas.worldCamera = Camera.main;
        }

        // Set the card text
        TextMeshProUGUI textField = cardObject.GetComponentInChildren<TextMeshProUGUI>();
        if (textField != null)
        {
            textField.text = card.name;
            textField.enabled = true; // Ensure text is enabled
            textField.ForceMeshUpdate(); // Force update for TextMeshPro
            Debug.Log($"Card text set to: {card.name} for type {card.type}");
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not found on card prefab.");
        }
    }

    // Method to get the correct card prefab based on the card type
    private GameObject GetPrefabByType(string type)
    {
        switch (type)
        {
            case "B": return cardPrefabB;
            case "C": return cardPrefabC;
            case "G": return cardPrefabG;
            case "V": return cardPrefabV;
            default: return null;
        }
    }
}
