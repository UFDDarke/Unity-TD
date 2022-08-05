using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Following the guide from:
// https://gamedevelopment.tutsplus.com/tutorials/using-the-composite-design-pattern-for-an-rpg-attributes-system--gamedev-243

[System.Serializable]
public class BaseAttribute
{
	private float baseValue;
	private float baseMultiplier;

	public AttributeType attributeType;
	public Attribute parent;

	public float BaseValue { get => baseValue; set => baseValue = value; }
	public float BaseMultiplier { get => baseMultiplier; set => baseMultiplier = value; }

	public BaseAttribute(float _baseValue, float _baseMultiplier = 0)
	{
		baseValue = _baseValue;
		baseMultiplier = _baseMultiplier;
	}
}

[System.Serializable]
public class RawBonus : BaseAttribute
{
	public RawBonus(float value, float multiplier) : base(value, multiplier) { }
}

[System.Serializable]
public class FinalBonus : BaseAttribute
{
	public FinalBonus(float value, float multiplier) : base(value, multiplier) { }
}

public enum AttributeType
{
	// TOWERS
	AttackSpeed = 1,
	Damage = 2,
	Range = 3,
	CriticalChance = 4,
	CriticalDamage = 5,
	ProjectileSpeed = 6,
	// ENEMIES
	MoveSpeed = 100,
	DamageTaken = 101
}