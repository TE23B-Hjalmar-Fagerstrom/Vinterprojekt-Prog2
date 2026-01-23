public class Item : Rarity
{
    private string name;
    private double buyCost = 20;
    private double sellCost = 10;

    public Item()
    {
        name = $"{theRarity} ";

        buyCost = (buyCost + RarityMultiplier) * RarityMultiplier;
        buyCost = Math.Round(buyCost);

        sellCost = (sellCost + RarityMultiplier) * RarityMultiplier;
        sellCost = Math.Round(sellCost);
    }

    public string Name
    {
        get => name;

        set
        {
            name += value;
        }
    }

    public double BuyCost
    {
        get => buyCost;
    }

    public double SellCost
    {
        get => sellCost;
    }
}
