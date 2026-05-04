public class Armor : Item
{
    private double armor = 5;
    private float armorMultiplier;
    private List<string> armorTyps = ["Läder rustning", "Koppar rustning", "Stål rustning"];

    public Armor() // ger vilka start värden/text variablerna ska ha 
    {
        armorMultiplier = RarityMultiplier + (RarityMultiplier / 10);

        if (theRarity != "Vanlig") // om rarity inte är common
        {
            armor = (armor + armorMultiplier + RarityMultiplier) * armorMultiplier;
            armor = Math.Round(armor);

        }

        Name = armorTyps[Random.Shared.Next(0, armorTyps.Count)];

        if (Name == $"{theRarity} Läder rustning") // om rustningen är läder så får den mindre armor
        {
            armor -= 2;
        }
        else if (Name == $"{theRarity} Stål rustning") // om rustningen är stål får den mer armor
        {
            armor += 3;
        }

        description = $"(blockar {armor} skada)";

        armorBool = true;
    }

    public double Defens
    {
        get => armor;
    }

    public float ArmorMultiplier
    {
        get => armorMultiplier;
    }
}