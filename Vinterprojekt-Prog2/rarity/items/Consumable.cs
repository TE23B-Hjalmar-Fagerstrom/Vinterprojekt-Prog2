public class Consumable : Item
{
    private double usesMax = 3;
    private double usesCurent;
    private double usesDuration;

    public Consumable()
    {
        usesMax = usesMax * RarityMultiplier;
        usesMax = Math.Round(usesMax);

        usesCurent = usesMax;
    }

    public double UsesMax
    {
        get => usesMax;
    }

    public double UsesCurent
    {
        get => usesCurent;

        set
        {
            usesCurent = value;
        }
    }

    public double UsesDuration
    {
        get => usesDuration;

        set
        {
            usesDuration = value;
        }
    }

    public virtual void Use(Player target)
    {
        
    }
}
