public class Player
{
    private float maxHP = 100;
    private double hp;
    private float maxMP = 30;
    private double mp;
    private double damage;
    private float xp;
    private int level = 1;
    private int gold = 5;
    private int pick;
    private double lifeStealDuration;
    private double potionDuration;
    private BackPack inventory = new();
    private Weapon weapon;
    private StrengthPotion strengthPotion;
    private Enemy target;
    private string actions;
    private bool playerDefending;

    public Player()
    {
        hp = maxHP;
        mp = maxMP;
        weapon = inventory.EquippedWeapon.Dequeue();

        inFight["attackera"] = () =>
        {
            playerDefending = false;

            damage = Random.Shared.Next((int)weapon.MinDamage + level, (int)weapon.MaxDamage + level + 1);

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
                target.Hp -= damage - Math.Round(target.Armor * 1.5);
            }
        };

        inFight["försvara"] = () =>
        {
            playerDefending = true;
        };

        inFight["magi"] = () =>
        {
            playerDefending = true;
        };

        inFight["föremål"] = () =>
        {

        };

        inWorld["lager"] = () =>
        {
            Console.WriteLine($"Använder: {weapon.Name} (kan göra {weapon.MinDamage} - {weapon.MaxDamage} skada)");
            Console.WriteLine();

            Console.WriteLine("I din ryggsäck:");
            inventory.Display();

            Console.WriteLine("skriv numret som står till vänster av föremålet du vill utrusta.");
            pick = TryP(inventory.Items.Count + 1);

            if (pick < inventory.Items.Count)
            {
                if (inventory.Items[pick].WeaponBool == true)
                {
                    inventory.EquipWeapon(pick);

                    if (pick <= inventory.Items.Count)
                    {
                        inventory.Items.Add(weapon);
                        weapon = inventory.EquippedWeapon.Dequeue();
                        Console.WriteLine($"Du utrustade {weapon.Name}. tryck enter för att lämna denna skärm");

                        Console.ReadLine();
                        Console.Clear();
                    }
                }
            }
            else
            {
                Console.WriteLine("Du valde att fortsätta använda vapnet du redan använde. tryck enter för att lämna denna skärm");
                Console.ReadLine();
                Console.Clear();
            }
        };
    }

    public bool PlayerDefending
    {
        get => playerDefending;
    }

    public string PlayerAction
    {
        get => actions;

        set
        {
            actions = value;
        }
    }

    public Weapon Weapon
    {
        get => weapon;
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
            hp = Math.Clamp(value, 0, maxHP);
        }
    }

    public double Mp
    {
        get => mp;

        set
        {
            mp = Math.Clamp(value, 0, maxMP);
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
            xp = value;

            if (xp >= 15 * level * 1.25f)
            {
                xp -= 15 * level * 1.25f;
                level++;
                maxHP += 2;
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
                gold = value;
            }
        }
    }

    public void LifeStealActiv(LifeSteal life)
    {
        if (lifeStealDuration > 0)
        {
            hp = damage * life.HelaAmount;
        }
    }

    public void ActionsForFight(Weapon weapon, StrengthPotion strengthPotion, Enemy target, string actions)
    {
        this.target = target;

        if (actions == "attack")
        {
            inFight["attack"]();
        }

        else if (actions == "defend")
        {
            inFight["defend"]();
        }

        else if (actions == "items")
        {
            inFight["items"]();
        }

        this.target = null;
    }

    public void ActionsForWorld(string actions)
    {
        if (actions == "lager")
        {
            inWorld["lager"]();
        }
    }

    public void printFightActions()
    {
        foreach (string key in inFight.Keys)
        {
            Console.Write($"{key}:  ");
        }

        Console.WriteLine();
    }

    public void printWorldActions()
    {
        foreach (string key in inWorld.Keys)
        {
            Console.Write($"{key}:  ");
        }

        Console.WriteLine();
    }

    public void PickActionInFight(Player player)
    {
        Console.WriteLine("skriv vad du vill göra");
        actions = Console.ReadLine().ToLower();

        while (!player.inFight.Keys.Contains(actions))
        {
            Console.WriteLine("Du måste skriva ett av alternativen");
            player.printFightActions();
            actions = Console.ReadLine().ToLower();
        }
    }

    public void PickActionInWorld(Player player)
    {
        Console.WriteLine("skriv vad du vill göra");
        actions = Console.ReadLine().ToLower();

        while (!player.inWorld.Keys.Contains(actions))
        {
            Console.WriteLine("Du måste skriva ett av alternativen");
            player.printWorldActions();
            actions = Console.ReadLine().ToLower();
        }
    }

    public int TryP(int countInItems) // TryP = TryParse
    {
        int pick = -10;

        while (pick < 1 || pick > countInItems)
        {
            string pickText = Console.ReadLine();
            int.TryParse(pickText, out pick);

            if (pick < 1 || pick > countInItems)
            {
                Console.WriteLine("Du måste skriva ett giltigt tal");
            }
        }

        return pick - 1;
    }

    public Dictionary<string, Action> inFight = new();
    public Dictionary<string, Action> inWorld = new();
}