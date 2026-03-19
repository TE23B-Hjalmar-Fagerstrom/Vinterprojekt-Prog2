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
            MageDamage = 20;
            burnDuration = 2;
        }
        else
        {
            MageDamage = (20 + RarityMultiplier) * RarityMultiplier;
            burnDuration = 2 * RarityMultiplier;
        }

        name = $"{theRarity} Eldklot";

        burnDamage = MageDamage * 0.25F;

        MageDamage = Math.Round(MageDamage);
        burnDuration = Math.Round(burnDuration);
        burnDamage = Math.Round(burnDamage);

        ManaCost = 10;

        description = $"{Name} skadar {MageDamage} och applicerar brinnande på fienden (gör {burnDamage} skada i {burnDuration} rundor) kåstar {ManaCost} mana";
    }

    public override void UseAbilitie(Enemy target, Player player)
    {
        if (player.Mp >= ManaCost)
        {
            target.Hp -= MageDamage;
            target.HowLongBurn += burnDuration;

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

        switch (num)
        {
            case 1:
                double oldMD = MageDamage;
                MageDamage = Math.Round(MageDamage * multiplier);
                Console.WriteLine($"skadan upgraderades från {oldMD} till {MageDamage} (påverkar inte brinnande skada)");
                break;

            case 2:

            default:
                ManaCost--;
                Console.WriteLine($"{name} costar nu en mana mindre");
                break;
        }
    }
}
