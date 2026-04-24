public class Item : Rarity
{
    private string name;
    protected string description;
    protected bool weaponBool = false;
    protected bool consumableBool = false;
    protected bool armorBool = false;
    protected bool salable = true;

    public Item()
    {
        name = $"{theRarity} ";

        description = "";
    }

    public bool WeaponBool{get => weaponBool;}
    public bool ConsumableBool {get => consumableBool;}
    public bool ArmorBool {get => armorBool;}

    public string Description
    {
        get => description;
    }

    public string Name
    {
        get => name;

        set
        {
            name += value;
        }
    }
}