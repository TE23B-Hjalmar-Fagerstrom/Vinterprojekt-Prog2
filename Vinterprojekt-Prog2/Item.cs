public class Item : Rarity
{
    private string name;
    private double buyCost = 20;
    private double sellCost = 10;

    public string Name
    {
        get => name;

        set { name = $"{theRarity} " + value; }
    }

    public double BuyCost
    {
        get => buyCost;

        set
        {
            buyCost += (value + RarityMultiplier) * RarityMultiplier;
            buyCost = Math.Round(buyCost);
        }
    }

    public double SellCost
    {
        get => sellCost;

        set
        {
            sellCost += (value + RarityMultiplier) * RarityMultiplier;
            sellCost = Math.Round(sellCost);
        }
    }
}
