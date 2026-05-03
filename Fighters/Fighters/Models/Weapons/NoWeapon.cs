namespace Fighters.Models.Weapons
{
    public class NoWeapon : IWeapon
    {
        public int Damage { get; } = 0;
        public string Name { get; } = "Без оружия";
    }
}