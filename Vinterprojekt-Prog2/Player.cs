public class Player
{
    private float maxHP = 100;
    private float hp;
    private float maxMP = 30;
    private float mp;
    private BackPack inventory = new();
    private float xp;
    private int level;
    private int gold;

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

        set { inventory.Items.Add(value); }
    }


    public float Hp
    {
        get => hp;

        set
        {
            hp += value;
            if (hp < 0) hp = 0;
            if (hp > maxHP) hp = maxHP;
        }
    }

    public float Mp
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

        set
        {
            if (xp >= 15 * level * 1.25f)
            {
                xp -= 15 * level * 1.25f;
                level++;
            }
        }
    }

    public float Xp
    {
        get => xp;

        set
        {
            xp += value;
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
