public class LifeSteal : Upgrade
{
    private double usesPerFight;
    private double useDuration;
    private double helaAmount;

    public double HelaAmount { get => helaAmount; }

    public LifeSteal(Player player)
    {
        if (RarityMultiplier == 1)
        {
            MageDamage = 15;
            usesPerFight = 2;
            useDuration = 2;
            helaAmount = player.Damage * 0.15f;
        }
        else
        {
            MageDamage = (15 + RarityMultiplier) * RarityMultiplier;
            usesPerFight = 2 * RarityMultiplier;
            useDuration = 2 * RarityMultiplier;
            helaAmount = player.Damage * (0.15f * RarityMultiplier);
        }

        MageDamage = Math.Round(MageDamage);
        usesPerFight = Math.Round(usesPerFight);
        useDuration = Math.Round(useDuration);

        ManaCost = 10;
    }

    public void UseAbilitie(Player player)
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
