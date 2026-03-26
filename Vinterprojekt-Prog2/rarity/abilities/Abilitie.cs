public class Abilitie : Rarity
{
    private double mageDamage;
    private double manaCost;
    protected string name = "";
    protected string description = "";

    public double MageDamage
    {
        get => mageDamage;

        protected set
        {
            mageDamage = value;
        }
    }

    public double ManaCost
    {
        get => manaCost;

        protected set
        {
            manaCost = value;
        }
    }

    public string Name
    {
        get => name;
    }

    public string Description
    {
        get => description;
    }

    public virtual void UseAbilitie(Enemy target, Player player)
    {
        
    }

    public virtual void Upgrade(float multiplier)
    {
        
    }
}