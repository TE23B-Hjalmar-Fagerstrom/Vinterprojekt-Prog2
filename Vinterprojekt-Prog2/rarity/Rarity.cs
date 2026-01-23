public class Rarity
{
    private string rarity;
    private int rarityLevel;
    private float rarityMultiplier;

    public string theRarity
    {
        get => rarity;
    }

    public int RarityLevel
    {
        get => rarityLevel;
    }

    public float RarityMultiplier
    {
        get => rarityMultiplier;
    }

    protected Rarity() // konstruktor 
    {
        rarityLevel = Random.Shared.Next(1, 101);

        if (rarityLevel <= 35)
        {
            rarity = "Common";
            rarityMultiplier = 1;
        }

        if (rarityLevel <= 65 && rarityLevel > 35)
        {
            rarity = "Uncommon";
            rarityMultiplier = 1.2f;
        }

        if (rarityLevel <= 85 && rarityLevel > 65)
        {
            rarity = "Rare";
            rarityMultiplier = 1.4f;
        }

        if (rarityLevel <= 95 && rarityLevel > 85)
        {
            rarity = "Epic";
            rarityMultiplier = 1.7f;
        }

        if (rarityLevel <= 100 && rarityLevel > 95)
        {
            rarity = "Legendary";
            rarityMultiplier = 2f;
        }
    }
}
