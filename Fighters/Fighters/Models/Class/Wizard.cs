namespace Fighters.Models.Class
{
    public class Wizard : IClass
    {
        public string Name { get; } = "Колдун";
        public int Damage { get; } = 6;
        public int Health { get; } = 4;
        public int Initiative { get; } = 2;
    }
}