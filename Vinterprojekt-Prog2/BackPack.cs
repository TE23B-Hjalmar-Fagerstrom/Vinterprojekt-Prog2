public class BackPack
{
    private List<Item> items = [];

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
