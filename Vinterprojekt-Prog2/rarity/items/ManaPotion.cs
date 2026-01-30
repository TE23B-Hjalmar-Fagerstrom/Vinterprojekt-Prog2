public class ManaPotion : Consumable
{
    private double manaAmount;

    public ManaPotion()
    {
        manaAmount = (5 + RarityMultiplier) * RarityMultiplier;
        manaAmount = Math.Round(manaAmount);

        Name = "Mana Potion";
    }

    public override void Use(Player target)
    {
        if (UsesCurent > 0)
        {
            target.Mp += manaAmount;
            UsesCurent--;
        }
    }
}
