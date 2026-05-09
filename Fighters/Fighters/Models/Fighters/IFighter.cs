namespace Fighters.Models.Fighters;

public interface IFighter
{
    string Name { get; }
    int Initiative { get; }

    public int GetCurrentHealth();
    public int GetMaxHealth();
    public int CalculateDamage();
    public int CalculateArmor();

    public string GetInfo();

    public void TakeDamage( int damage );

    public bool IsAlive();
}