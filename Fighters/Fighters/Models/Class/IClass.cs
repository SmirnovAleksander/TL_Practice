using Fighters.Models.Interfaces;

namespace Fighters.Models.Class;

public interface IClass : INamed
{
    public int Damage { get; }
    public int Health { get; }
    public int Initiative { get; }
}