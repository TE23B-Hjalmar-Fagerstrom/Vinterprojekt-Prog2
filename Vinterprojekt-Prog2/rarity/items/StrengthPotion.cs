public class StrengthPotion : Consumable
{
    private double damageMultiplierFromPotion = 1;

    public StrengthPotion()
    {
        damageMultiplierFromPotion = RarityMultiplier * RarityMultiplier;
        damageMultiplierFromPotion = Math.Round(damageMultiplierFromPotion);

        UsesCurent = UsesMax;
        UsesDuration = 2;
    }

    public override void Use(Player target)
    {
        target.Damage *= damageMultiplierFromPotion;
        UsesCurent -= 1;
    }
}
