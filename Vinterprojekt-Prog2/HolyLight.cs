public class HolyLight : Upgrade
{
    private double stunDuration;

    public HolyLight()
    {
        if (RarityMultiplier == 1)
        {
            MageDamage = 30;
            stunDuration = 1;
        }
        else
        {
            MageDamage = (30 + RarityMultiplier) * RarityMultiplier;
            stunDuration = 1 + RarityMultiplier;
        }

        MageDamage = Math.Round(MageDamage);
        stunDuration = Math.Round(stunDuration);

        ManaCost = 15;
    }


    public void UseAbilitie(Enemy target, Player player)
    {
        if (player.Mp >= 10)
        {
            target.Hp -= MageDamage;
            target.HowLongStund += stunDuration;

            player.Mp -= ManaCost;
        }
        else
        {
            Console.WriteLine("Du har inte tillräckligt med mana");
        }
    }
}
