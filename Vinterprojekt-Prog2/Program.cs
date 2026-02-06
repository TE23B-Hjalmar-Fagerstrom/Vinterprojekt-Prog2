Player player = new();
Weapon weapon = new();
StrengthPotion strengthPotion = new();
Enemy enemy = new(player);
Tank tank = new(player);

Console.WriteLine($"{weapon.Name}");

player.printFightActions();

player.PickAction(player);

Console.WriteLine($"{enemy.Hp} HP Before");
player.ActionsForFight(weapon, strengthPotion, enemy, player.PlayerAction);
Console.WriteLine($"{enemy.Hp} HP After");

Console.ReadLine();

