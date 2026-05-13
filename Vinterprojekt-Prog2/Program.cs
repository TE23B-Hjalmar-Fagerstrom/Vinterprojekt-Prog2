Player player = new();
Enemy enemy = new(player);

int bossFightCountDown = 2;
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

while (player.Hp > 0) // så länge spelaren lever så fortsätter spelet
{
    Fight();
    rooms++;

    if (player.Hp > 0) // om spelaren lever så får den belöningar 
    {
        player.NewItem();

        if (player.Spell == null)
        {
            player.PickAbility();
        }

        player.WorldOrder(player);
    }
}

Console.WriteLine($"På vägen genom slotet mötte du samma öde som alla före och efter dig, döden. du kom till rum {rooms} och hade samlat {player.Gold} guld");

Console.ReadLine();

void spawnEnemy(Player player) // skapar en slumpad mängd fiender till striderna
{
    if (bossFightCountDown > 0) // om en boss inte ska skapas så görs vanliga fiender
    {
        for (int i = 0; i < Random.Shared.Next(1, 3 + (player.Level / 5)); i++) // gör 1 till 3 fiender och kan bli flera desto högre level spelaren är
        {
            if (Random.Shared.Next(1, 11) < 10) // slumpar om det ska vara en vanlig fiende eller tank
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
    else // skapar en boss efter ett visst antal strider
    {
        enemiesAlive.Add(new Boss(player));
        bossFightCountDown = 10;
    }
}

void Fight() // årdningen av hur striderna ska gå till
{
    player.Pick = -1;
    spawnEnemy(player); // skapar fienderna för striden

    while (player.Hp > 0 && enemiesAlive.Count > 0) // så länge spelaren lever och fiende listan inte är tom
    {
        while (enemy.EnemyTurn == false && player.Hp > 0 && enemiesAlive.Count > 0) // så länge det är spelarens omgång 
        {
            if (player.Pick < 0) // så länge spelaren inte har valt något
            {
                for (int i = 0; i < enemiesAlive.Count; i++) // skriver ut vad alla fienderna tänker att göra
                {
                    Console.WriteLine($"{i + 1}: {enemiesAlive[i].EnemyName} HP {enemiesAlive[i].Hp}");
                    enemiesAlive[i].BattleLogic(player, enemiesAlive[Random.Shared.Next(0, enemiesAlive.Count)]);
                    Console.WriteLine("");
                }
                if (enemiesAlive.Count > 0) // skriver ut information om och till spelaren om det fortfarande finns fiender
                {
                    Console.WriteLine($"du har {player.Hp} hp och {player.Mp} mana");
                    Console.WriteLine("");
                    Console.WriteLine("skriv nummret till vänster av fienden du vill attackera");

                    player.Pick = player.TryP(enemiesAlive.Count); // läser in spelarens val
                }
            }

            if (enemiesAlive.Count > 0) // gör det spelaren valde och kollar om fienderna överlevde eller inte
            {
                player.FightOrder(player.PlayerWeapon, player, player.StrengthPotion, enemiesAlive[player.Pick]);
                Console.WriteLine();

                for (int i = 0; i < enemiesAlive.Count; i++) // kollar om alla fiender lever
                {
                    if (enemiesAlive[i].Hp <= 0) // om fienden inte lever så tar den ur den och stoppar in det i död listan
                    {
                        Console.WriteLine($"{enemiesAlive[i].EnemyName} dräptes");
                        Console.WriteLine();

                        enemiesDead.Add(enemiesAlive[i]);
                        enemiesAlive.Remove(enemiesAlive[i]);
                    }
                }
            }
        }

        for (int i = 0; i < enemiesAlive.Count; i++) // gör alla fiendernas avsikter
        {
            enemiesAlive[i].BattleLogic(player, enemiesAlive[Random.Shared.Next(0, enemiesAlive.Count)]);
        }

        Console.WriteLine("Tryck enter för att fortsätta");

        Console.ReadLine();
        Console.Clear();

        player.Pick = -1;
        enemy.EnemyTurn = false;
    }

    for (int i = 0; i < enemiesDead.Count; i++) // när striden är slut så går den igenom död listan och ger xp och guldet spelaren ska få
    {
        Console.WriteLine($"du fick {enemiesDead[i].XpDrop} xp och {enemiesDead[i].GoldDrop} guld från {enemiesDead[i].EnemyName}");

        player.Xp += enemiesDead[i].XpDrop;
        player.Gold += (int)Math.Round(enemiesDead[i].GoldDrop);

        Console.WriteLine();
    }

    enemiesDead.Clear(); // tömmer död listan

    Console.WriteLine("Tryck enter för att välja din belöning");
    Console.ReadLine();
    Console.Clear();

    player.LifeStealDuration = 0;
    player.PotionDuration = 0;

    player.IsInFight = false;
    player.IsInWorld = true;
}
