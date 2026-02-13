public class Tank : Enemy
{
    public Tank(Player player) : base(player)
    {
        randomMax = 125;
        armorMultiplier = 1.75;
        difficultyMultiplier = 1.25;
        Armor = Armor * armorMultiplier;
        XpDrop = XpDrop * difficultyMultiplier;
        GoldDrop = GoldDrop * difficultyMultiplier;

        Armor = Math.Round(Armor);
        XpDrop = Math.Round(XpDrop);
        GoldDrop = Math.Round(GoldDrop);

        EnemyName = "Tank: ";
    }

}
