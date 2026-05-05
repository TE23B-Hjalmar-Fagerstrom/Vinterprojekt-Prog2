public class ManaPotion : Consumable
{
    private double manaAmount;

    public ManaPotion() // ger vilka start värden/text variablerna ska ha 
    {
        manaAmount = (5 + RarityMultiplier) * RarityMultiplier;
        manaAmount = Math.Round(manaAmount);

        Name = "Mana Dryck";

        consumableValue = manaAmount;
        effect = $"Mana: användningar kvar({UsesCurent})";

        TheDescription(); // läser in föremålets beskrivning
    }

    public override void Use(Player target) // gör så att spelaren får mana och minskar hur många gånger man kan göra det
    {
        if (UsesCurent > 0) // så länge föremålet forfarande har användningar
        {
            target.Mp += manaAmount;
            UsesCurent--;
            effect = $"Mana: användningar kvar({UsesCurent})";
            TheDescription(); // läser in föremålets beskrivning

            Console.WriteLine($"du använde {Name} och din mana är nu {target.Mp}. Den har {UsesCurent} användningar kvar");
            Console.WriteLine($"Tryck enter för att lämna denna skärm");

            Console.ReadLine();
        }
    }
}