public class Abilitie : Rarity
{
    private double mageDamage;
    private double manaCost;

    public double MageDamage
    {
        get => mageDamage;

        set
        {
            mageDamage = value;
        }
    }

    public double ManaCost
    {
        get => manaCost;

        set
        {
            manaCost = value;
        }
    }
}
