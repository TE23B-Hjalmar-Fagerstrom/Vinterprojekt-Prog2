Player player = new();
StrengthPotion strengthPotion = new();
Enemy enemy = new(player);
Tank tank = new(player);

int random = 0;
int amount = 0;

List<Enemy> enemiesAlive = [];
List<Enemy> enemiesDead = [];

NewItem(player, random, amount);

// Console.WriteLine($"{player.Weapon.Name}");
// Console.WriteLine($"{player.Damage}");
// Console.WriteLine($"{enemy.Armor}");
player.WorldActions(player);

while (player.PlayerAction == "lager")
{
    player.WorldActions(player);
}

Console.ReadLine();

static Player NewItem(Player player, int random, int amount)
{
    amount = Random.Shared.Next(5, 11);

    for (int i = 0; i < amount; i++)
    {
        Item newItem;

        random = Random.Shared.Next(1, 4);
        if (random == 1)
        {
            newItem = new Weapon();
        }
        else if (random == 2)
        {
            newItem = new Armor();
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

    return player;
}