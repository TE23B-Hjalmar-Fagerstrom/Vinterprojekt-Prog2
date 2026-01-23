public class Boss : Enemy
{
    private double difficultyMultiplier = 1.75;
    private double goldMultiplier = 2;
    private int chargeUp = 2;

    public Boss(Player player) : base(player)
    {
        Hp *= difficultyMultiplier;
        Armor *= difficultyMultiplier;
        XpDrop *= difficultyMultiplier;
        GoldDrop *= goldMultiplier;
    }

    public void SpecialMove(Player target)
    {
        chargeUp--;

        if (chargeUp <= 0)
        {
            Damage *= 2;
            target.Hp -= Damage;
            
            Damage /= 2;
            chargeUp = 2;
        }
    }
}
