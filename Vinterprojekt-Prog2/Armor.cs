public class Armor : Item
{
    private double armor = 5;
    private double mageArmor = 3;
    private float armorMultiplier;

    public Armor()
    {
        armor = (armor + armorMultiplier + RarityMultiplier) * armorMultiplier;
        Math.Round(armor);

        mageArmor = (mageArmor + armorMultiplier + RarityMultiplier) * armorMultiplier;
        Math.Round(mageArmor);

        armorMultiplier = RarityMultiplier;
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
