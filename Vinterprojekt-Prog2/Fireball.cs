using System.Threading.Channels;

public class Fireball : Upgrade
{
    private double burnDuration;
    private double burnDamage;

    public double BurnDamage
    {
        get => burnDamage;
    }

    public Fireball()
    {
        if (RarityMultiplier == 1)
        {
            MageDamage = 20;
            burnDuration = 2;
        }
        else
        {
            MageDamage = (20 + RarityMultiplier) * RarityMultiplier;
            burnDuration = 2 * RarityMultiplier;
        }

        burnDamage = MageDamage * 0.25F;

        MageDamage = Math.Round(MageDamage);
        burnDuration = Math.Round(burnDuration);
        burnDamage = Math.Round(burnDamage);

        ManaCost = 10;
    }

    public void UseAbilitie(Enemy target)
    {
        target.Hp -= MageDamage;
        target.HowLongBurn += burnDuration;
    }
}
