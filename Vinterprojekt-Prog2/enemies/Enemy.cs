public class Enemy
{
    private double maxHP = 100;
    private double hp;
    private double damage = 10;
    private double armor = 1;
    private double goldDrop = 5;
    private double xpDrop = 4;
    private double howLongStund;
    private double howLongBurn;
    private double armorUpDuration;

    public Enemy(Player player)
    {
        maxHP = maxHP + (player.Level * 0.5f);
        maxHP = Math.Round(maxHP);

        hp = maxHP;

        damage = damage + (player.Level * 0.5f);
        damage = Math.Round(damage);

        armor = armor * player.Level;
        armor = Math.Round(armor);

        xpDrop = xpDrop + (player.Level * 0.5f);
        xpDrop = Math.Round(xpDrop);

        goldDrop = goldDrop + (player.Level * 0.5f);
        goldDrop = Math.Round(goldDrop);
    }

    public double Hp
    {
        get => hp;

        set
        {
            hp += value;

            if (hp < 0) hp = 0;
        }
    }

    public double Damage
    {
        get => damage;

        protected set
        {
            damage = value;
        }
    }


    public double GoldDrop
    {
        get => goldDrop;

        protected set
        {
            goldDrop = value;
        }
    }

    public double XpDrop
    {
        get => xpDrop;

        protected set
        {
            xpDrop = value;
        }
    }

    public double Armor
    {
        get => armor;

        set
        {
            armor = value;
            armor = Math.Round(armor);
        }
    }

    public double HowLongStund
    {
        get => howLongStund;

        set
        {
            howLongStund = value;

            if (howLongStund <= 0)
            {
                howLongStund = 0;
            }
        }
    }

    public double HowLongBurn
    {
        get => howLongBurn;

        set
        {
            howLongBurn = value;

            if (howLongBurn <= 0)
            {
                howLongBurn = 0;
            }
        }
    }

    public double ArmorUpDuration
    {
        get => armorUpDuration;

        set
        {
            armorUpDuration = value;

            if (armorUpDuration <= 0)
            {
                armorUpDuration = 0;
            }
        }
    }

    public void Attack(Player player)
    {

        ArmordUpCheck(player);
    }

    public void Defend(Player player)
    {

        ArmordUpCheck(player);
    }

    private void ArmordUpCheck(Player player)
    {
        if (armorUpDuration > 0)
        {
            armorUpDuration--;
        }
        else if (armorUpDuration <= 0)
        {
            armor = armor * (player.Level * 0.5f);
            armor = Math.Round(armor);
        }
    }
}
