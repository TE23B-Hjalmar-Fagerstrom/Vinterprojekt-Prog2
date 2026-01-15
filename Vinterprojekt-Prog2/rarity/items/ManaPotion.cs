public class ManaPotion : Consumable
{
    private double manaAmount;

    public ManaPotion()
    {
        manaAmount = (5 + RarityMultiplier) * RarityMultiplier;
        manaAmount = Math.Round(manaAmount);

        UsesCurent = UsesMax;
        UsesDuration = 1;
    }

    public override void Use(Player target)
    {
        target.Mp += manaAmount;
        UsesCurent -= 1;
    }
}
