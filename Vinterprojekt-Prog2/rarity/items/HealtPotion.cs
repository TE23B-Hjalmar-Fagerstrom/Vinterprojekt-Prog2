public class HealtPotion : Consumable
{
    private double healAmount;

    public HealtPotion() // ger vilka start värden/text variablerna ska ha 
    {
        healAmount = Math.Round((15 + RarityMultiplier) * RarityMultiplier);

        Name = "hälso dryck";

        consumableValue = healAmount;
        effect = $"helande: användningar kvar({UsesCurent})";

        TheDescription(); // läser in föremålets beskrivning
    }

    public override void Use(Player target) // använder 
    {
        if (UsesCurent > 0) // så länge föremålet forfarande har användningar
        {
            target.Hp += healAmount;
            UsesCurent--;
            effect = $"helande: användningar kvar({UsesCurent})";
            TheDescription(); // läser in föremålets beskrivning

            Console.WriteLine($"du använde {Name} och ditt HP är nu {target.Hp}. Den har {UsesCurent} användningar kvar");
            Console.WriteLine($"Tryck enter för att lämna denna skärm");

            Console.ReadLine();
        }
    }
}