using Fighters.Models.Armors;
using Fighters.Models.Class;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public class Fighter : IFighter
{
    private readonly IRace _race;
    private readonly IClass _fighterClass;
    private readonly IArmor _armor;
    private readonly IWeapon _weapon;

    private int _currentHealth;

    public string Name { get; }
    public int Initiative => _race.Initiative + _fighterClass.Initiative;

    public Fighter(
        string name,
        IRace race,
        IArmor armor,
        IWeapon weapon,
        IClass fighterClass )
    {
        Name = name;
        _race = race;
        _armor = armor;
        _weapon = weapon;
        _fighterClass = fighterClass;

        _currentHealth = GetMaxHealth();
    }

    public int GetCurrentHealth() => _currentHealth;

    public int GetMaxHealth() => _race.Health + _fighterClass.Health;

    public int CalculateDamage() => _weapon.Damage + _fighterClass.Damage + _race.Damage;

    public int CalculateArmor() => _armor.Armor + _race.Armor;

    public override string ToString()
    {
        return $@"   Класс: {_fighterClass.Name}, Раса: {_race.Name}, Броня на герое: {_armor.Name}, Оружие: {_weapon.Name}
   HP: {GetMaxHealth()}, Урон: {CalculateDamage()}, Броня: {CalculateArmor()}, Инициатива: {Initiative}";
    }

    public void TakeDamage( int damage )
    {
        int newHealth = _currentHealth - damage;
        _currentHealth = newHealth < 0 ? 0 : newHealth;
    }

    public bool IsAlive()
    {
        return _currentHealth > 0;
    }
}