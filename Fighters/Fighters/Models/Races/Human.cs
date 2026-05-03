namespace Fighters.Models.Races
{
    public class Human : IRace
    {
        public string Name { get; } = "Человек";
        public int Damage { get; } = 11;
        public int Health { get; } = 100;
        public int Armor { get; } = 4;
        public int Initiative { get; } = 2;
    }
}
