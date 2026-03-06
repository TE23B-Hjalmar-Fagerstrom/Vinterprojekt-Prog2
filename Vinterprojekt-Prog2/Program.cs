Player player = new();
StrengthPotion strengthPotion = new();
Enemy enemy = new(player);
Tank tank = new(player);

int random;

List<Enemy> enemiesAlive = [];
List<Enemy> enemiesDead = [];

for (int i = 0; i < 4; i++)
{
    Item newItem;
    
    random = Random.Shared.Next(1, 4);
    if (random == 1)
    {
        newItem = new Weapon();
    }
    else if (random == 2)
    {
        newItem = new Armor ();
    }
    else
    {
        random = Random.Shared.Next(1, 4);
        if (random == 1)
        {
            newItem = new HealtPotion();
        }
        else if (random == 2)
        {
            newItem = new ManaPotion();
        }
        else
        {
            newItem = new StrengthPotion();
        }
    }
    player.Inventory.Items.Add(newItem);

}

// Console.WriteLine($"{player.Weapon.Name}");
// Console.WriteLine($"{player.Damage}");
// Console.WriteLine($"{enemy.Armor}");
WorldActions(player);

while (player.PlayerAction == "lager")
{
    WorldActions(player);
}

Console.ReadLine();




static void WorldActions(Player player)
{
    player.printWorldActions();

    player.PickActionInWorld(player);

    Console.Clear();

    player.ActionsForWorld(player.PlayerAction);
}

static void FightActions(Player player, StrengthPotion strengthPotion, Enemy target)
{
    player.printFightActions();

    player.PickActionInFight(player);

    Console.Clear();

    player.ActionsForFight(player.Weapon, strengthPotion, target, player.PlayerAction);
}