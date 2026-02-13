Player player = new();
Weapon weapon = new();
StrengthPotion strengthPotion = new();
Enemy enemy = new(player);
Tank tank = new(player);

// Console.WriteLine($"{weapon.Name}");
// Console.WriteLine($"{player.Damage}");
// Console.WriteLine($"{enemy.Armor}");

player.printFightActions();

player.PickAction(player);

Console.WriteLine($"{enemy.Hp} HP före");
player.ActionsForFight(weapon, strengthPotion, enemy, player.PlayerAction);
Console.WriteLine($"{enemy.Hp} HP efter");

Console.ReadLine();

