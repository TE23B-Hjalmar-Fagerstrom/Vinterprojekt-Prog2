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
    protected bool hasRolled = false;
    protected static bool enemyTurn = false;
    protected string enemyName;

    List<string> monster = ["Slime", "skelett", "zombie", "vampyr", "varulv"];

    public Enemy(Player player)
    {
        maxHP += player.Level * 1.5f;
        maxHP = Math.Round(maxHP);

        hp = maxHP;

        damage += player.Level * 1.5f;
        damage = Math.Round(damage);

        armor += player.Level;
        armor = Math.Round(armor);

        startArmor = armor;

        xpDrop += Random.Shared.Next(-player.Level, (int)Math.Round(player.Level * 1.5f));
        xpDrop = Math.Round(xpDrop);

        goldDrop += Random.Shared.Next(-player.Level, (int)Math.Round(player.Level * 1.5f));
        goldDrop = Math.Round(goldDrop);

        randomMin = 0;
        randomMax = 100;

        enemyName = $"{monster[Random.Shared.Next(0, monster.Count)]}: ";
    }

    public bool Defending
    {
        get => defending;
    }

    public bool EnemyTurn
    {
        get => enemyTurn;

        set
        {
            enemyTurn = value;
        }
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

    public string EnemyName
    {
        get => enemyName;

        set
        {
            enemyName = value;
        }
    }

    public void Attack(Player player)
    {
        defending = false;
        if (player.PlayerDefending == false)
        {
            player.Hp -= damage;
            Console.WriteLine($"{enemyName} gjorde {Math.Round(damage)} skada på dig");
        }
        else if (player.PlayerDefending == true && player.Armor != null)
        {
            player.Hp -= Math.Round((damage - player.Armor.Defens) * player.Block);
            Console.WriteLine($"{enemyName} gjorde {Math.Round((damage - player.Armor.Defens) * player.Block)} skada på dig");
        }
        else if (player.PlayerDefending == false && player.Armor != null)
        {
            player.Hp -= Math.Round(damage - player.Armor.Defens);
            Console.WriteLine($"{enemyName} gjorde {Math.Round(damage - player.Armor.Defens)} skada på dig");
        }
        else
        {
            player.Hp -= Math.Round(damage * player.Block);
            Console.WriteLine($"{enemyName} gjorde {Math.Round(damage * player.Block)} skada på dig");
        }

        Console.WriteLine();

        ArmordUpCheck();
    }

    public void Defend()
    {
        defending = true;

        ArmordUpCheck();
    }

    public void ArmorUp(Enemy target)
    {
        defending = false;
        if (target.ArmorUpDuration <= 0)
        {
            target.Armor = target.Armor * armorMultiplier;
        }
        target.ArmorUpDuration = 2;

        Console.WriteLine($"{enemyName} använde armor up på {target.EnemyName}");

        ArmordUpCheck();
    }

    public void SpecialMove(Player player)
    {
        defending = false;
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
        if (enemyTurn == false)
        {
            if (hasRolled == false)
            {
                randomNum = Random.Shared.Next(randomMin, randomMax + 1);
            }

            if (randomNum <= 50)
            {
                Console.WriteLine($"{enemyName} planerar att försvara sig ");
                defending = true;
                hasRolled = true;
            }

            else if (randomNum > 50 && randomNum <= 100)
            {
                if (hasRolled == false)
                {
                    damage = Random.Shared.Next(5, (int)Math.Round(damage + (player.Level * 1.5f)));
                }

                hasRolled = true;

                Console.WriteLine($"{enemyName} planerar att attackera dig ({Damage} skada)");
            }

            else if (randomNum > 100 && randomNum <= 125)
            {
                Console.WriteLine($"{enemyName} planerar att ge en fiende mer armor");

                hasRolled = true;
            }
        }

        if (enemyTurn == true && hp > 0)
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

            hasRolled = false;
        }
    }
}