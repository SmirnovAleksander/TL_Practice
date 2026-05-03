namespace Fighters.Models.Races
{
    public class Ghost : IRace
    {
        public string Name { get; } = "Призрак";
        public int Damage { get; } = 10;
        public int Health { get; } = 30;
        public int Armor { get; } = 0;
        public int Initiative { get; } = 4;
    }
}