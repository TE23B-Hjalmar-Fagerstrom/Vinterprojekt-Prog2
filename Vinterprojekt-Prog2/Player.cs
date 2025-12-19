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

    public Player()
    {
        hp = maxHP;
        mp = maxMP;
        level = 1;
        gold = 5;
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

    public Dictionary<string, Action> inFight = new();
    public Dictionary<string, Action> inWorld = new();
}
