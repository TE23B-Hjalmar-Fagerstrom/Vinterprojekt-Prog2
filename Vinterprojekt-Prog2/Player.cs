public class Player
{
    private float maxHP = 100;
    private double hp;
    private float maxMP = 30;
    private double mp;
    private double damage;
    private double block;
    private double xp;
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
    private bool tutorial = true;

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

            if (target?.Defending == false)
            {
                Damage -= target.Armor;

                target.Hp -= damage;
                Console.WriteLine($"Du gjorde {damage} skada på {target.EnemyName}");

                if (lifeStealDuration > 0)
                {
                    LifeSteal life = (LifeSteal)spell;

                    Hp += damage * life.HelaAmount;

                    Console.WriteLine($"och du fick {damage * life.HelaAmount}");
                }

                damage += target.Armor;

                Console.WriteLine();
            }
            else
            {
                Damage -= Math.Round(target.Armor * 1.5);
                target.Hp -= damage;
                Console.WriteLine($"Du gjorde {damage} skada på {target.EnemyName}");
                Console.WriteLine();

                if (lifeStealDuration > 0)
                {
                    LifeSteal life = (LifeSteal)spell;

                    Hp += damage * life.HelaAmount;

                    Console.WriteLine($"och du fick {damage * life.HelaAmount}");
                }

                Damage += Math.Round(target.Armor * 1.5);
            }

            target.EnemyTurn = true;
        };

        inFight["försvara"] = () =>
        {
            playerDefending = true;
            block = 0.7;
            target.EnemyTurn = true;
        };

        inFight["magi"] = () =>
        {
            if (spell != null)
            {
                if (mp >= spell.ManaCost)
                {
                    spell.UseAbilitie(target, this);
                    target.EnemyTurn = true;
                }
                else
                {
                    spell.UseAbilitie(target, this);
                }
            }
            else
            {
                Console.WriteLine("Du har inte en trollformel att utföra");
                Console.WriteLine("tryck enter för att lämna denna skärm");

                Console.ReadLine();
            }
        };

        inFight["föremål"] = () =>
        {
            List<Consumable> tempHolder = [];

            for (int i = 0; i < inventory.Items.Count; i++)
            {
                if (inventory.Items[i].ConsumableBool == true)
                {
                    tempHolder.Add((Consumable)inventory.Items[i]);
                    inventory.Items.Remove(inventory.Items[i]);
                    i--;
                }
            }

            if (tempHolder.Count > 0)
            {
                for (int i = 0; i < tempHolder.Count; i++)
                {
                    Console.WriteLine($"{i + 1})  {tempHolder[i].Name}: {tempHolder[i].Description}");
                }

                Console.WriteLine($"{tempHolder.Count + 1}) Backa");
                Console.WriteLine();
                Console.WriteLine("skriv nummret till vänster av föremålet du vill använda");

                pick = TryP(tempHolder.Count + 1);

                if (pick < tempHolder.Count)
                {
                    tempHolder[pick].Use(this);
                }

                if (strengthPotion == null)
                {
                    if (potionDuration > 0)
                    {
                        strengthPotion = (StrengthPotion)tempHolder[pick];
                    }
                }

                if (potionDuration <= 0)
                {
                    strengthPotion = null;
                }

                for (int i = 0; i < tempHolder.Count; i++)
                {
                    if (tempHolder[i].UsesCurent > 0)
                    {
                        inventory.Items.Add(tempHolder[i]);
                    }
                }

                tempHolder.Clear();
                pick = -10;
            }
            else
            {
                Console.WriteLine("Du har inga föremål");
                Console.WriteLine("Tryck enter för att lämna denna skärm");
                Console.ReadLine();
                Console.Clear();
            }
        };

        inFight["backa"] = () =>
        {
            pick = -1;
        };

        inWorld["lager"] = () =>
        {
            // if (inventory.Items.Count == 0)
            // {
            //     // skriv vad som gick fel
            //     return;
            // }

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
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Du kan inte använda den här. tryck enter för att lämna denna skärm");
                    }
                }
                else
                {
                    Console.WriteLine("Du valde att fortsätta använda det du redan använde. tryck enter för att lämna denna skärm");
                }
            }
            else
            {
                Console.WriteLine("du har inget i din ryggsäck. tryck enter för att lämna denna skärm");
            }

            Console.ReadLine();
            Console.Clear();
        };

        inWorld["upgradera"] = () =>
        {
            if (spell != null && hasUsedWorldAction == false)
            {
                float multiplier = Random.Shared.Next(1, 6);
                multiplier = 1 + (multiplier / 10);
                spell.Upgrade(multiplier);

                hasUsedWorldAction = true;
            }
            else if (hasUsedWorldAction == false && spell == null)
            {
                Console.WriteLine("Du har ingen förmåga att upgradera");
            }
            else
            {
                Console.WriteLine("Du har redan gjort ditt engångs val");
            }
        };

        inWorld["vila"] = () =>
        {
            if (hasUsedWorldAction == false)
            {
                Hp += Math.Round(maxHP * 0.2);
                Mp += Math.Round(maxMP * 0.2);
                hasUsedWorldAction = true;

                Console.WriteLine($"Du vilar och ditt HP är nu {hp} (+ {Math.Round(maxHP * 0.2)}) och MP är {mp} (+ {Math.Round(maxMP * 0.2)})");
            }
            else
            {
                Console.WriteLine("Du har redan gjort ditt engångs val");
            }
        };

        inWorld["fortsätt"] = () =>
        {
            isInWorld = false;
            hasUsedWorldAction = false;

            isInFight = true;
        };
    }

    public bool PlayerDefending
    {
        get => playerDefending;
    }

    public bool IsInFight
    {
        get => isInFight;

        set
        {
            isInFight = value;
        }
    }

    public bool IsInWorld
    {
        get => isInWorld;

        set
        {
            isInWorld = value;
        }
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

    public Abilitie Spell
    {
        get => spell;
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

    public Armor Armor
    {
        get => armor;
    }

    public StrengthPotion StrengthPotion
    {
        get => strengthPotion;
    }

    public double Damage
    {
        get => damage;

        set
        {
            if (damage + value > 0)
            {
                damage = value;
            }
            else
            {
                damage = 0;
            }
        }
    }

    public double Block
    {
        get => block;
    }

    public double LifeStealDuration
    {
        get => lifeStealDuration;

        set
        {
            lifeStealDuration = value;

            if (lifeStealDuration < 0)
            {
                lifeStealDuration = 0;
            }
        }
    }

    public double PotionDuration
    {
        get => potionDuration;

        set
        {
            potionDuration = value;

            if (potionDuration < 0)
            {
                PotionDuration = 0;
            }
        }
    }

    public double Hp
    {
        get => hp;

        set
        {
            hp = Math.Round(Math.Clamp(value, 0, maxHP));
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

    public double Xp
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
                Console.WriteLine($"Du levla up, du är nu level {level}");
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

    public int Pick
    {
        get => pick;

        set
        {
            pick = value;
        }
    }

    public void LifeStealActiv()
    {
        if (lifeStealDuration > 0)
        {
            LifeSteal life = (LifeSteal)spell;
            hp = damage * life.HelaAmount;
        }
    }

    public void ActionsForFight(Weapon weapon, StrengthPotion strengthPotion, Enemy target, string actions)
    {
        this.target = target;

        if (actions == "attackera")
        {
            inFight["attackera"]();
        }

        else if (actions == "försvara")
        {
            inFight["försvara"]();
        }

        else if (actions == "föremål")
        {
            inFight["föremål"]();
        }

        else if (actions == "magi")
        {
            inFight["magi"]();
        }

        else if (actions == "backa")
        {
            inFight["backa"]();
        }

        this.target = null;
    }

    public void ActionsForWorld(string actions)
    {
        if (actions == "lager")
        {
            inWorld["lager"]();
        }
        else if (actions == "upgradera")
        {
            inWorld["upgradera"]();
        }
        else if (actions == "vila")
        {
            inWorld["vila"]();
        }
        else if (actions == "fortsätt")
        {
            inWorld["fortsätt"]();
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

    public void WorldOrder(Player player)
    {
        isInWorld = true;

        while (isInWorld == true)
        {
            if (tutorial)
            {
                Console.WriteLine("Du har kommit till en viloplats och du kan titta igenom ditt lager, upgradera din magi och vila för hp och mana.");
                Console.WriteLine("Du kan dock när det kommer till uppgradera och vila så får du bara göra en av dem en gång pär vilo plats. ");
                Console.WriteLine();
            }

            player.PrintWorldActions();

            player.PickActionInWorld(player);

            Console.Clear();

            player.ActionsForWorld(player.PlayerAction);
        }
    }

    public void FightOrder(Weapon playerWeapon, Player player, StrengthPotion strengthPotion, Enemy target)
    {
        player.PrintFightActions();

        player.PickActionInFight(player);

        Console.Clear();

        player.ActionsForFight(player.PlayerWeapon, strengthPotion, target, player.PlayerAction);

        player.PotionDuration--;
        player.LifeStealDuration--;
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
            int random = Random.Shared.Next(1, 6);

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

                pick = TryP(tempHolder.Count);
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
            Console.WriteLine();

            if (i == amount - 1)
            {
                Console.WriteLine();
                Console.WriteLine("skriv numret till vänster av förmågan du vill ha");

                pick = TryP(tempHolder.Count);
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