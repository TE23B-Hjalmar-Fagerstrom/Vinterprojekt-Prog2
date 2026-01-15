Player player = new();
Enemy enemy = new(player);
Tank tank = new(player);

Console.WriteLine($"{enemy.Armor}");

tank.ArmorUp(enemy);

Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine($"{enemy.Armor}");
Console.WriteLine($"{enemy.ArmorUpDuration}");

Console.ReadLine();