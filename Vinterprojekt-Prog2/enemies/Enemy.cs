public class Enemy
{
    private double maxHP = 25;
    private double hp;
    private double damage = 10;
    private double armor = 1;
    private double startArmor;
    private double goldDrop = 5;
    private double xpDrop = 4;
    private double howLongStund;
    private double howLongBurn;
    private double armorUpDuration;
    protected double difficultyMultiplier;
    protected double armorMultiplier;
    protected int randomMin;
    protected int randomMax;
    protected int randomNum;
    private int chargeUp = 2;
    private bool defending = false;
    protected bool EnemyTurn = false;
    protected string EnemyName;

    public Enemy(Player player)
    {
        maxHP = maxHP + (player.Level * 0.5f);
        maxHP = Math.Round(maxHP);

        hp = maxHP;

        damage = damage + (player.Level * 0.5f);
        damage = Math.Round(damage);

        armor = armor + player.Level;
        armor = Math.Round(armor);

        startArmor = armor;

        xpDrop = xpDrop + (player.Level * 0.5f);
        xpDrop = Math.Round(xpDrop);

        goldDrop = goldDrop + (player.Level * 0.5f);
        goldDrop = Math.Round(goldDrop);

        randomMin = 0;
        randomMax = 100;

        EnemyName = "Fiende: ";
    }

    public bool Defending
    {
        get => defending;
    }

    public double MaxHp
    {
        get => maxHP;

        protected set
        {
            maxHP = value;
        }
    }

    public double Hp
    {
        get => hp;

        set
        {
            hp = Math.Clamp(value, 0, maxHP);
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
        defending = false;

        ArmordUpCheck();
    }

    public void Defend()
    {
        defending = true;

        ArmordUpCheck();
    }

    public void ArmorUp(Enemy target)
    {
        if (target.ArmorUpDuration <= 0)
        {
            target.Armor = target.Armor * armorMultiplier;
        }
        target.ArmorUpDuration = 2;

        Console.WriteLine($"Enemy Tank used armor up on {target}");

        ArmordUpCheck();
    }

    public void SpecialMove(Player player)
    {
        chargeUp--;
        randomMin = randomMax;

        if (chargeUp <= 0)
        {
            Damage *= 2;
            player.Hp -= Damage;

            Damage /= 2;
            chargeUp = 2;

            randomMin = 0;
        }

        ArmordUpCheck();
    }

    private void ArmordUpCheck()
    {
        if (armorUpDuration > 0)
        {
            armorUpDuration--;
        }
        else if (armorUpDuration <= 0)
        {
            armor = startArmor;
        }
    }

    public virtual void BattleLogic(Player player, Enemy target)
    {
        if (EnemyTurn == false)
        {
            randomNum = Random.Shared.Next(randomMin, randomMax + 1);

            if (randomNum <= 50)
            {
                Console.WriteLine($"{EnemyName} planerar att försvara sig ");
            }

            else if (randomNum > 50 && randomNum <= 100)
            {
                Console.WriteLine($"{EnemyName} planerar att attackera dig ({Damage} skada)");
            }

            else if (randomNum > 100 && randomNum <= 125)
            {
                Console.WriteLine($"{EnemyName} planerar att ge en fiende mer armor");
            }
        }

        if (EnemyTurn == true)
        {
            if (randomNum <= 50)
            {
                Defend();
            }

            else if (randomNum > 50 && randomNum <= 100)
            {
                Attack(player);
            }

            else if (randomNum > 100 && randomNum <= 125)
            {
                ArmorUp(target);
            }

            else
            {
                SpecialMove(player);
            }
        }
    }
}