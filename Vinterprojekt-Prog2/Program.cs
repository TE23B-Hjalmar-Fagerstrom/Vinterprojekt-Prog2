Player player = new();
StrengthPotion strengthPotion = new();
Enemy enemy = new(player);
Tank tank = new(player);

int bossFightCountDown = 10;

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
    Fight();

    if (player.Hp > 0)
    {
        if (player.Spell == null)
        {
            player.PickAbility();
        }
        player.NewItem();

        player.WorldActions(player);
    }

}

Console.ReadLine();

void spawnEnemy(Player player)
{
    if (bossFightCountDown >= 1)
    {
        for (int i = 0; i < 4; i++)
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
    spawnEnemy(player);

    while (player.Hp > 0 || enemiesAlive.Count > 0)
    {
        while (enemy.EnemyTurn == false)
        {
            for (int i = 0; i < enemiesAlive.Count; i++)
            {
                Console.WriteLine($"{i}: {enemiesAlive[i].EnemyName}");
                enemiesAlive[i].BattleLogic(player, enemiesAlive[Random.Shared.Next(0, enemiesAlive.Count)]);
                Console.WriteLine("");
            }

            player.FightActions(player.PlayerWeapon, strengthPotion, enemiesAlive[player.TryP(enemiesAlive.Count)], player.PlayerAction);
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
        }

        for (int i = 0; i < enemiesAlive.Count; i++)
        {
            enemiesAlive[i].BattleLogic(player, enemiesAlive[Random.Shared.Next(0, enemiesAlive.Count)]);
        }


    }

}