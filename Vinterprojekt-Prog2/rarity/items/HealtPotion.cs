public class HealtPotion : Consumable
{
    private double healAmount;

    public HealtPotion()
    {
        healAmount = (25 + RarityMultiplier) * RarityMultiplier;
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
        }
    }
}