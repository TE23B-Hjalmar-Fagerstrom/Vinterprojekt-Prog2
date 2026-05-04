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

    public void Display() // skriver ut all information om det som finns i ryggsäcken
    {
        int itemCount = 1;

        for (int i = 0; i < items.Count; i++) // skriver ut items information den mängden items
        {
            Console.WriteLine($"({itemCount}) {Items[i].Name}: {items[i].Description}");
            itemCount++;
        }

        Console.WriteLine($"{items.Count + 1} Lämna");
    }

    public void EquipWeapon(int pick) // metoden gör så spelaren kan utrusta vappen
    {
        if (items[pick].WeaponBool == true) // om det spelaren valde var ett vappen så utrustas den och tas ur ryggsäcken
        {
            equippedWeapon.Enqueue((Weapon)items[pick]);
            items.Remove(items[pick]);
        }
    }

    public void EquipArmor(int pick) // metoden gör så spelaren kan utrusta armor 
    {
        if (items[pick].ArmorBool == true) // om det spelaren valde var armor så utrustas den och tas ur ryggsäcken
        {
            equippedArmor.Enqueue((Armor)items[pick]);
            items.Remove(items[pick]);
        }
    }
}