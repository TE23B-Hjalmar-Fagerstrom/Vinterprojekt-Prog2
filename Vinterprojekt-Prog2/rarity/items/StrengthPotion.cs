public class StrengthPotion : Consumable
{
    private double damageMultiplierFromPotion = 1;
    public double DamageMultiplierFromPotion { get => damageMultiplierFromPotion; }

    public StrengthPotion() // ger vilka start värden/text variablerna ska ha 
    {
        damageMultiplierFromPotion = (RarityMultiplier * RarityMultiplier) + (RarityMultiplier / 10);
        damageMultiplierFromPotion = Math.Round(damageMultiplierFromPotion, 3);

        UsesCurent = UsesMax;
        UsesDuration = 2;

        Name = "Strength Potion";

        consumableValue = damageMultiplierFromPotion;

        effect = $"Styrke multiplikator: användningar kvar({UsesCurent})";

        TheDescription(); // läser in föremålets beskrivning
    }

    public override void Use(Player target) // gör så att spelaren får mera skada under några rundor och minskar hur många gånger man kan göra det
    {
        if (UsesCurent > 0) // så länge föremålet forfarande har användningar
        {
            target.Damage *= damageMultiplierFromPotion;
            target.PotionDuration += UsesDuration;
            UsesCurent--;
            effect = $"Styrke multiplikator: användningar kvar({UsesCurent})";
            TheDescription(); // läser in föremålets beskrivning

            Console.WriteLine($"du använde {Name} och du gör nu {DamageMultiplierFromPotion} gånger mer skada. Den har {UsesCurent} användningar kvar och varar i {UsesDuration} rundor");
            Console.WriteLine($"Tryck enter för att lämna denna skärm");

            Console.ReadLine();
        }
    }
}