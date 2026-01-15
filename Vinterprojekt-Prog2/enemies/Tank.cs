public class Tank : Enemy
{
    private double armorMultiplier = 1.75f;
    private double difficultyMultiplier = 1.25;

    public Tank(Player player) : base(player)
    {
        Armor = Armor * armorMultiplier;
        XpDrop = XpDrop * difficultyMultiplier;
        GoldDrop = GoldDrop * difficultyMultiplier;

        Armor = Math.Round(Armor);
        XpDrop = Math.Round(XpDrop);
        GoldDrop = Math.Round(GoldDrop);
    }

    public void ArmorUp(Enemy target)
    {
        target.ArmorUpDuration = 2;
        target.Armor = target.Armor * armorMultiplier;

        Console.WriteLine($"Enemy Tank used armor up on {target}");
    }
}
