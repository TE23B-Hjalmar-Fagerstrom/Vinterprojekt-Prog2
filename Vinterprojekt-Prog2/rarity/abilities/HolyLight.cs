public class HolyLight : Abilitie
{
    private double stunDuration;
    private int upgradeStunDuration;

    public HolyLight() // ger vilka start värden/text variablerna ska ha 
    {
        MageDamage = 15;
        stunDuration = 1;

        if (RarityMultiplier != 1) // om denns rarity inte är common så kalkylerar den vilka värden variablerna ska ha
        {
            MageDamage = (MageDamage + RarityMultiplier) * RarityMultiplier;
            stunDuration += RarityMultiplier;
        }

        MageDamage = Math.Round(MageDamage);
        stunDuration = Math.Round(stunDuration);

        name = $"{theRarity} heligt ljus";

        ManaCost = 15;

        description = $"{name} skadar {MageDamage} och lamslår fienden i {stunDuration} rundor, kostar {ManaCost} mana";
    }


    public override void UseAbilitie(Enemy target, Player player) // när man använder en förmågan kollar den om spelaren har tillräckligt med mana eller inte 
    {
        if (player.Mp >= 10) // om spelaren har tillräckligt med mana så gör den förmågan och avslutar spelarens tur
        {
            target.Hp -= MageDamage;
            target.HowLongStund += stunDuration;

            player.Mp -= ManaCost;

            Console.WriteLine($"{target.EnemyName} tar {MageDamage} skada och blir lamslagen (vara i {target.HowLongStund} rundor)");

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
            num = Random.Shared.Next(1, 4);
        }
        else // om mana costnaden är max upgraderad så kan den inte slumpas längre
        {
            num = Random.Shared.Next(1, 3);
        }

        if (num == 2 && upgradeStunDuration == 0) // om den slumpar numret är två så kommer det ge text och göra så att nästa gång upgradera metoden görs att nummret inte slumpas och blir två
        {
            Console.WriteLine("Nästa gång du uppgraderar så förlängs hur länge en fiende blir lamslagen med 1 runda");
            Console.WriteLine("Tryck enter för att lämna denna skärm.");

            Console.ReadLine();
        }
        else if (upgradeStunDuration == 1) // om det slumpade tallet var två föra gången så komer den bli två igen och upgradera hur länge effekten varar
        {
            num = 2;
        }

        switch (num) // säger vilken uppgradering som ska göras baserat på det slumpade tallet 
        {
            case 1:
                double oldMD = MageDamage;
                MageDamage = Math.Round(MageDamage * multiplier);
                Console.WriteLine($"skadan upgraderades från {oldMD} till {MageDamage}");
                break;

            case 2: // ökar hur länge fienden kommer vara lamslagen
                upgradeStunDuration++;
                if (upgradeStunDuration == 2)
                {
                    double oldSD = stunDuration;
                    stunDuration++;
                    upgradeStunDuration = 0;

                    Console.WriteLine($"du lamslår fiender nu i {stunDuration} rundor istället för {oldSD} rundor");
                }
                break;

            default: // minskar mana kostnaden med 1 
                ManaCost--;
                Console.WriteLine($"{name} costar nu en mana mindre ({ManaCost} mana)");
                break;
        }
    }
}