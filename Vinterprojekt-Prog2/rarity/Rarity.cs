public class Rarity
{
    private string rarity;
    private int rarityLevel;
    private float rarityMultiplier;
    private int randomMin = 1;
    private int randomMax = 101;
    protected static bool firstWeapon = true;

    public string theRarity
    {
        get => rarity;

        protected set
        {
            rarity = value;
        }
    }

    public int RarityLevel
    {
        get => rarityLevel;
    }

    public float RarityMultiplier
    {
        get => rarityMultiplier;

        protected set
        {
            rarityMultiplier = value;
        }
    }

    public Rarity() // konstruktor 
    {
        rarityLevel = Random.Shared.Next(randomMin, randomMax);

        if (rarityLevel <= 45 || firstWeapon == true)
        {
            rarity = "(Vanlig)";
            rarityMultiplier = 1;
        }

        else if (rarityLevel <= 75 && rarityLevel > 45)
        {
            rarity = "(Ovanlig)";
            rarityMultiplier = 1.2f;
        }

        else if (rarityLevel <= 94 && rarityLevel > 75)
        {
            rarity = "(Sällsynt)";
            rarityMultiplier = 1.4f;
        }

        else if (rarityLevel <= 99 && rarityLevel > 94)
        {
            rarity = "(Episk)";
            rarityMultiplier = 1.7f;
        }

        else
        {
            rarity = "(Legendarisk)";
            rarityMultiplier = 2f;
        }
    }
}
