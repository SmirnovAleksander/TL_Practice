using Fighters.Models.Interfaces;

namespace Fighters.Models.Weapons;

public interface IWeapon : INamed
{
    public int Damage { get; }
}