using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hand
{
    private List<Card> playerCards; // List of cards for each player and table cards

    public Hand(List<Card> dealtCards)
    {
        playerCards = dealtCards;
    }

    // Method to get the highest hand ranking for each player
    public long[] GetHighestCardRanks()
    {
        long[] playerRanks = new long[6];

        for (int i = 0; i < 6; i++)
        {
            // Combine each player's two cards with the table cards
            var allCards = new List<Card> { playerCards[i * 2], playerCards[i * 2 + 1] };
            allCards.AddRange(playerCards.Skip(12).Take(5));

            // Rank each player, stopping at the first identified hand
            long score = RoyalFlush(allCards);
            if (score > 0) { playerRanks[i] = score; continue; }

            score = StraightFlush(allCards);
            if (score > 0) { playerRanks[i] = score; continue; }

            score = FourOfAKind(allCards);
            if (score > 0) { playerRanks[i] = score; continue; }

            score = FullHouse(allCards);
            if (score > 0) { playerRanks[i] = score; continue; }

            score = Flush(allCards);
            if (score > 0) { playerRanks[i] = score; continue; }

            score = Straight(allCards);
            if (score > 0) { playerRanks[i] = score; continue; }

            score = ThreeOfAKind(allCards);
            if (score > 0) { playerRanks[i] = score; continue; }

            score = TwoPairs(allCards);
            if (score > 0) { playerRanks[i] = score; continue; }

            score = Pair(allCards);
            if (score > 0) { playerRanks[i] = score; continue; }

            playerRanks[i] = HighCard(allCards); // Assign high card if no other hand is found
        }

        return playerRanks;
    }

    private long HighCard(List<Card> cards) =>
        cards.Max(card => card.GetValue());

    private long Pair(List<Card> cards)
    {
        var pair = cards.GroupBy(card => card.name)
                        .Where(g => g.Count() == 2)
                        .Select(g => g.First().GetValue())
                        .OrderByDescending(value => value)
                        .FirstOrDefault(); // Gets the highest pair value

        return pair > 0 ? 100 + pair : 0;
    }

    private long TwoPairs(List<Card> cards)
    {
        // Find pairs by grouping and selecting values of pairs only
        var pairs = cards.GroupBy(card => card.name)
                         .Where(g => g.Count() == 2)
                         .Select(g => g.First().GetValue())
                         .OrderByDescending(value => value)
                         .ToList();

        // Check if we have exactly two pairs
        if (pairs.Count >= 2)
        {
            int higherPair = pairs[0];
            int lowerPair = pairs[1];
            // Format the score as a string with each pair repeated in two-digit format, followed by a zero
            string scoreString = $"2{higherPair:D2}{higherPair:D2}{lowerPair:D2}{lowerPair:D2}";
            return long.Parse(scoreString);
        }

        return 0; // No two pairs
    }

    private long ThreeOfAKind(List<Card> cards)
    {
        var threeOfAKind = cards.GroupBy(card => card.name)
                                .Where(g => g.Count() == 3)
                                .Select(g => g.First().GetValue())
                                .FirstOrDefault(); // Get the value of the three-of-a-kind

        // Format the score as a string and convert it to long
        return threeOfAKind > 0 ? long.Parse($"3{threeOfAKind:D2}{threeOfAKind:D2}{threeOfAKind:D2}00") : 0;
    }

    private long Straight(List<Card> cards)
    {
        // Sort and get distinct values
        var sortedValues = cards.Select(card => card.GetValue()).Distinct().OrderByDescending(v => v).ToList();

        // Check for consecutive sequences, starting from the highest values
        for (int i = 0; i <= sortedValues.Count - 5; i++)
        {
            var potentialStraight = sortedValues.Skip(i).Take(5).ToList();

            if (potentialStraight.Zip(potentialStraight.Skip(1), (a, b) => a - b).All(d => d == 1))
            {
                // Format the straight as a string starting with "4" for Straight, then descending card values
                string scoreString = "4" + string.Join("", potentialStraight.Select(v => v.ToString("D2")));
                return long.Parse(scoreString);
            }
        }

        // Special case for A-2-3-4-5 (the wheel straight)
        if (sortedValues.Contains(14) && sortedValues.Take(4).SequenceEqual(new List<int> { 5, 4, 3, 2 }))
        {
            return long.Parse("40504030201");
        }

        return 0;
    }

    private long Flush(List<Card> cards)
    {
        // Group by suit and find any suit with 5 or more cards
        var flushCards = cards.GroupBy(card => card.type)
                              .FirstOrDefault(g => g.Count() >= 5)?
                              .Select(card => card.GetValue())
                              .OrderByDescending(v => v)
                              .ToList();

        // If a flush is found, format the score
        if (flushCards != null)
        {
            // Format the score with "5" prefix followed by the top 5 flush card values
            string scoreString = "5" + string.Join("", flushCards.Take(5).Select(v => v.ToString("D2")));
            return long.Parse(scoreString);
        }

        return 0;
    }

    private long FullHouse(List<Card> cards)
    {
        // Find the three-of-a-kind value
        var threeOfAKind = cards.GroupBy(card => card.name)
                                .Where(g => g.Count() == 3)
                                .Select(g => g.First().GetValue())
                                .FirstOrDefault();

        // Find the pair value, excluding the three-of-a-kind value
        var pair = cards.GroupBy(card => card.name)
                        .Where(g => g.Count() == 2 && g.First().GetValue() != threeOfAKind)
                        .Select(g => g.First().GetValue())
                        .FirstOrDefault();

        // Ensure both three-of-a-kind and pair exist, then format the score as a string
        if (threeOfAKind > 0 && pair > 0)
        {
            string scoreString = $"6{threeOfAKind:D2}{threeOfAKind:D2}{threeOfAKind:D2}{pair:D2}{pair:D2}";
            return long.Parse(scoreString); // Convert the formatted string to long
        }

        return 0;
    }

    private long FourOfAKind(List<Card> cards)
    {
        // Identify the four-of-a-kind value
        var fourOfAKind = cards.GroupBy(card => card.name)
                               .Where(g => g.Count() == 4)
                               .Select(g => g.First().GetValue())
                               .FirstOrDefault();

        // Identify the kicker (highest remaining card)
        var kicker = cards.Where(card => card.GetValue() != fourOfAKind)
                          .Select(card => card.GetValue())
                          .DefaultIfEmpty(0) // Default to 0 if no kicker is available
                          .Max();

        // Ensure both four-of-a-kind and kicker exist, then format the score as a string
        if (fourOfAKind > 0)
        {
            // Correctly format the score with the kicker appearing only once
            string scoreString = $"7{fourOfAKind:D2}{fourOfAKind:D2}{fourOfAKind:D2}{fourOfAKind:D2}00";
            return long.Parse(scoreString); // Convert the formatted string to long
        }

        return 0;
    }

    private long StraightFlush(List<Card> cards)
    {
        // Group by suit and find any suit with 5 or more cards
        var flushGroup = cards.GroupBy(card => card.type)
                              .FirstOrDefault(g => g.Count() >= 5);

        if (flushGroup != null)
        {
            // Get values of cards in the flush group and sort them in descending order
            var flushCards = flushGroup.Select(card => card.GetValue())
                                       .Distinct()
                                       .OrderByDescending(v => v)
                                       .ToList();

            // Check for a straight within these flush cards, from highest to lowest
            for (int i = 0; i <= flushCards.Count - 5; i++)
            {
                var potentialStraight = flushCards.Skip(i).Take(5).ToList();

                if (potentialStraight.Zip(potentialStraight.Skip(1), (a, b) => a - b).All(d => d == 1))
                {
                    // Format the score string with "8" for Straight Flush, followed by each card in descending order
                    string scoreString = "8" + string.Join("", potentialStraight.Select(v => v.ToString("D2")));
                    return long.Parse(scoreString);
                }
            }

            // Special case for A-2-3-4-5 (low straight flush)
            if (flushCards.Contains(14) && flushCards.Take(4).SequenceEqual(new List<int> { 5, 4, 3, 2 }))
            {
                return long.Parse("80504030201");
            }
        }

        return 0;
    }

    private long RoyalFlush(List<Card> cards)
    {
        // Group cards by suit and filter for any suit with 5 or more cards
        var flushGroup = cards.GroupBy(card => card.type)
                              .FirstOrDefault(g => g.Count() >= 5);

        // Check if the flush contains 10, J, Q, K, A
        if (flushGroup != null)
        {
            var flushCards = flushGroup.Select(card => card.GetValue()).ToHashSet();
            var royalValues = new HashSet<int> { 10, 11, 12, 13, 14 };

            // Confirm all royal cards are present in the flush
            if (royalValues.IsSubsetOf(flushCards))
            {
                return 90000000000 + 1413121110; // Unique high score for Royal Flush
            }
        }

        return 0;
    }


}
