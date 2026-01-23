public class Weapon : Item
{
    private double minDamage = 5;
    private double maxDamage = 15;
    private float damageMultiplier;

    public Weapon()
    {
        damageMultiplier = RarityMultiplier;

        minDamage = (minDamage + RarityMultiplier + damageMultiplier) * damageMultiplier;
        minDamage = Math.Round(minDamage);

        maxDamage = (maxDamage + RarityMultiplier + damageMultiplier) * damageMultiplier;
        maxDamage = Math.Round(minDamage);
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
