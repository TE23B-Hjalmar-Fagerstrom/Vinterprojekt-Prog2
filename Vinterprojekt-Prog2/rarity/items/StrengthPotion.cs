public class StrengthPotion : Consumable
{
    private double damageMultiplierFromPotion = 1;
    public double DamageMultiplierFromPotion { get => damageMultiplierFromPotion;}

    public StrengthPotion()
    {
        damageMultiplierFromPotion = RarityMultiplier * RarityMultiplier;

        UsesCurent = UsesMax;
        UsesDuration = 2;

        Name = "Strength Potion";

        consumableValue = damageMultiplierFromPotion;
        effect = $"Styrke multiplikator: användningar kvar({UsesCurent})";

        TheDescription();
    }

    public override void Use(Player target)
    {
        if (UsesCurent > 0)
        {
            target.Damage *= damageMultiplierFromPotion;
            target.PotionDuration += UsesDuration;
            UsesCurent -= 1;
        }
    }
}
