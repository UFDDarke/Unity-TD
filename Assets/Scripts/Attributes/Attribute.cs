using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute : BaseAttribute
{
	protected List<RawBonus> rawBonuses;
	protected List<FinalBonus> finalBonuses;

	protected float finalValue;
	protected bool isDirty = true;


	public Attribute(float _baseValue) : base(_baseValue)
	{
		rawBonuses = new List<RawBonus>();
		finalBonuses = new List<FinalBonus>();

		finalValue = _baseValue;
	}

	public void addRawBonus(RawBonus bonus)
	{
		rawBonuses.Add(bonus);
		bonus.parent = this;
		isDirty = true;
	}

	public void addFinalBonus(FinalBonus bonus)
	{
		finalBonuses.Add(bonus);
		bonus.parent = this;
		isDirty = true;
	}


	public void removeRawBonus(RawBonus bonus)
	{
		rawBonuses.Remove(bonus);
		isDirty = true;
	}
	public void removeFinalBonus(FinalBonus bonus)
	{
		finalBonuses.Remove(bonus);
		isDirty = true;
	}

	public float GetFlatBonus()
	{
		float bonusValue = 0;

		foreach(RawBonus bonus in rawBonuses)
		{
			bonusValue += bonus.BaseValue;
		}

		foreach (FinalBonus bonus in finalBonuses)
		{
			bonusValue += bonus.BaseValue;
		}

		return bonusValue;
	}

	public float GetMultiplierBonus()
	{
		float bonusMultiplier = 0;

		foreach (RawBonus bonus in rawBonuses)
		{
			bonusMultiplier += bonus.BaseMultiplier;
		}

		foreach (FinalBonus bonus in finalBonuses)
		{
			bonusMultiplier += bonus.BaseMultiplier;
		}

		return bonusMultiplier;
	}

	public virtual float processValue() // If no arguments given, just processes the final value of this attribute
	{
		return processValue(BaseValue);
	}

	public virtual float processValue(float toProcess)
	{
		float processedValue = toProcess;

		// Raw Bonuses apply before final bonuses

		float rawBonusValue = 0;
		float rawBonusMultiplier = 0;

		foreach (RawBonus bonus in rawBonuses)
		{
			rawBonusValue += bonus.BaseValue;
			rawBonusMultiplier += bonus.BaseMultiplier;
		}

		processedValue += rawBonusValue;
		processedValue *= (1 + rawBonusMultiplier);

		// Final Bonuses

		float finalBonusValue = 0;
		float finalBonusMultiplier = 0;

		foreach (FinalBonus bonus in finalBonuses)
		{
			finalBonusValue += bonus.BaseValue;
			finalBonusMultiplier += bonus.BaseMultiplier;
		}

		processedValue += finalBonusValue;
		processedValue *= (1 + finalBonusMultiplier);

		return processedValue;
	}


	public float getFinalValue()
	{
		// TODO: Don't recalculate each time the value is checked, only when it has to

		if(isDirty)
		{
			isDirty = false;
			finalValue = processValue();
			return finalValue;
		} else
		{
			// No changes to value, so don't waste time processing
			return finalValue;
		}
	}
}
