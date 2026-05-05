public class Weapon : Item
{
    private double minDamage = 5;
    private double maxDamage = 15;
    private float damageMultiplier;
    private List<string> weaponNames = ["Svärd", "Yxa", "Klubba", "Spjut"];

    public Weapon() // ger vilka start värden/text variablerna ska ha 
    {
        if (firstWeapon == true) // gör så att det första vapnet man för är 
        {
            theRarity = "Vanliga";
            RarityMultiplier = 1;
            firstWeapon = false;
            Name = "knytnävar";
            maxDamage = 10;
        }
        else // anars ge ett vapen med slumpad namn
        {
            Name = weaponNames[Random.Shared.Next(0, weaponNames.Count)];
        }

        damageMultiplier = RarityMultiplier + (RarityMultiplier / 10);

        if (RarityMultiplier > 1) // om vapnet inte är common
        {
            minDamage = Math.Round((minDamage + RarityMultiplier + damageMultiplier) * damageMultiplier);

            maxDamage = Math.Round((maxDamage + RarityMultiplier + damageMultiplier) * damageMultiplier);
        }

        description = $"({MinDamage} - {MaxDamage} skada)";

        weaponBool = true;
    }

    public double MinDamage
    {
        get => minDamage;
    }

    public double MaxDamage
    {
        get => maxDamage;
    }

    public float DamageMultiplier
    {
        get => damageMultiplier;
    }
}