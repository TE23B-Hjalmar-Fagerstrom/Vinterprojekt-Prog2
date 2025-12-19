public class Enemy
{
    private double maxHP = 100;
    private double hp;
    private double damage = 10;
    private double armor = 1;
    private int goldDrop = 5;
    private double xpDrop = 4;
    private int howLongStund;

    public Enemy(Player player1)
    {
        maxHP = maxHP * (player1.Level * 0.5f);
        maxHP = Math.Round(maxHP);

        hp = maxHP;

        damage = damage * (player1.Level * 0.5f);
        damage = Math.Round(damage);

        armor = armor * (player1.Level * 0.5f);
        armor = Math.Round(armor);

        xpDrop = xpDrop * (player1.Level * 0.5f);
        xpDrop = Math.Round(xpDrop);

        goldDrop = goldDrop + player1.Level;
    }

    public double Hp
    {
        get => hp;

        set
        {
            hp += value;

            if (hp < 0)
            {
                hp = 0;
            }
        }
    }

    public double Damage
    {
        get => damage;
    }

    public int GoldDrop
    {
        get => goldDrop;
    }

    public double XpDrop
    {
        get => xpDrop;
    }

    public int HowLongStund
    {
        get => howLongStund;

        set
        {
            howLongStund += value;

            if (howLongStund <= 0)
            {
                howLongStund = 0;
            }
        }
    }

    public void Attack()
    {

    }

    public void Defend()
    {

    }
}
