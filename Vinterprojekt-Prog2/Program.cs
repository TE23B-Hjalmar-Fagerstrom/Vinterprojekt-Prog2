Item item = new();
Rarity rarity = new();

Console.WriteLine(rarity.RarityLevel);

item.Name = "sword";
item.BuyCost = 20;
item.SellCost = 10;

Console.WriteLine(item.Name);
Console.WriteLine(item.BuyCost);
Console.WriteLine(item.SellCost);

Console.ReadLine();