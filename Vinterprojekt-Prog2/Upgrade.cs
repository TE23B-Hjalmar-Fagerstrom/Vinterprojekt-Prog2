public class Upgrade : Abilitie
{
    private float upgradeMultiplier;

    public Upgrade()
    {
        upgradeMultiplier = (RarityMultiplier * RarityMultiplier) * 0.75f;
    }

    public float UpgradeMultiplier
    {
        get => upgradeMultiplier;
    }
}
