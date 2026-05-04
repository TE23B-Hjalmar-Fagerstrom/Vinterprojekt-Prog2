public class Boss : Enemy
{
    static int bossCount;

    public Boss(Player player) : base(player) // ger vilka start värden/text variablerna ska ha 
    {
        randomMax = 125;
        difficultyMultiplier = 1.75;
        Damage = 10 * difficultyMultiplier;
        MaxHp = Math.Round(MaxHp * difficultyMultiplier * 1.5);
        Hp = MaxHp;
        Armor *= difficultyMultiplier;
        XpDrop *= difficultyMultiplier;
        GoldDrop *= 2;
        bossCount++;

        if (bossCount <= 5) // de fem första bossarna är dragungar
        {
            EnemyName = "Drakunge";
        }
        else if (bossCount <= 10 && bossCount > 5) // bossarna 6 till 10 är drakar och har dubelt så mycket hp som dragungarna
        {
            EnemyName = "Drake";
            MaxHp *= 2;
        }
        else // alla bossar efter 10 är Äldre drakar som har fyra gånger så mycket hp som dragungarna och hp blir störe med 0,2 efter vargje boss
        {
            EnemyName = "Äldre Drake";
            MaxHp *= 4 + ((bossCount - 11) / 5);
        }
    }

    public override void BattleLogic(Player player, Enemy target) // logiken för vad fienden kommer att göra under striden
    {
        if (EnemyTurn == false) // när det inte är fiendens tur så kommer den slumpa vad den kommer att göra och skriva ut det till spelaren
        {
            if (hasRolled == false && chargeUp != 1) // om fienden inte har slumpat vad den ska göra så gör den det 
            {
                randomNum = Random.Shared.Next(randomMin, randomMax + 1);
                hasRolled = true;
            }

            if (randomNum <= 50) // om det slumpade tallet är 50 eller mindre så försvarar fienden sig
            {
                Console.WriteLine($"{EnemyName} Planerar att försvara sig ");
                defending = true;
            }

            else if (randomNum > 50 && randomNum <= 100) // om det slumpade tallet är störe än 50 och mindre än 101 så attackera fienden
            {
                Damage = Random.Shared.Next((int)Damage, (int)Math.Round(Damage + (player.Level * 1.5f))); // slumpar fiendens skada
                Console.WriteLine($"{EnemyName} Planerar att attackera dig ({Damage} skada)");
                defending = false;
            }

            else if (randomNum > 100 && randomNum <= 125) // om det slumpade tallet är störe än 100 och mindre än 126 så laddar fienden up för en speciell attack
            {
                Console.WriteLine($"{EnemyName} Ladar up en speciell attack ");
                defending = false;
            }
        }

        if (EnemyTurn == true && Hp > 0 && howLongStund < 1) // om fienden lever och inte är lamslagen så gör den det den hade planerat
        {
            if (randomNum <= 50) 
            {
                Defend();
            }

            else if (randomNum > 50 && randomNum <= 100) 
            {
                Attack(player);
            }

            else
            {
                SpecialMove(player);
            }
        }

        EnemyTick(player); // om fienden har en debuff så kommer denns effekt aktiveras och hur länge den varar minskar
    }
}