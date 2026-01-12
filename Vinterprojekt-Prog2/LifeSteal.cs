public class LifeSteal : Upgrade
{
    private double usesPerFight;
    private double useDuration;
    private float helaAmount;

    public LifeSteal()
    {
        if (RarityMultiplier == 1)
        {
            MageDamage = 15;
            usesPerFight = 2;
            useDuration = 2;
        }
        else
        {
            MageDamage = (15 + RarityMultiplier) * RarityMultiplier;
            usesPerFight = 2 * RarityMultiplier;
            useDuration = 2 * RarityMultiplier;
        }

        Math.Round(MageDamage);
        Math.Round(usesPerFight);
        Math.Round(useDuration);

        ManaCost = 10;
    }

    public void UseAbilitie(Enemy target, Player player)
    {
        if (usesPerFight > 0 && player.Mp >= ManaCost)
        {
            player.Mp -= ManaCost;
            player.LifeStealDuration += useDuration;

            usesPerFight--;
        }
        else if (usesPerFight == 0)
        {
            Console.WriteLine("Du har inga fler användningar");
        }
        else
        {
            Console.WriteLine("Du har inte tillräckligt med mana");
        }

    }
}
