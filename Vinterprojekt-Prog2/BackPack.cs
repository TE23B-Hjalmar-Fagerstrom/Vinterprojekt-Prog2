public class BackPack
{
    private List<Item> items = [];
    private Queue<Item> equippedWeapon = [];

    private Weapon weapon;

    public BackPack()
    {
        weapon = new() {Name = "Fist"};

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

    public void Display()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine(Items[i].Name);
        }
    }
}
