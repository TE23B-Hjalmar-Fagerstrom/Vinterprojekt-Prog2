using System.Security.Cryptography.X509Certificates;

public class Player
{
    private float maxHP = 100;
    private double hp;
    private float maxMP = 30;
    private double mp;
    private BackPack inventory = new();
    private double damage;
    private float xp;
    private int level;
    private int gold;
    private double lifeStealDuration;
    private double potionDuration;
    private Weapon weapon;
    private StrengthPotion strengthPotion;
    private Enemy target;

    public Player()
    {
        hp = maxHP;
        mp = maxMP;
        level = 1;
        gold = 5;
        damage = 10;

        inFight["attack"] = () =>
        {
            damage = Random.Shared.Next((int)weapon.MinDamage, (int)weapon.MaxDamage);

            if (potionDuration > 0)
            {
                damage *= strengthPotion.DamageMultiplierFromPotion;
            }

            damage = Math.Round(damage);

            if (target.Defending == false)
            {
                target.Hp -= damage - target.Armor;
            }
            else
            {
                Console.WriteLine($"{target.Armor}");
                target.Hp -= damage - Math.Round(target.Armor * 1.5);
                Console.WriteLine($"{target.Armor}");
            }
        };
    }

    public float MaxHP
    {
        get => maxHP;
    }

    public float MaxMP
    {
        get => maxMP;
    }

    public BackPack Inventory
    {
        get => inventory;
    }

    public double Damage
    {
        get => damage;

        set
        {
            damage = value;
        }
    }

    public double LifeStealDuration
    {
        get => lifeStealDuration;

        set
        {
            lifeStealDuration = value;
        }
    }

    public double PotionDuration
    {
        get => potionDuration;

        set
        {
            potionDuration = value;
        }
    }

    public double Hp
    {
        get => hp;

        set
        {
            hp += value;

            if (hp < 0) hp = 0;
            if (hp > maxHP) hp = maxHP;
        }
    }

    public double Mp
    {
        get => mp;

        set
        {
            if ((mp += value) >= 0)
            {
                mp += value;
            }

            if (mp > maxMP) mp = maxMP;
        }
    }

    public int Level
    {
        get => level;
    }

    public float Xp
    {
        get => xp;

        set
        {
            xp += value;

            if (xp >= 15 * level * 1.25f)
            {
                xp -= 15 * level * 1.25f;
                level++;
                maxHP++;
                maxMP++;
            }
        }
    }

    public int Gold
    {
        get => gold;

        set
        {
            if ((gold += value) >= 0)
            {
                gold += value;
            }
        }
    }

    public void LifeStealActiv(LifeSteal life)
    {
        if (lifeStealDuration > 0)
        {
            hp += damage * life.HelaAmount;
        }
    }

    public void ActionsForFight(Weapon weapon, StrengthPotion strengthPotion, Enemy target)
    {
        inFight["attack"]();

        inFight["defend"]();
        {

        }
    }

    public Dictionary<string, Action> inFight = new();
    public Dictionary<string, Action> inWorld = new();



}
