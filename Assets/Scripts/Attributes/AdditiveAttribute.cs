using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditiveAttribute : Attribute
{
	public AdditiveAttribute(float _baseValue) : base(_baseValue)
	{
	}

	public override float processValue(float toProcess)
	{
		float processedValue = toProcess;

		// Multipliers are currently not yet implemented for Additive attributes.
		// These work similarly to the increased attack speed stat in Warcraft 3,
		// Final Value = (baseValue) / (1 + totalBonusValue)

		float rawBonusValue = 0;

		foreach (RawBonus bonus in rawBonuses)
		{
			rawBonusValue += bonus.BaseValue;
		}

		processedValue = processedValue / (1 + rawBonusValue);

		// Final Bonuses still work, though

		float finalBonusValue = 0;

		foreach (FinalBonus bonus in finalBonuses)
		{
			finalBonusValue += bonus.BaseValue;
		}

		processedValue = processedValue / (1 + finalBonusValue);

		return processedValue;
	}
}
