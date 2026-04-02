public class Boss : Enemy
{
    static int bossCount;

    public Boss(Player player) : base(player)
    {
        randomMax = 125;
        difficultyMultiplier = 1.75;
        MaxHp *= difficultyMultiplier;
        Hp = MaxHp;
        Armor *= difficultyMultiplier;
        XpDrop *= difficultyMultiplier;
        GoldDrop *= 2;
        bossCount++;

        if (bossCount < 5)
        {
            EnemyName = "Drakunge";
        }
        else if (bossCount < 10)
        {
            EnemyName = "Drake";
        }
        else
        {
            EnemyName = "Äldre Drake";
        }
    }

    public override void BattleLogic(Player player, Enemy target)
    {
        if (EnemyTurn == false)
        {
            if (hasRolled == false)
            {
                randomNum = Random.Shared.Next(randomMin, randomMax + 1);
            }

            if (randomNum <= 50)
            {
                Console.WriteLine($"{EnemyName} Planerar att försvara sig ");
            }

            else if (randomNum > 50 && randomNum <= 100)
            {
                Console.WriteLine($"{EnemyName} Planerar att attackera dig ({Damage} skada)");
            }

            else if (randomNum > 100 && randomNum <= 125)
            {
                Console.WriteLine($"{EnemyName} Ladar up en speial attack ");
            }
        }

        if (EnemyTurn == true && Hp > 0)
        {
            if (randomNum <= 50)
            {
                Defend();
            }

            else if (randomNum > 50 && randomNum <= 100)
            {
                Attack(player);
            }

            else if (randomNum > 100 && randomNum <= 125)
            {
                ArmorUp(target);
            }

            else
            {
                SpecialMove(player);
            }
        }
    }
}