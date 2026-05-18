public class Enemy
{
    private double maxHP = Random.Shared.Next(20, 31);
    private double hp;
    private double damage = 10;
    private double armor = 1;
    private double startArmor;
    private double goldDrop = 5;
    private double xpDrop = 4;
    protected double howLongStund;
    protected double howLongBurn;
    private double armorUpDuration;
    protected double difficultyMultiplier;
    protected double armorMultiplier;
    protected int randomMin;
    protected int randomMax;
    protected int randomNum;
    protected int chargeUp = 2;
    protected bool defending = false;
    protected bool hasRolled = false;
    protected static bool enemyTurn = false;
    protected string enemyName;

    List<string> monster = ["Slime", "skelett", "zombie", "vampyr", "varulv"];

    public Enemy(Player player) // ger vilka start värden/text variablerna ska ha 
    {
        maxHP += player.Level * 1.5;
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

            if (damage < 0)
            {
                damage = 0;
            }
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

            if (howLongBurn < 0)
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

    private void ArmordUpCheck() // kollar om fienden har fått mer armor och minskar rundorna som den har med ett 
    {
        if (armorUpDuration > 0) // om armor up är aktiv så minskar rundorna som den her med ett
        {
            armorUpDuration--;
        }
        else if (armorUpDuration <= 0) // om den inte är aktiv längre får fienden tillbaka sin vanliga armor
        {
            armor = startArmor;
        }
    }

    public void EnemyTick(Player player) // om fienden har en debuff så kommer denns effekt aktiveras och hur länge den varar minskar
    {
        if (howLongStund > 0) // om fienden är lamslagen så gör den inget och effekten minskar med ett
        {
            Console.WriteLine($"{enemyName} är lamslagen i {howLongStund} rundor");

            howLongStund--;
            hasRolled = false;
        }

        if (howLongBurn > 0) // om fienden briner så tar den skada vargje runda tills effekten avtar
        {
            Fireball fireball = (Fireball)player.Spell;

            Hp -= fireball.BurnDamage;
            howLongBurn--;
        }
    }

    public void Attack(Player player) // hur skada på spelaren räknas
    {
        if (player.PlayerDefending == false && player.Armor == null) // hur skada räknas om spelaren inte försvarar och inte har armor
        {
            player.Hp -= Damage;
            Console.WriteLine($"{enemyName} gjorde {Math.Round(Damage)} skada på dig");
        }
        else if (player.PlayerDefending == true && player.Armor != null) // hur skada räknas om spelaren försvarar och har armor
        {
            if ((Damage - player.Armor.Defens) * player.Block > 0) // kollar så att skadan är över 0 så att spelaren inte får hp om det är lägre
            {
                player.Hp -= Math.Round((Damage - player.Armor.Defens) * player.Block);
                Console.WriteLine($"{enemyName} gjorde {Math.Round((Damage - player.Armor.Defens) * player.Block)} skada på dig");
            }
            else
            {
                Console.WriteLine($"{enemyName} gjorde 0 skada på dig");
            }
        }
        else if (player.PlayerDefending == false && player.Armor != null) // hur skada räknas om spelaren inte försvarar och har armor
        {
            if (Damage - player.Armor.Defens > 0)
            {
                player.Hp -= Math.Round(Damage - player.Armor.Defens);
                Console.WriteLine($"{enemyName} gjorde {Math.Round(Damage - player.Armor.Defens)} skada på dig");
            }
            else
            {
                Console.WriteLine($"{enemyName} gjorde 0 skada på dig");
            }
        }
        else // hur skada räknas om spelaren försvarar och inte har armor
        {
            player.Hp -= Math.Round(Damage * player.Block);
            Console.WriteLine($"{enemyName} gjorde {Math.Round(Damage * player.Block)} skada på dig");
        }

        Console.WriteLine();

        ArmordUpCheck();
    }

    public void Defend() // gör så att fienden försvarar mot spelaren
    {
        defending = true;

        ArmordUpCheck();
    }

    public void ArmorUp(Enemy target) // ger en fiende mer armor under 2 rundor
    {
        defending = false;
        if (target.ArmorUpDuration <= 0) // om fienden redan har armor up så ökas bara hur länge effekten varar 
        {
            target.Armor *= armorMultiplier;
        }

        target.ArmorUpDuration += 2;

        Console.WriteLine($"{enemyName} använde armor up på {target.EnemyName}");

        ArmordUpCheck();
    }

    public void SpecialMove(Player player) // en speciell attack bara bossarna kan göra
    {
        defending = false;
        chargeUp--;
        randomMin = randomMax - 2;

        if (chargeUp <= 0) // om nedräkningen är klar så gör fienden dubelt så mycket skada än vanligt
        {
            Damage *= 2;
            player.Hp -= Damage;

            Damage /= 2;
            chargeUp = 2;

            randomMin = 0;
        }

        ArmordUpCheck();
    }

    public virtual void BattleLogic(Player player, Enemy target) // logiken för vad fienden kommer att göra under striden
    {
        if (enemyTurn == false) // när det inte är fiendens tur så kommer den slumpa vad den kommer att göra och skriva ut det till spelaren
        {
            if (hasRolled == false) // om fienden inte har slumpat vad den ska göra så gör den det 
            {
                randomNum = Random.Shared.Next(randomMin, randomMax + 1);
                hasRolled = true;
            }

            if (randomNum <= 50) // om det slumpade tallet är 50 eller mindre så försvarar fienden sig
            {
                Console.WriteLine($"{enemyName} planerar att försvara sig ");
                defending = true;
            }

            else if (randomNum > 50 && randomNum <= 100) // om det slumpade tallet är störe än 50 och mindre än 101 så attackera fienden
            {
                damage = Random.Shared.Next(5, (int)Math.Round(damage + (player.Level * 1.5f))); // slumpar fiendens skada
                defending = false;

                Console.WriteLine($"{enemyName} planerar att attackera dig ({Damage} skada)");
            }

            else if (randomNum > 100 && randomNum <= 125) // om det slumpade tallet är störe än 100 och mindre än 126 så ger fienden armor till en 
            {
                Console.WriteLine($"{enemyName} planerar att ge en fiende mer armor");
            }
        }

        if (enemyTurn == true && hp > 0 && howLongStund < 1) // om fienden lever och inte är lamslagen så gör den det den hade planerat
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

        EnemyTick(player);
    }
}