using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class CardDeal : MonoBehaviour
{
    public GetCards getCards;
    public GameObject cardPrefabB;
    public GameObject cardPrefabC;
    public GameObject cardPrefabG;
    public GameObject cardPrefabV;
    public Transform[] cardPositions;
    public Button redealButton; // Reference to the Redeal button

    void Start()
    {
        if (getCards == null || cardPositions.Length < 17)
        {
            Debug.LogError("GetCards script or card positions are not assigned or insufficient.");
            return;
        }

        redealButton.onClick.AddListener(Redeal); // Set up the button's click event

        Redeal(); // Initial deal on start
    }

    public void Redeal()
    {
        // Clear any existing card display
        foreach (Transform position in cardPositions)
        {
            foreach (Transform child in position)
            {
                Destroy(child.gameObject); // Remove previous cards
            }
        }

        getCards.ResetDeck();
        List<Card> dealtCards = getCards.DealCards(17);

        if (dealtCards != null)
        {
            DisplayDealtCards(dealtCards);
            GetWinner(dealtCards);
        }
        else
        {
            Debug.LogError("Failed to deal cards.");
        }
    }

    private void DisplayDealtCards(List<Card> dealtCards)
    {
        for (int i = 0; i < dealtCards.Count; i++)
        {
            CreateCardUI(dealtCards[i], cardPositions[i]);
        }
    }

    private void CreateCardUI(Card card, Transform position)
    {
        GameObject selectedPrefab = GetPrefabByType(card.type);
        if (selectedPrefab == null)
        {
            Debug.LogError($"No prefab found for card type: {card.type}");
            return;
        }

        GameObject cardUI = Instantiate(selectedPrefab, position.position, Quaternion.identity, position);
        Canvas canvas = cardUI.GetComponentInChildren<Canvas>();
        if (canvas != null)
        {
            canvas.overrideSorting = true;
            canvas.sortingOrder = 4;

            if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            {
                canvas.worldCamera = Camera.main;
            }
        }
        else
        {
            Debug.LogError("Canvas component not found in card prefab.");
        }

        TextMeshProUGUI cardNameText = cardUI.GetComponentInChildren<TextMeshProUGUI>();
        if (cardNameText != null)
        {
            cardNameText.text = card.name;
            Debug.Log($"Card name set to: {card.name}");
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not found in card prefab.");
        }
    }

    private GameObject GetPrefabByType(string type)
    {
        switch (type)
        {
            case "B":
                return cardPrefabB;
            case "C":
                return cardPrefabC;
            case "G":
                return cardPrefabG;
            case "V":
                return cardPrefabV;
            default:
                Debug.LogError($"Invalid card type: {type}");
                return null;
        }
    }

    private void GetWinner(List<Card> dealtCards)
    {
        Hand handEvaluator = new Hand(dealtCards);
        long[] playerRanks = handEvaluator.GetHighestCardRanks();

        for (int i = 0; i < playerRanks.Length; i++)
        {
            Debug.Log($"Player {i + 1} hand ranking: {playerRanks[i]}");
        }

        long highestRank = playerRanks.Max();
        List<int> winners = new List<int>();

        for (int i = 0; i < playerRanks.Length; i++)
        {
            if (playerRanks[i] == highestRank)
            {
                winners.Add(i + 1);
            }
        }

        if (winners.Count > 1)
        {
            Debug.Log("It's a tie! Players with the highest rank are: " + string.Join(", ", winners));
        }
        else
        {
            Debug.Log($"Player {winners[0]} wins with the highest rank: {highestRank}");
        }
    }

    List<Card> DealCardsManually()
    {
        return new List<Card>
        {
            // Player 1 cards (Two Pairs: K-K and Q-Q)
            CardDataBase.cardList.Find(card => card.name == "10" && card.type == "G"),
            CardDataBase.cardList.Find(card => card.name == "9" && card.type == "G"),

            // Player 2 cards (Three of a Kind: A-A-A)
            CardDataBase.cardList.Find(card => card.name == "10" && card.type == "G"),
            CardDataBase.cardList.Find(card => card.name == "A" && card.type == "G"),

            // Player 3 cards (High Card: 3 and 4)
            CardDataBase.cardList.Find(card => card.name == "7" && card.type == "G"),
            CardDataBase.cardList.Find(card => card.name == "4" && card.type == "G"),

            // Player 4 cards (Random)
            CardDataBase.cardList.Find(card => card.name == "K" && card.type == "C"),
            CardDataBase.cardList.Find(card => card.name == "10" && card.type == "C"),

            // Player 5 cards (Random)
            CardDataBase.cardList.Find(card => card.name == "A" && card.type == "C"),
            CardDataBase.cardList.Find(card => card.name == "A" && card.type == "C"),

            // Player 6 cards (Random)
            CardDataBase.cardList.Find(card => card.name == "2" && card.type == "V"),
            CardDataBase.cardList.Find(card => card.name == "2" && card.type == "V"),

            // Table cards to complete the board for testing
            CardDataBase.cardList.Find(card => card.name == "2" && card.type == "C"),
            CardDataBase.cardList.Find(card => card.name == "Q" && card.type == "G"),
            CardDataBase.cardList.Find(card => card.name == "K" && card.type == "G"),
            CardDataBase.cardList.Find(card => card.name == "2" && card.type == "C"),
            CardDataBase.cardList.Find(card => card.name == "J" && card.type == "G")
        };
    }
}
