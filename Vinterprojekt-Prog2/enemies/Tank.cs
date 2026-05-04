public class Tank : Enemy
{
    public Tank(Player player) : base(player) // ger vilka start värden/text variablerna ska ha 
    {
        randomMax = 125;
        armorMultiplier = 1.75;
        difficultyMultiplier = 1.25;
        Armor *= armorMultiplier;
        XpDrop *= difficultyMultiplier;
        GoldDrop *= difficultyMultiplier;

        Armor = Math.Round(Armor);
        XpDrop = Math.Round(XpDrop);
        GoldDrop = Math.Round(GoldDrop);

        EnemyName = "Spök riddare: ";
    }
}