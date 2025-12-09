public class Player
{
    private float maxHP = 100;
    private float hp;
    private float maxMP = 30;
    private float mp;
    private BackPack inventory = new();
    private int level;
    private int gold;

    public Dictionary<string, Action> inFight = new();
    public Dictionary<string, Action> inWorld = new();
}
