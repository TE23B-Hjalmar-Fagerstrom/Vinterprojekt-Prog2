public class LifeSteal : Abilitie
{
    private double usesPerFight;
    private double useDuration;
    private double helaAmount;
    private int upgradeTheDuration;

    public double HelaAmount { get => helaAmount; }

    public LifeSteal() // ger vilka start värden/text variablerna ska ha 
    {
        usesPerFight = 2;
        useDuration = 2;
        helaAmount = 0.15f;

        if (RarityMultiplier != 1) // om denns rarity inte är common så kalkylerar den vilka värden variablerna ska ha
        {
            usesPerFight *= RarityMultiplier;
            useDuration *= RarityMultiplier;
            helaAmount *= RarityMultiplier;
        }

        name = $"{theRarity} livsstjäla";

        usesPerFight = Math.Round(usesPerFight);
        useDuration = Math.Round(useDuration);
        helaAmount = Math.Round(helaAmount, 4);

        ManaCost = 10;

        description = $"{name} ger dig {helaAmount * 100}% hp baserat på hur mycket skada du gör (effekten vara i {useDuration} rundor och kan göras {usesPerFight} gånger vargje strid) kostar {ManaCost} mana";

    }

    public override void UseAbilitie(Enemy target, Player player) // när man använder en förmågan kollar den om spelaren har tillräckligt med mana eller inte 
    {
        if (usesPerFight > 0 && player.Mp >= ManaCost) // om spelaren har tillräckligt med mana och användningar kvar så gör den förmågan och avslutar spelarens tur
        {
            player.Mp -= ManaCost;
            player.LifeStealDuration += useDuration;

            usesPerFight--;

            Console.WriteLine($"Du får nu {helaAmount * 100}% av skadan du gör som HP");

            target.EnemyTurn = true;
        }
        else if (usesPerFight == 0) // om spelaren inte har flera användningar kvar så skriver den ut det och låter spelaren göra något annat
        {
            Console.WriteLine("Du har inga fler användningar");
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

        if (num == 2 && upgradeTheDuration == 0) // om den slumpar numret är två så kommer det ge text och göra så att nästa gång upgradera metoden görs att nummret inte slumpas och blir två
        {
            Console.WriteLine("Nästa gång du uppgraderar så förlängs hur länge effekten varar med 1 runda");
            Console.WriteLine("Tryck enter för att lämna denna skärm.");

            Console.ReadLine();
        }
        else if (upgradeTheDuration == 1) // om det slumpade tallet var två föra gången så komer den bli två igen och upgradera hur länge effekten varar
        {
            num = 2;
        }

        switch (num) // säger vilken uppgradering som ska göras baserat på det slumpade tallet 
        {
            case 1: // ökar hur mycket hp man får 
                double oldHA = helaAmount;
                helaAmount = Math.Round(helaAmount * multiplier, 4);
                Console.WriteLine($"Du får nu {Math.Round(helaAmount * 100, 4)}% HP istället för {oldHA * 100}%");
                break;

            case 2: // ökar hur länge effekten kommer att vara
                upgradeTheDuration++;
                if (upgradeTheDuration == 2)
                {
                    double oldUD = useDuration;
                    useDuration++;
                    upgradeTheDuration = 0;

                    Console.WriteLine($"effekten vara nu {useDuration} rundor istället för {oldUD} rundor");
                }
                break;

            default: // minskar mana kostnaden med 1 
                ManaCost--;
                Console.WriteLine($"{name} costar nu en mana mindre ({ManaCost} mana)");
                break;
        }
    }
}