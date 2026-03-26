public class BackPack
{
    private List<Item> items = [];
    private Queue<Weapon> equippedWeapon = [];
    private Queue<Armor> equippedArmor = [];

    private Weapon weapon;

    public BackPack()
    {
        weapon = new();

        equippedWeapon.Enqueue(weapon);
    }

    public List<Item> Items
    {
        get => items;

        set
        {
            items.AddRange(value);
        }
    }

    public Queue<Weapon> EquippedWeapon
    {
        get => equippedWeapon;
    }

    public Queue<Armor> EquippedArmor
    {
        get => equippedArmor;
    }

    public void Display()
    {
        int itemCount = 1;

        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"({itemCount}) {Items[i].Name}: {items[i].Description}");
            itemCount++;
        }

        Console.WriteLine($"{items.Count + 1} Lämna");
    }

    public void EquipWeapon(int pick)
    {
        if (items[pick].WeaponBool == true)
        {
            equippedWeapon.Enqueue((Weapon)items[pick]);
            items.Remove(items[pick]);
        }
        else if (items[pick].WeaponBool == false)
        {
            Console.WriteLine("Du kan inte använda det som ditt vapen");
        }
    }

    public void EquipArmor(int pick)
    {
        if (items[pick].ArmorBool == true)
        {
            equippedArmor.Enqueue((Armor)items[pick]);
            items.Remove(items[pick]);
        }
        else if (items[pick].ArmorBool == false)
        {
            Console.WriteLine("Du kan inte använda det som din utrustning");
        }
    }
}