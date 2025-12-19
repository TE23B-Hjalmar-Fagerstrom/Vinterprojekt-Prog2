public class HolyLight : Upgrade
{
    private int stunDuration;

    public HolyLight()
    {
        MageDamage = (30 + RarityMultiplier) * RarityMultiplier;
        MageDamage = Math.Round(MageDamage);

        ManaCost = 15;
    }

    
    public void UseAbilitie(Enemy target)
    {
        target.Hp -= MageDamage;
        target.HowLongStund += stunDuration;
    }
}
