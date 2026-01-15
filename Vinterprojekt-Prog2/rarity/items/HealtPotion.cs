public class HealtPotion : Consumable
{
    private double healAmount;

    public HealtPotion()
    {
        healAmount = (25 + RarityMultiplier) * RarityMultiplier;
        healAmount = Math.Round(healAmount);
        UsesCurent = UsesMax;
        UsesDuration = 1;

        Name ="health Potion";
    }

    public override void Use(Player target)
    {
        target.Hp += healAmount;
        UsesCurent -= 1;
    }
}
