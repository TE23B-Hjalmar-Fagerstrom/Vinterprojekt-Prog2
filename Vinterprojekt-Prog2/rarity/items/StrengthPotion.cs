public class StrengthPotion : Consumable
{
    private double damageMultiplierFromPotion = 1;
    public double DamageMultiplierFromPotion { get => damageMultiplierFromPotion; }

    public StrengthPotion()
    {
        damageMultiplierFromPotion = RarityMultiplier * RarityMultiplier;
        damageMultiplierFromPotion = Math.Round(damageMultiplierFromPotion, 3);

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
            UsesCurent--;
            effect = $"Styrke multiplikator: användningar kvar({UsesCurent})";
            TheDescription();

            Console.WriteLine($"du använde {Name} och du gör nu {DamageMultiplierFromPotion}% mer skada. Den har {UsesCurent} användningar kvar och varar i {UsesDuration} rundor");
            Console.WriteLine($"Tryck enter för att lämna denna skärm");

            Console.ReadLine();
        }
    }
}