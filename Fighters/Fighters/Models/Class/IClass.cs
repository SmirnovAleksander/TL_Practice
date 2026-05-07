namespace Fighters.Models.Class
{
    public interface IClass
    {
        public string Name { get; }
        public int Damage { get; }
        public int Health { get; }
        public int Initiative { get; }
    }
}