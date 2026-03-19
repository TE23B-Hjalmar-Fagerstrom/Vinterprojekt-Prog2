Player player = new();
StrengthPotion strengthPotion = new();
Enemy enemy = new(player);
Tank tank = new(player);

List<Enemy> enemiesAlive = [];
List<Enemy> enemiesDead = [];

player.PickAbility();
player.NewItem();

player.WorldActions(player);

while (player.PlayerAction == "lager")
{
    player.WorldActions(player);
}

Console.ReadLine();
