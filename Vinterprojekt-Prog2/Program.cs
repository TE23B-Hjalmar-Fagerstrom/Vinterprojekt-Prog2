Weapon weapon = new();
Player player = new();
Item item = new();
Rarity rarity = new();
HealtPotion healtPotion = new();

Console.WriteLine(healtPotion.Name);
Console.WriteLine(healtPotion.UsesDuration);

Console.ReadLine();

player.Damage = Random.Shared.Next((int)weapon.MinDamage, (int)weapon.MaxDamage + 1);

Console.WriteLine(player.Damage);

Console.ReadLine();


Console.WriteLine(rarity.RarityLevel);

Console.WriteLine(item.Name);
Console.WriteLine(item.BuyCost);
Console.WriteLine(item.SellCost);

Console.ReadLine();