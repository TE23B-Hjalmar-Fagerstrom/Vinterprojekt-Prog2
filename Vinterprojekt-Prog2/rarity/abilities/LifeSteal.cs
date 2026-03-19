public class LifeSteal : Abilitie
{
    private double usesPerFight;
    private double useDuration;
    private double helaAmount;
    private int upgradeTheDuration;

    public double HelaAmount { get => helaAmount; }

    public LifeSteal()
    {
        if (RarityMultiplier == 1)
        {
            MageDamage = 15;
            usesPerFight = 2;
            useDuration = 2;
            helaAmount = 0.15f;
        }
        else
        {
            MageDamage = (15 + RarityMultiplier) * RarityMultiplier;
            usesPerFight = 2 * RarityMultiplier;
            useDuration = 2 * RarityMultiplier;
            helaAmount = 0.15f * RarityMultiplier;
        }

        name = $"{theRarity} livsstjäla";

        MageDamage = Math.Round(MageDamage);
        usesPerFight = Math.Round(usesPerFight);
        useDuration = Math.Round(useDuration);
        helaAmount = Math.Round(helaAmount, 4);

        ManaCost = 10;

        description = $"{name} ger dig {helaAmount * 100}% hp baserat på hur mycket skada du gör (effekten vara i {useDuration} rundor och kan göras {usesPerFight} gånger vargje strid) kostar {ManaCost} mana";

    }

    public override void UseAbilitie(Enemy target, Player player)
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

    public override void Upgrade(float multiplier)
    {
        int num = Random.Shared.Next(1, 3);

        if (num == 2 && upgradeTheDuration == 0)
        {
            Console.WriteLine("Nästa gång du uppgraderar så förlängs hur länge effekten varar med 1 runda");
            Console.WriteLine("Tryck enter för att lämna denna skärm.");

            Console.ReadLine();
        }
        else if (upgradeTheDuration == 1)
        {
            num = 2;
        }

        switch (num)
        {
            case 1:
                double oldHA = helaAmount;
                helaAmount = Math.Round(helaAmount * multiplier, 4);
                Console.WriteLine($"Du får nu {helaAmount * 100}% HP istället för {oldHA * 100}%");
                break;

            case 2:
                upgradeTheDuration++;
                if (upgradeTheDuration == 2)
                {
                    double oldUD = useDuration;
                    useDuration++;
                    upgradeTheDuration = 0;

                    Console.WriteLine($"effekten vara nu {useDuration} rundor istället för {oldUD} rundor");
                }
                break;

            default:
                ManaCost--;
                Console.WriteLine($"{name} costar nu en mana mindre");
                break;
        }
    }
}
