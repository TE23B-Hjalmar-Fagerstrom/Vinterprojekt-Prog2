public class Tank : Enemy
{
    public Tank(Player player) : base(player) // ger vilka start värden/text variablerna ska ha 
    {
        randomMax = 125;
        armorMultiplier = 1.75;
        difficultyMultiplier = 1.25;
        
        MaxHp = Math.Round(MaxHp * difficultyMultiplier);
        Armor = Math.Round(Armor * armorMultiplier);
        XpDrop = Math.Round(XpDrop * difficultyMultiplier);
        GoldDrop = Math.Round(GoldDrop * difficultyMultiplier);

        EnemyName = "Spök riddare: ";
    }
}