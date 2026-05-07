namespace Fighters.Models.Class
{
    public class Assassin : IClass
    {
        public string Name { get; } = "Ассасин";
        public int Damage { get; } = 4;
        public int Health { get; } = 5;
        public int Initiative { get; } = 4;
    }
}