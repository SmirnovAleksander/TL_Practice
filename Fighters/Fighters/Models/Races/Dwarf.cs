namespace Fighters.Models.Races;

public class Dwarf : IRace
{
    public string Name { get; } = "Гном";
    public int Damage { get; } = 8;
    public int Health { get; } = 90;
    public int Armor { get; } = 4;
    public int Initiative { get; } = 3;
}