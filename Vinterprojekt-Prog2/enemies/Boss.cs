public class Boss : Enemy
{
    public Boss(Player player) : base(player)
    {
        randomMax = 150;
        difficultyMultiplier = 1.75;
        MaxHp *= difficultyMultiplier;
        Hp = MaxHp;
        Armor *= difficultyMultiplier;
        XpDrop *= difficultyMultiplier;
        GoldDrop *= 2;

        while (randomNum > 100 && randomNum <= 125)
        {
            randomNum = Random.Shared.Next(randomMin, randomMax + 1);
        }

    }

    public override void BattleLogic()
    {
        
    }
}
