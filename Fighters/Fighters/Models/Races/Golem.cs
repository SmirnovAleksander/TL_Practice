namespace Fighters.Models.Races
{
    public class Golem : IRace
    {
        public string Name { get; } = "Голем";
        public int Damage { get; } = 20;
        public int Health { get; } = 250;
        public int Armor { get; } = 20;
        public int Initiative { get; } = 3;
    }
}