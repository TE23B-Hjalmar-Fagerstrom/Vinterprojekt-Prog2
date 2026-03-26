public class Weapon : Item
{
    private double minDamage = 5;
    private double maxDamage = 15;
    private float damageMultiplier;
    private List<string> weaponNames = ["Svärd", "Yxa", "Klubba", "Spjut"];

    public Weapon()
    {
        if (firstWeapon == true)
        {
            theRarity = "Vanliga";
            RarityMultiplier = 1;
            firstWeapon = false;
            salable = false;
            Name = "knytnävar";
            maxDamage = 10;
        }
        else
        {
            Name = weaponNames[Random.Shared.Next(0, weaponNames.Count)];
        }

        damageMultiplier = RarityMultiplier;

        if (RarityMultiplier > 1)
        {
            minDamage = (minDamage + RarityMultiplier + damageMultiplier) * damageMultiplier;
            minDamage = Math.Round(minDamage);

            maxDamage = (maxDamage + RarityMultiplier + damageMultiplier) * damageMultiplier;
            maxDamage = Math.Round(maxDamage);
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