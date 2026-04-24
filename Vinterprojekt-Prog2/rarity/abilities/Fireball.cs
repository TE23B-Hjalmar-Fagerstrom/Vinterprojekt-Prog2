public class Fireball : Abilitie
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
            MageDamage = 10;
            burnDuration = 2;
        }
        else
        {
            MageDamage = (10 + RarityMultiplier) * RarityMultiplier;
            burnDuration = 2 * RarityMultiplier;
        }

        name = $"{theRarity} Eldklot";

        burnDamage = MageDamage * 0.25F;

        MageDamage = Math.Round(MageDamage);
        burnDuration = Math.Round(burnDuration);
        burnDamage = Math.Round(burnDamage);

        ManaCost = 10;

        description = $"{Name} skadar {MageDamage} och applicerar brinnande på fienden (gör {burnDamage} skada i {burnDuration} rundor) kostar {ManaCost} mana";
    }

    public override void UseAbilitie(Enemy target, Player player)
    {
        if (player.Mp >= ManaCost)
        {
            target.HowLongBurn += burnDuration;
            target.Hp -= MageDamage;

            player.Mp -= ManaCost;

            Console.WriteLine($"{target.EnemyName} tar {MageDamage} skada och börjar brina (vara i {target.HowLongBurn} rundor)");
        }
        else
        {
            Console.WriteLine("Du har inte tillräckligt med mana");
        }
    }

    public override void Upgrade(float multiplier)
    {
        int num;

        if (ManaCost > 1)
        {
            num = Random.Shared.Next(1, 5);
        }
        else
        {
            num = Random.Shared.Next(1, 4);
        }

        int upgradeTheDuration = 0;

        if (num == 2 && upgradeTheDuration == 0)
        {
            Console.WriteLine("Nästa gång du uppgraderar så förlängs hur länge en fiende blir lamslagen med 1 runda");
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
                double oldMD = MageDamage;
                MageDamage = Math.Round(MageDamage * multiplier);
                Console.WriteLine($"skadan upgraderades från {oldMD} till {MageDamage} (påverkar inte brännskada)");
                break;

            case 2:
                double oldBD = burnDamage;
                burnDamage = Math.Round(burnDamage * multiplier);
                Console.WriteLine($"brännskada upgraderades från {oldBD} till {burnDamage}");
                break;

            case 3:
                upgradeTheDuration++;
                if (upgradeTheDuration == 2)
                {
                    double oldDU = burnDuration;
                    burnDuration++;
                    upgradeTheDuration = 0;

                    Console.WriteLine($"effekten vara nu {burnDuration} rundor istället för {oldDU} rundor");
                }
                break;

            default:
                ManaCost--;
                Console.WriteLine($"{name} costar nu en mana mindre ({ManaCost} mana)");
                break;
        }
    }
}