public class Armor : Item
{
    private double armor = 5;
    private double mageArmor = 3;
    private float armorMultiplier;
    private List<string> armorTyps = ["Läder", "Koppar", "Stål"];

    public Armor()
    {
        armorMultiplier = RarityMultiplier + .1f;

        armor = (armor + armorMultiplier + RarityMultiplier) * armorMultiplier;
        armor = Math.Round(armor);

        mageArmor = (mageArmor + armorMultiplier + RarityMultiplier) * armorMultiplier;
        mageArmor = Math.Round(mageArmor);

        Name = armorTyps[Random.Shared.Next(0, armorTyps.Count)];

        description = $"rustning (blockar {armor} skada)";

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
