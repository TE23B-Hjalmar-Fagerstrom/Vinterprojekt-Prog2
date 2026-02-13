public class Weapon : Item
{
    private double minDamage = 5;
    private double maxDamage = 15;
    private float damageMultiplier;
    private List<string> weaponNames = ["Svärd", "Yxa", "Klubba", "Spjut"];
    public static bool firstWeapon = true;

    public Weapon()
    {
        if (firstWeapon == true)
        {
            randomMax = 2;
            firstWeapon = false;
            Name = "knytnävar";
        }
        else
        {
            Name = weaponNames[Random.Shared.Next(0, weaponNames.Count)];   
        }

        damageMultiplier = RarityMultiplier;

        minDamage = (minDamage + RarityMultiplier + damageMultiplier) * damageMultiplier;
        minDamage = Math.Round(minDamage);

        maxDamage = (maxDamage + RarityMultiplier + damageMultiplier) * damageMultiplier;
        maxDamage = Math.Round(maxDamage);
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
