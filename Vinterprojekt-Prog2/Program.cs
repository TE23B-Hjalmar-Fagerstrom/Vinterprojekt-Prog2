Player player = new();
StrengthPotion strengthPotion = new();
Enemy enemy = new(player);
Tank tank = new(player);

player.Inventory.Display();

// Console.WriteLine($"{player.Weapon.Name}");
// Console.WriteLine($"{player.Damage}");
// Console.WriteLine($"{enemy.Armor}");

// player.printFightActions();

player.PickAction(player);

Console.WriteLine($"{enemy.Hp} HP före");
player.ActionsForFight(player.Weapon, strengthPotion, enemy, player.PlayerAction);
Console.WriteLine($"{enemy.Hp} HP efter");

Console.ReadLine();