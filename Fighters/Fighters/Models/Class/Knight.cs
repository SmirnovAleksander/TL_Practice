namespace Fighters.Models.Class
{
    public class Knight : IClass
    {
        public string Name { get; } = "Рыцарь";
        public int Damage { get; } = 3;
        public int Health { get; } = 10;
        public int Initiative { get; } = 1;
    }
}