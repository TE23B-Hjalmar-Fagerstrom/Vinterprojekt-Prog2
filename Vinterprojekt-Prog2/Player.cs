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

    public Player() // ger spelarens start värden och logiken till alla spelarens funktioner
    {
        hp = maxHP;
        mp = maxMP;
        weapon = inventory.EquippedWeapon.Dequeue();

        inFight["attackera"] = () => // gör så att spelaren attackerar sin valda fiende
        {
            playerDefending = false;

            damage = Random.Shared.Next((int)weapon.MinDamage + level, (int)weapon.MaxDamage + level + 1);

            if (potionDuration > 0) // kollar om spelaren har en styrke dryck 
            {
                damage *= strengthPotion.DamageMultiplierFromPotion;
            }

            damage = Math.Round(damage);

            if (target?.Defending == false) // om fienden som attackeras inte blockar så gör spelaren mer skada
            {
                Damage -= target.Armor;

                target.Hp -= damage;
                Console.WriteLine($"Du gjorde {damage} skada på {target.EnemyName}");

                LifeStealActiv(); // kollar om spelaren har LifeSteal och ger hp baserat på skadan som gjordes

                damage += target.Armor;

                Console.WriteLine();
            }
            else // om fienden som attackeras blockar så gör spelaren minder skada
            {
                Damage -= Math.Round(target.Armor * 1.5);
                target.Hp -= damage;
                Console.WriteLine($"Du gjorde {damage} skada på {target.EnemyName}");
                Console.WriteLine();

                LifeStealActiv(); // kollar om spelaren har LifeSteal och ger hp baserat på skadan som gjordes

                Damage += Math.Round(target.Armor * 1.5);
            }

            target.EnemyTurn = true;
        };

        inFight["försvara"] = () => // gör så att spelaren försvarar mot fienderna
        {
            playerDefending = true;
            block = 0.7;
            target.EnemyTurn = true;
        };

        inFight["magi"] = () => // gör din förmåga
        {
            if (spell != null) // om spelaren har en förmåga så görs förmågan
            {
                spell.UseAbilitie(target, this);
            }
            else // om spelaren inte har en förmåga förklaras det till spelaren
            {
                Console.WriteLine("Du har inte en trollformel att utföra");
                Console.WriteLine("tryck enter för att lämna denna skärm");

                Console.ReadLine();
            }
        };

        inFight["föremål"] = () => // skriver ut alla potions spelaren har och använder den spelaren väljer
        {
            List<Consumable> tempHolder = [];

            for (int i = 0; i < inventory.Items.Count; i++) // stoppar in alla potions i en temporär hållare som spelaren kan interagera med under strider
            {
                if (inventory.Items[i].ConsumableBool == true)
                {
                    tempHolder.Add((Consumable)inventory.Items[i]);
                    inventory.Items.Remove(inventory.Items[i]);
                    i--;
                }
            }

            if (tempHolder.Count > 0) // om man har potions att använda
            {
                for (int i = 0; i < tempHolder.Count; i++) // skriver ut alla potions spelaren har 
                {
                    Console.WriteLine($"{i + 1})  {tempHolder[i].Name}: {tempHolder[i].Description}");
                }

                Console.WriteLine($"{tempHolder.Count + 1}) Backa");
                Console.WriteLine();
                Console.WriteLine("skriv nummret till vänster av föremålet du vill använda");

                pick = TryP(tempHolder.Count + 1); // läser in vad spelaren vill gör

                if (pick < tempHolder.Count) // använder den potionen spelaren valde
                {
                    tempHolder[pick].Use(this);
                }

                if (strengthPotion == null) // om spelaren valde en styrke dryck så stoppas den in i spelaren och räknar vilka värden spelaren ska få från den
                {
                    if (potionDuration > 0)
                    {
                        strengthPotion = (StrengthPotion)tempHolder[pick];
                    }
                }

                if (potionDuration <= 0) // om styrke drycks effekten är noll eller minder blir variabeln strengthPotion tömd
                {
                    strengthPotion = null;
                }

                for (int i = 0; i < tempHolder.Count; i++) // läger tillbaka alla potions från temporära hållaren till ryggsäcken
                {
                    if (tempHolder[i].UsesCurent > 0)
                    {
                        inventory.Items.Add(tempHolder[i]);
                    }
                }

                tempHolder.Clear(); // tömmer den temporära hållaren
                pick = -10;
            }
            else // om spelaren inte har någon potions så förklaras det till spelaren
            {
                Console.WriteLine("Du har inga föremål");
                Console.WriteLine("Tryck enter för att lämna denna skärm");
                Console.ReadLine();
                Console.Clear();
            }
        };

        inFight["backa"] = () => // går tillbaka till vyn där man ser alla fiender och spelaren
        {
            pick = -1;
        };

        inWorld["lager"] = () => // skriver ut alla föremål spelaren har på sig samt i ryggsäcken, spelaren kan också utrusta det den har i ryggsäcken
        {
            if (inventory.Items.Count >= 1)
            {
                Console.WriteLine("Använder: ");
                Console.WriteLine($"Vapen: {weapon.Name} (kan göra {weapon.MinDamage} - {weapon.MaxDamage} skada)");
                if (armor == null) // om spelaren inte har någon armor på sig skriver den ut det till spelaren
                {
                    Console.WriteLine("Armor: ingen utrustad");
                }
                else // om spelaren har armor på sig skriver den ut det till spelaren
                {
                    Console.WriteLine($"Armor: {armor.Name} (blockar {armor.Defens} skada)");
                }
                if (spell != null) // om spelaren har en förmåga så skrever den ut det till spelaren
                {
                    Console.WriteLine($"Förmåga: {spell.Name}");
                }
                Console.WriteLine();

                Console.WriteLine("I din ryggsäck:");
                inventory.Display(); // skrive ut allt i ryggsäcken

                Console.WriteLine();
                Console.WriteLine("skriv numret som står till vänster av föremålet du vill utrusta.");
                pick = TryP(inventory.Items.Count + 1); // spelaren väljer det den vill utrusta eller att backa ut

                if (pick < inventory.Items.Count) // om spelaren valde att försöka utrusta något
                {
                    if (inventory.Items[pick].WeaponBool == true) // om spelaren valde att utrusta ett vapen
                    {
                        inventory.EquipWeapon(pick);

                        if (pick <= inventory.Items.Count)
                        {
                            inventory.Items.Add(weapon); // stoppar vapnet splaren hade in i ryggsäcken
                            weapon = inventory.EquippedWeapon.Dequeue(); // utrustar vapnet som var i ryggsäcken

                            Console.WriteLine($"Du utrustade {weapon.Name}. tryck enter för att lämna denna skärm");
                        }
                    }
                    else if (inventory.Items[pick].ArmorBool == true) // om spelaren valde att utrusta armor
                    {
                        inventory.EquipArmor(pick);

                        if (pick <= inventory.Items.Count)
                        {
                            if (armor != null)
                            {
                                inventory.Items.Add(armor); // stoppar armorn splaren hade in i ryggsäcken
                            }

                            armor = inventory.EquippedArmor.Dequeue(); // utrustar armorn som var i ryggsäcken

                            Console.WriteLine($"Du utrustade {armor.Name}. tryck enter för att lämna denna skärm");
                        }
                    }
                    else // om spelaren valde en potion
                    {
                        Console.WriteLine($"Du kan inte använda den här. tryck enter för att lämna denna skärm");
                    }
                }
                else // om spelaren valda att inte utrusta något
                {
                    Console.WriteLine("Du valde att fortsätta använda det du redan använde. tryck enter för att lämna denna skärm");
                }
            }
            else // om spelaren inte har något i ryggsäcken
            {
                Console.WriteLine("du har inget i din ryggsäck. tryck enter för att lämna denna skärm");
            }

            Console.ReadLine();
            Console.Clear();
        };

        inWorld["upgradera"] = () => // upgraderar spelarens förmåga
        {
            if (spell != null && hasUsedWorldAction == false) // om man inte har gjort sitt engångsval och har en förmåga så 
            {
                float multiplier = Random.Shared.Next(1, 6);
                multiplier = 1 + (multiplier / 10);
                spell.Upgrade(multiplier);

                hasUsedWorldAction = true;
            }
            else if (hasUsedWorldAction == false && spell == null) // om man inte har fått en förmåga än
            {
                Console.WriteLine("Du har ingen förmåga att upgradera");
            }
            else // man har gjort sitt engångsval
            {
                Console.WriteLine("Du har redan gjort ditt engångs val");
            }
        };

        inWorld["vila"] = () => // ger spelaren hp och mana
        {
            if (hasUsedWorldAction == false) // om spelaren inte har gjort sitt engångsval så får spelaren 20% av sitt max hp och mana tillbaka 
            {
                Hp += Math.Round(maxHP * 0.2);
                Mp += Math.Round(maxMP * 0.2);
                hasUsedWorldAction = true;

                Console.WriteLine($"Du vilar och ditt HP är nu {hp} (+ {Math.Round(maxHP * 0.2)}) och MP är {mp} (+ {Math.Round(maxMP * 0.2)})");
            }
            else // spelaren har gjort sitt engångsval
            {
                Console.WriteLine("Du har redan gjort ditt engångs val");
            }
        };

        inWorld["fortsätt"] = () => // startar nästa strid
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

            if (xp >= 15 * level)
            {
                xp -= 15 * level;
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

    public void LifeStealActiv() // kollar om spelaren har LifeSteal och ger hp baserat på skadan som gjordes
    {
        if (lifeStealDuration > 0)
        {
            LifeSteal life = (LifeSteal)spell;
            hp += damage * life.HelaAmount;

            Console.WriteLine($"och du fick {damage * life.HelaAmount}");
        }
    }

    public void ActionsForFight(Weapon weapon, StrengthPotion strengthPotion, Enemy target, string actions) // läser in vilken handling spelaren ville göra i striden och gör den
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

    public void ActionsForWorld(string actions) // läser in vilken handling spelaren ville göra i utanför striden och gör den
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

    public void PrintFightActions() // skriver ut alla handlingar spelaren kan göra under striden
    {
        foreach (string key in inFight.Keys)
        {
            Console.Write($"{key}:  ");
        }

        Console.WriteLine();
    }

    public void PrintWorldActions() // skriver ut alla handlingar spelaren kan göra utanför striderna
    {
        foreach (string key in inWorld.Keys)
        {
            Console.Write($"{key}:  ");
        }

        Console.WriteLine();
    }

    public void PickActionInFight(Player player) // läser in vilken handling spelaren valde att göra under striden
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

    public void PickActionInWorld(Player player) // läser in vilken handling spelaren valde att göra utanför striden
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

    public void WorldOrder(Player player) // gör så att alla metoderna ovan som handlar om world är i en årdning och lätare att använda utanför player
    {
        isInWorld = true;

        while (isInWorld == true)
        {
            if (tutorial) // om det är första gången spelaren är i world så får spelaren en förklaring av vad alla val gör
            {
                Console.WriteLine("Du har kommit till en viloplats och du kan titta igenom ditt lager, upgradera din magi och vila för hp och mana.");
                Console.WriteLine("Du kan dock när det kommer till uppgradera och vila så får du bara göra en av dem en gång pär vilo plats. ");
                Console.WriteLine();

                tutorial = false;
            }

            player.PrintWorldActions();

            player.PickActionInWorld(player);

            Console.Clear();

            player.ActionsForWorld(player.PlayerAction);
        }
    }

    public void FightOrder(Weapon playerWeapon, Player player, StrengthPotion strengthPotion, Enemy target) // gör så att alla metoderna ovan som handlar om Fight är i en årdning och lätare att använda utanför player
    {
        player.PrintFightActions();

        player.PickActionInFight(player);

        Console.Clear();

        player.ActionsForFight(player.PlayerWeapon, strengthPotion, target, player.PlayerAction);

        player.PotionDuration--;
        player.LifeStealDuration--;
    }

    public int TryP(int countInItems) // läser in vad spelaren väljer när det kommer till numer val
    {
        int pick = -10;

        while (pick < 1 || pick > countInItems) // så länge spelaren inte skrev ett giltigt numer
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

    public void NewItem() // skapar 3 till 5 nya slumpade föremål där spelaren kan välja en av dem som uppoffras
    {
        int amount = Random.Shared.Next(3, 6);

        List<Item> tempHolder = [];

        for (int i = 0; i < amount; i++) //gör och slumpar den mängden föremål som slumpades innan
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
        }

        Console.WriteLine();
        Console.WriteLine("skriv numret till vänster om det föremål du vill ta");

        pick = TryP(tempHolder.Count);
        Inventory.Items.Add(tempHolder[pick]);

        for (int a = 0; a < tempHolder.Count; a++)
        {
            tempHolder.Remove(tempHolder[a]);
        }
    }

    public void PickAbility() // skapar dem 3 spellsen som finns och spelaren får välja en av dem 3 som uppoffras
    {
        List<Abilitie> tempHolder = [];

        for (int i = 0; i < 3; i++)
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
        }

        Console.WriteLine();
        Console.WriteLine("skriv numret till vänster av förmågan du vill ha");

        pick = TryP(tempHolder.Count);
        spell = tempHolder[pick];

        for (int a = 0; a < tempHolder.Count; a++)
        {
            tempHolder.Remove(tempHolder[a]);
        }
    }

    public Dictionary<string, Action> inFight = new();
    public Dictionary<string, Action> inWorld = new();
}