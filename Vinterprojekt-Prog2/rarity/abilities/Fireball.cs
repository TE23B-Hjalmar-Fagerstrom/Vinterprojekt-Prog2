public class Fireball : Abilitie
{
    private double burnDuration;
    private double burnDamage;

    public double BurnDamage
    {
        get => burnDamage;
    }

    public Fireball() // ger vilka start värden/text variablerna ska ha 
    {
        MageDamage = 10;
        burnDuration = 2;
        burnDamage = MageDamage * 0.25F;

        if (RarityMultiplier != 1) // om denns rarity inte är common så kalkylerar den om vilka värden variablerna ska ha
        {
            MageDamage = (MageDamage + RarityMultiplier) * RarityMultiplier;
            burnDuration *= RarityMultiplier;
            burnDamage = MageDamage * 0.25F * RarityMultiplier;
        }

        name = $"{theRarity} Eldklot";

        MageDamage = Math.Round(MageDamage);
        burnDuration = Math.Round(burnDuration);
        burnDamage = Math.Round(burnDamage);

        ManaCost = 10;

        description = $"{Name} skadar {MageDamage} och applicerar brinnande på fienden (gör {burnDamage} skada i {burnDuration} rundor) kostar {ManaCost} mana";
    }

    public override void UseAbilitie(Enemy target, Player player) // när man använder en förmågan kollar den om spelaren har tillräckligt med mana eller inte 
    {
        if (player.Mp >= ManaCost) // om spelaren har tillräckligt med mana så gör den förmågan och avslutar spelarens tur
        {
            target.HowLongBurn += burnDuration;
            target.Hp -= MageDamage;

            player.Mp -= ManaCost;

            Console.WriteLine($"{target.EnemyName} tar {MageDamage} skada och börjar brina (vara i {target.HowLongBurn} rundor)");

            target.EnemyTurn = true;
        }
        else // om man inte har tillräckligt med mana så skriver den ut det och låter spelaren göra något annat
        {
            Console.WriteLine("Du har inte tillräckligt med mana");
        }
    }

    public override void Upgrade(float multiplier) // metoden slumpar en uppgradering till förmågan
    {
        int num;

        if (ManaCost > 1) // om mana costnaden är över 1 så kan den upgraderas tills den blir 1
        {
            num = Random.Shared.Next(1, 5);
        }
        else // om mana costnaden är max upgraderad så kan den inte slumpas längre
        {
            num = Random.Shared.Next(1, 4);
        }

        int upgradeTheDuration = 0;

        if (num == 2 && upgradeTheDuration == 0) // om den slumpar numret är två så kommer det ge text och göra så att nästa gång upgradera metoden görs att nummret inte slumpas och blir två
        {
            Console.WriteLine("Nästa gång du uppgraderar så förlängs hur länge en fiende blir lamslagen med 1 runda");
            Console.WriteLine("Tryck enter för att lämna denna skärm.");

            Console.ReadLine();
        }
        else if (upgradeTheDuration == 1) // om det slumpade tallet var två föra gången så komer den bli två igen och upgradera hur länge effekten varar
        {
            num = 2;
        }

        switch (num) // säger vilken uppgradering som ska göras baserat på det slumpade tallet 
        {
            case 1: // ökar hur mycket skada äld klotet gör
                double oldMD = MageDamage;
                MageDamage = Math.Round(MageDamage * multiplier);
                Console.WriteLine($"skadan upgraderades från {oldMD} till {MageDamage} (påverkar inte brännskada)");
                break;

            case 2: // ökar hur mycket brännskadan gör vargje runda den är applicerad
                double oldBD = burnDamage;
                burnDamage = Math.Round(burnDamage * multiplier);
                Console.WriteLine($"brännskada upgraderades från {oldBD} till {burnDamage}");
                break;

            case 3: // ökar hur länge fienden kommer att brina 
                upgradeTheDuration++;
                if (upgradeTheDuration == 2)
                {
                    double oldDu = burnDuration;
                    burnDuration++;
                    upgradeTheDuration = 0;

                    Console.WriteLine($"effekten vara nu {burnDuration} rundor istället för {oldDu} rundor");
                }
                break;

            default: // minskar mana kostnaden med 1 
                ManaCost--;
                Console.WriteLine($"{name} costar nu en mana mindre ({ManaCost} mana)");
                break;
        }
    }
}