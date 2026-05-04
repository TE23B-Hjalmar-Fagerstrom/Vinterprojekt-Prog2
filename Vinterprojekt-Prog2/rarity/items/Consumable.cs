public class Consumable : Item
{
    private double usesMax = 3;
    private double usesCurent;
    private double usesDuration;
    protected double consumableValue;
    protected string effect;

    public Consumable() // ger vilka start värden/text variablerna ska ha 
    {
        usesMax = Math.Round(usesMax * RarityMultiplier);

        usesCurent = usesMax;

        consumableBool = true;
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

    public virtual void Use(Player target) // gör så att man kan använda variabeln Consumable till föremål och fortfarande ha åtkomst till deras personliga Use
    {

    }

    public void TheDescription() // ger en beskrivning av vad föremålet gör till spelaren
    {
        description = $"{consumableValue} {effect}";
    }
}