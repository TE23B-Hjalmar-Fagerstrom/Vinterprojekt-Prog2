Player player = new();
Weapon weapon = new();
StrengthPotion strengthPotion = new();
Enemy enemy = new(player);
Tank tank = new(player);

Console.WriteLine($"{weapon.MinDamage}");

enemy.Defending = true;
player.ActionsForFight(weapon, strengthPotion, enemy);

Console.ReadLine();