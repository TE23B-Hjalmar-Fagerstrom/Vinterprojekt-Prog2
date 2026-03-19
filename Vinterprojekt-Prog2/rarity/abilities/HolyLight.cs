public class HolyLight : Abilitie
{
    private double stunDuration;
    private int upgradeStunDuration;

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

        name = $"{theRarity} heligt ljus";

        ManaCost = 15;

        description = $"{name} skadar {MageDamage} och lamslår fienden i {stunDuration} rundor, kostar {ManaCost} mana";
    }


    public override void UseAbilitie(Enemy target, Player player)
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

    public override void Upgrade(float multiplier)
    {
        int num = Random.Shared.Next(1, 3);

        if (num == 2 && upgradeStunDuration == 0)
        {
            Console.WriteLine("Nästa gång du uppgraderar så förlängs hur länge en fiende blir lamslagen med 1 runda");
            Console.WriteLine("Tryck enter för att lämna denna skärm.");

            Console.ReadLine();
        }
        else if (upgradeStunDuration == 1)
        {
            num = 2;
        }

        switch (num)
        {
            case 1:
                double oldMD = MageDamage;
                MageDamage = Math.Round(MageDamage * multiplier);
                Console.WriteLine($"skadan upgraderades från {oldMD} till {MageDamage}");
                break;

            case 2:
                upgradeStunDuration++;
                if (upgradeStunDuration == 2)
                {
                    double oldSD = stunDuration;
                    stunDuration++;
                    upgradeStunDuration = 0;

                    Console.WriteLine($"du lamslår fiender nu i {stunDuration} rundor istället för {oldSD} rundor");
                }
                break;

            default:
                ManaCost--;
                Console.WriteLine($"{name} costar nu en mana mindre");
                break;
        }
    }
}