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
    private Armor armor;
    private Abilitie spell;
    private StrengthPotion strengthPotion;
    private Enemy target;
    private string actions;
    private bool playerDefending;
    private bool hasUsedWorldAction = false;
    private bool isInWorld;
    private bool isInFight;

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
            if (spell != null)
            {
                spell?.UseAbilitie(target, this);
            }
            else
            {
                Console.WriteLine("Du har inte en trollformel att utföra");
            }
        };

        inFight["föremål"] = () =>
        {

        };

        inWorld["lager"] = () =>
        {
            if (inventory.Items.Count >= 1)
            {
                Console.WriteLine("Använder: ");
                Console.WriteLine($"Vapen: {weapon.Name} (kan göra {weapon.MinDamage} - {weapon.MaxDamage} skada)");
                if (armor == null)
                {
                    Console.WriteLine("Armor: ingen utrustad");
                }
                else
                {
                    Console.WriteLine($"Armor: {armor.Name} (blockar {armor.Defens} fysisk skada och {armor.MageArmor} magisk skada)");
                }
                if (spell != null)
                {
                    Console.WriteLine($"Förmåga: {spell.Name}");
                }
                Console.WriteLine();

                Console.WriteLine("I din ryggsäck:");
                inventory.Display();

                Console.WriteLine();
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
                    else if (inventory.Items[pick].ArmorBool == true)
                    {
                        inventory.EquipArmor(pick);

                        if (pick <= inventory.Items.Count)
                        {
                            if (armor != null)
                            {
                                inventory.Items.Add(armor);
                            }
                            armor = inventory.EquippedArmor.Dequeue();
                            Console.WriteLine($"Du utrustade {armor.Name}. tryck enter för att lämna denna skärm");

                            Console.ReadLine();
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Du kan inte använda den här. tryck enter för att lämna denna skärm");

                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("Du valde att fortsätta använda det du redan använde. tryck enter för att lämna denna skärm");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine("du har inget i din ryggsäck. tryck enter för att lämna denna skärm");
                Console.ReadLine();
                Console.Clear();
            }
        };

        inWorld["upgradera"] = () =>
        {
            if (hasUsedWorldAction == false)
            {
                float multiplier = Random.Shared.Next(1, 6);
                multiplier = 1 + (multiplier / 10);
                spell?.Upgrade(multiplier);

                hasUsedWorldAction = true;
            }
            else
            {
                Console.WriteLine("Du har redan gjort ditt engångs val");
            }
        };

        inWorld["villa"] = () =>
        {
            
        };

        inWorld["fortsätt"] = () =>
        {
            
        };
    }

    public bool PlayerDefending
    {
        get => playerDefending;
    }

    public bool IsInFight
    {
        get => isInFight;
    }
    
    public bool IsInWorld
    {
        get => isInWorld;
    }

    public string PlayerAction
    {
        get => actions;

        set
        {
            actions = value;
        }
    }

    public Weapon PlayerWeapon
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

    public void PrintFightActions()
    {
        foreach (string key in inFight.Keys)
        {
            Console.Write($"{key}:  ");
        }

        Console.WriteLine();
    }

    public void PrintWorldActions()
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

        while (!player.inFight.ContainsKey(actions))
        {
            Console.WriteLine("Du måste skriva ett av alternativen");
            player.PrintFightActions();
            actions = Console.ReadLine().ToLower();
        }
    }

    public void PickActionInWorld(Player player)
    {
        Console.WriteLine("skriv vad du vill göra");
        actions = Console.ReadLine().ToLower();

        while (!player.inWorld.ContainsKey(actions))
        {
            Console.WriteLine("Du måste skriva ett av alternativen");
            player.PrintWorldActions();
            actions = Console.ReadLine().ToLower();
        }
    }

    public void WorldActions(Player player)
    {
        player.PrintWorldActions();

        player.PickActionInWorld(player);

        Console.Clear();

        player.ActionsForWorld(player.PlayerAction);
    }

    public void FightActions(Player player, StrengthPotion strengthPotion, Enemy target)
    {
        player.PrintFightActions();

        player.PickActionInFight(player);

        Console.Clear();

        player.ActionsForFight(player.PlayerWeapon, strengthPotion, target, player.PlayerAction);
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

        Console.Clear();

        return pick - 1;
    }

    public void NewItem()
    {
        int amount = Random.Shared.Next(3, 6);

        List<Item> tempHolder = [];

        for (int i = 0; i < amount; i++)
        {
            int random = Random.Shared.Next(1, 5);

            Item newItem = random switch
            {
                1 => new Weapon(),
                2 => new Armor(),
                3 => new HealtPotion(),
                4 => new ManaPotion(),
                _ => new StrengthPotion(),
            };

            tempHolder.Add(newItem);
            Console.WriteLine($"{i + 1}: {tempHolder[i].Name} {tempHolder[i].Description}");

            if (i == amount - 1)
            {
                Console.WriteLine();
                Console.WriteLine("skriv numret till vänster om det föremål du vill ta");

                int pick = TryP(tempHolder.Count);
                Inventory.Items.Add(tempHolder[pick]);

                for (int a = 0; a < tempHolder.Count; a++)
                {
                    tempHolder.Remove(tempHolder[a]);
                }

                i++;
            }
        }
    }

    public void PickAbility()
    {
        List<Abilitie> tempHolder = [];
        int amount = 3;

        for (int i = 0; i < amount; i++)
        {
            Abilitie newAbility = i switch
            {
                1 => new Fireball(),
                2 => new HolyLight(),
                _ => new LifeSteal()
            };

            tempHolder.Add(newAbility);

            Console.WriteLine($"{i + 1}: {tempHolder[i].Description}");

            if (i == amount - 1)
            {
                Console.WriteLine();
                Console.WriteLine("skriv numret till vänster av förmågan du vill ha");

                int pick = TryP(tempHolder.Count);
                spell = tempHolder[pick];

                for (int a = 0; a < tempHolder.Count; a++)
                {
                    tempHolder.Remove(tempHolder[a]);
                }

                i++;
            }

        }
    }

    public Dictionary<string, Action> inFight = new();
    public Dictionary<string, Action> inWorld = new();
}