public class Abilitie : Rarity
{
    private double mageDamage;
    private double manaCost;

    public double MageDamage
    {
        get => mageDamage;

        protected set
        {
            mageDamage = value;
        }
    }

    public double ManaCost
    {
        get => manaCost;

        protected set
        {
            manaCost = value;
        }
    }

    public void Upgrade(float multiplier)
    {
        MageDamage = MageDamage * multiplier;
    }
}
