public class BackPack
{
    private List<Item> items = [];
    private Queue<Weapon> equippedWeapon = [];

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

    public void Display()
    {
        int itemCount = 1;

        Console.WriteLine();
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"({itemCount}) {Items[i].Name}: {items[i].Description}");
            itemCount++;
        }
    }

    public void Equip()
    {

    }
}
