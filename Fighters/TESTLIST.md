# Тесты Fighters

## Fighter

- [x] Constructor_ValidName_SetsName - конструктор устанавливает имя
- [x] GetMaxHealth_RaceAndClassHealthSum_ReturnsTotalMaxHealth - здоровье = Race.Health + Class.Health
- [x] CalculateDamage_AllComponentsSum_ReturnsTotalDamage - урон = Weapon + Class + Race
- [x] CalculateArmor_ArmorAndRaceArmorSum_ReturnsTotalArmor - броня = Armor + Race
- [x] Initiative_RaceAndClassInitiativeSum_ReturnsTotalInitiative - инициатива = Race + Class
- [x] TakeDamage_DamageLessThanHealth_ReduceCurrentHealth - урон уменьшает HP
- [x] TakeDamage_DamageMoreHealth_SetHealthToZero - урон > HP = 0
- [x] IsAlive_HealthGreaterThanZero_ReturnsTrue - жив при HP > 0
- [x] IsAlive_HealthZero_ReturnsFalse - мёртв при HP = 0
- [x] GetCurrentHealth_AfterDamage_ReturnsMaxHealthMinusDamage - после создания HP = max, после урона max - damage

## DamageCalculator

- [x] CalculateDamage_BaseDamageMinusArmor_ReturnsReducedDamage - baseDamage - armor
- [x] CalculateDamage_NegativeModifier_ReduceDamage - модификатор -20% уменьшает урон
- [x] CalculateDamage_PositiveModifier_IncreasesDamage - модификатор +10% увеличивает урон
- [x] CalculateDamage_CriticalHit_DoublesDamageBeforeArmor - крит ×2 до вычета брони
- [x] CalculateDamage_DamageLessThanArmor_ReturnsZero - урон меньше брони -> 0
- [x] CalculateDamage_ZeroArmor_ReturnsFullDamage - без брони полный урон
- [x] CalculateDamage_CriticalWithNegativeModifier_AppliesBothMultipliers - крит + отрицательный модификатор

## BattleManager

- [x] StartBattle_LessThanTwoFighters_WritesError - < 2 бойцов: битва не начинается, боец жив
- [x] StartBattle_TwoFighters_DeclaresWinner - 2 бойца: победитель жив, проигравший мёртв
- [x] StartBattle_ThreeFighters_RemovesDeadAndDeclaresWinner - 3 бойца: выживает сильнейший

## FighterFactory

- [x] CreateFighter_ValidChoices_ReturnsFighterWithCorrectStats - создание с верными статами
- [x] CreateFighter_AllMenusDisplayed_ShowsAllFourChoices - выводятся все 4 меню с пунктами
- [x] CreateFighter_SelectSecondItem_ReturnsWeaponWithCorrectDamage - выбор 2-го оружия

## GameManager

- [x] PlayGame_EmptyName_DoesNotCreateFighter - пустое имя: боец не создаётся
- [x] PlayGame_ValidName_CreatesFighter - валидное имя: боец создаётся
- [x] PlayGame_LessThanTwoFighters_DoesNotStartBattle - < 2 бойцов: битва не запускается

## InputHelper

- [x] ReadChoice_ValidInput_ReturnsNumber - валидный ввод возвращает число
- [x] ReadChoice_InvalidInput_RetriesUntilValid - невалидный ввод: повтор до успеха (0, больше max, пустая строка)
- [x] ReadChoice_InvalidString_ShowsErrorMessage - "abc": сообщение об ошибке
