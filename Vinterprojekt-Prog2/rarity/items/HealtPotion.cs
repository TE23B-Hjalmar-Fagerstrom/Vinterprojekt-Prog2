public class HealtPotion : Consumable
{
    private double healAmount;

    public HealtPotion()
    {
        healAmount = (15 + RarityMultiplier) * RarityMultiplier;
        healAmount = Math.Round(healAmount);

        Name = "hälso dryck";

        consumableValue = healAmount;
        effect = $"helande: användningar kvar({UsesCurent})";

        TheDescription();
    }

    public override void Use(Player target)
    {
        if (UsesCurent > 0)
        {
            target.Hp += healAmount;
            UsesCurent--;
            effect = $"helande: användningar kvar({UsesCurent})";
            TheDescription();

            Console.WriteLine($"du använde {Name} och ditt HP är nu {target.Hp}. Den har {UsesCurent} användningar kvar");
            Console.WriteLine($"Tryck enter för att lämna denna skärm");

            Console.ReadLine();
        }
    }
}