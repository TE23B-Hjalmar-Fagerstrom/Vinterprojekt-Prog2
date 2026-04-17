Player player = new();
StrengthPotion strengthPotion = new();
Enemy enemy = new(player);
Tank tank = new(player);

int bossFightCountDown = 10;
int rooms = 0;

List<Enemy> enemiesAlive = [];
List<Enemy> enemiesDead = [];

Console.WriteLine("Du är en fattig bonde under medeltiden på jakt efter förmögenhet och en dag stöte");
Console.WriteLine("du på en gammal övergivet slot som kan hålla många olicka skater som bara");
Console.WriteLine("väntar, men det finns ryckten om att det kan finnas monster som vaktar skaterna");
Console.WriteLine("");
Console.WriteLine("tryck enter för att fortsätta");
Console.ReadLine();
Console.Clear();

while (player.Hp > 0)
{
    player.NewItem();
    player.NewItem();
    player.NewItem();
    player.NewItem();
    player.NewItem();
    player.NewItem();
    player.NewItem();
    player.NewItem();
    player.NewItem();



    Fight();
    rooms++;

    if (player.Hp > 0)
    {
        player.NewItem();

        if (player.Spell == null)
        {
            player.PickAbility();
        }

        player.WorldActions(player);
    }
}

Console.WriteLine($"På vägen genom slotet mötte du samma öde som alla före och efter dig, döden. du kom till rum {rooms} och hade samlat {player.Gold} guld");

Console.ReadLine();

void spawnEnemy(Player player)
{
    if (bossFightCountDown >= 1)
    {
        for (int i = 0; i < Random.Shared.Next(1, 4); i++)
        {
            if (Random.Shared.Next(1, 11) < 10)
            {
                enemiesAlive.Add(new Enemy(player));
            }
            else
            {
                enemiesAlive.Add(new Tank(player));
            }
        }

        bossFightCountDown--;
    }
    else
    {
        enemiesAlive.Add(new Boss(player));
        bossFightCountDown = 10;
    }
}

void Fight()
{
    player.Pick = -1;
    spawnEnemy(player);

    while (player.Hp > 0 && enemiesAlive.Count > 0)
    {
        while (enemy.EnemyTurn == false && player.Hp > 0 && enemiesAlive.Count > 0)
        {
            if (player.Pick < 0)
            {
                for (int i = 0; i < enemiesAlive.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {enemiesAlive[i].EnemyName} HP {enemiesAlive[i].Hp}");
                    enemiesAlive[i].BattleLogic(player, enemiesAlive[Random.Shared.Next(0, enemiesAlive.Count)]);
                    Console.WriteLine("");
                }
                if (enemiesAlive.Count > 0)
                {
                    Console.WriteLine($"du har {player.Hp} hp");
                    Console.WriteLine("");
                    Console.WriteLine("skriv nummret till vänster av fienden du vill attackera");

                    player.Pick = player.TryP(enemiesAlive.Count);
                }
            }

            if (enemiesAlive.Count > 0)
            {
                player.FightActions(player.PlayerWeapon, player, strengthPotion, enemiesAlive[player.Pick]);
                Console.WriteLine();

                for (int i = 0; i < enemiesAlive.Count; i++)
                {
                    if (enemiesAlive[i].Hp <= 0)
                    {
                        Console.WriteLine($"{enemiesAlive[i].EnemyName} dräptes");
                        Console.WriteLine();

                        enemiesDead.Add(enemiesAlive[i]);
                        enemiesAlive.Remove(enemiesAlive[i]);
                    }
                }

                Console.WriteLine($"enemy turn {enemy.EnemyTurn}");
            }
        }

        for (int i = 0; i < enemiesAlive.Count; i++)
        {
            enemiesAlive[i].BattleLogic(player, enemiesAlive[Random.Shared.Next(0, enemiesAlive.Count)]);
        }

        Console.WriteLine("Tryck enter för att fortsätta");

        Console.ReadLine();
        Console.Clear();

        player.Pick = -1;
        enemy.EnemyTurn = false;
    }

    for (int i = 0; i < enemiesDead.Count; i++)
    {
        player.Xp += enemiesDead[i].XpDrop;
        player.Gold += (int)Math.Round(enemiesDead[i].GoldDrop);
        Console.WriteLine($"du fick {enemiesDead[i].XpDrop} xp och {enemiesDead[i].GoldDrop} guld från {enemiesDead[i].EnemyName}");
        Console.WriteLine();
    }

    while (enemiesDead.Count > 0)
    {
        enemiesDead.RemoveAt(0);
    }

    Console.WriteLine("Tryck enter för att välja din belöning");
    Console.ReadLine();
    Console.Clear();

    player.IsInFight = false;
    player.IsInWorld = true;
}