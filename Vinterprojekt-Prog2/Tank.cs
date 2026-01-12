public class Tank : Enemy
{
    private double armorMultiplier = 1.75;
    private double difficultyMultiplier = 1.25;

    public Tank(Player player) : base(player)
    {
        Armor = Armor * armorMultiplier;
        XpDrop = XpDrop * difficultyMultiplier;
        GoldDrop = GoldDrop * difficultyMultiplier;

        Math.Round(Armor);
        Math.Round(XpDrop);
        Math.Round(GoldDrop);
    }

    public void ArmorUp(Enemy target)
    {
        target.ArmorUpDuration = 2;
        target.Armor = Armor * armorMultiplier;
        
        Math.Round(target.Armor);
    }
}
