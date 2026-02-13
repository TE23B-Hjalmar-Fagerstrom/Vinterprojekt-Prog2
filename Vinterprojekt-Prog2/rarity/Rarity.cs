public class Rarity
{
    private string rarity;
    private int rarityLevel;
    private float rarityMultiplier;
    protected int randomMin = 1;
    protected int randomMax = 101;

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
        rarityLevel = Random.Shared.Next(randomMin, randomMax);

        if (rarityLevel <= 35)
        {
            rarity = "Common";
            rarityMultiplier = 1;
        }

        else if (rarityLevel <= 65 && rarityLevel > 35)
        {
            rarity = "Uncommon";
            rarityMultiplier = 1.2f;
        }

        else if (rarityLevel <= 85 && rarityLevel > 65)
        {
            rarity = "Rare";
            rarityMultiplier = 1.4f;
        }

        else if (rarityLevel <= 95 && rarityLevel > 85)
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
