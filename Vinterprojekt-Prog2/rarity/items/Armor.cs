public class Armor : Item
{
    private double armor = 5;
    private double mageArmor = 3;
    private float armorMultiplier;
    private List<string> armorTyps = ["Läder rustning", "Koppar rustning", "Stål rustning"];

    public Armor()
    {
        armorMultiplier = RarityMultiplier + .1f;

        if (theRarity != "Vanlig")
        {
            armor = (armor + armorMultiplier + RarityMultiplier) * armorMultiplier;
            armor = Math.Round(armor);

            mageArmor = (mageArmor + armorMultiplier + RarityMultiplier) * armorMultiplier;
            mageArmor = Math.Round(mageArmor);
        }

        Name = armorTyps[Random.Shared.Next(0, armorTyps.Count)];

        if (Name == $"{theRarity} Läder rustning")
        {
            armor -= 2;
            mageArmor -= 1;
        }
        else if (Name == $"{theRarity} Stål rustning")
        {
            armor += 3;
            mageArmor += 2;
        }

        description = $"(blockar {armor} fysisk skada och {mageArmor} magisk skada)";

        armorBool = true;
    }

    public double Defens
    {
        get => armor;
    }

    public double MageArmor
    {
        get => mageArmor;
    }

    public float ArmorMultiplier
    {
        get => armorMultiplier;
    }
}