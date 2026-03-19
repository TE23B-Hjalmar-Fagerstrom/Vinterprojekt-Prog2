public class Item : Rarity
{
    private string name;
    private double buyCost = 20;
    private double sellCost = 10;
    protected string description;
    protected bool weaponBool = false;
    protected bool consumableBool = false;
    protected bool armorBool = false;
    protected bool salable = true;

    public Item()
    {
        name = $"{theRarity} ";

        buyCost = (buyCost + RarityMultiplier) * RarityMultiplier;
        buyCost = Math.Round(buyCost);

        sellCost = (sellCost + RarityMultiplier) * RarityMultiplier;
        sellCost = Math.Round(sellCost);

        description = "";
    }

    public bool WeaponBool{get => weaponBool;}
    public bool ConsumableBool {get => consumableBool;}
    public bool ArmorBool {get => armorBool;}

    public string Description
    {
        get => description;
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
