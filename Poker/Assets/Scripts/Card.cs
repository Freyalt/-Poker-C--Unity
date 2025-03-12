using UnityEngine;

[System.Serializable]
public class Card
{
    public string name; // Card rank (e.g., "2", "A", "K")
    public string type; // Suit (e.g., "G" for giles, "B" for bugnai, "V" for vynai, "C" for cirvai)

    public Card() { }

    public Card(string name, string type)
    {
        this.name = name;
        this.type = type;
    }

    // Method to get the numeric value of the card for ranking comparisons
    public int GetValue()
    {
        // Parse card rank as integer for "2" - "10"
        if (int.TryParse(name, out int value)) return value;

        // Map face cards to their appropriate values
        switch (name)
        {
            case "J": return 11;
            case "Q": return 12;
            case "K": return 13;
            case "A": return 14;
            default: return 0; // Return 0 if card name is invalid or unrecognized
        }
    }
}
