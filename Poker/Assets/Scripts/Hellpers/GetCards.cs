using System.Collections.Generic;
using UnityEngine;

public class GetCards : MonoBehaviour
{
    private List<Card> deck;

    private void Awake()
    {
        // Initialize the deck from the card database
        ResetDeck();
    }

    public void ResetDeck()
    {
        // Re-initialize the deck from the card database
        deck = new List<Card>(CardDataBase.cardList);
        ShuffleDeck();
    }

    public void ShuffleDeck()
    {
        // Fisher-Yates shuffle algorithm
        for (int i = deck.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Card temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    public List<Card> DealCards(int numberOfCards)
    {
        // Check if there are enough cards to deal
        if (numberOfCards > deck.Count)
        {
            Debug.LogError("Not enough cards in the deck to deal the requested number of cards.");
            return null;
        }

        List<Card> dealtCards = new List<Card>();
        for (int i = 0; i < numberOfCards; i++)
        {
            dealtCards.Add(DrawCard());
        }
        return dealtCards;
    }

    private Card DrawCard()
    {
        if (deck.Count == 0)
        {
            Debug.LogError("The deck is empty. Cannot draw more cards.");
            return null;
        }
        Card drawnCard = deck[0];
        deck.RemoveAt(0);
        return drawnCard;
    }

    public List<Card>[] DealCardsToPlayers(int numberOfPlayers, int cardsPerPlayer)
    {
        int totalCardsNeeded = numberOfPlayers * cardsPerPlayer;
        if (totalCardsNeeded > deck.Count)
        {
            Debug.LogError("Not enough cards in the deck to deal to all players.");
            return null;
        }

        List<Card>[] playerHands = new List<Card>[numberOfPlayers];
        for (int i = 0; i < numberOfPlayers; i++)
        {
            playerHands[i] = new List<Card>();
            for (int j = 0; j < cardsPerPlayer; j++)
            {
                playerHands[i].Add(DrawCard());
            }
        }
        return playerHands;
    }
}
