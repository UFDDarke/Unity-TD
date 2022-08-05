using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffComponent : MonoBehaviour
{
	public abstract void ApplyBuff(BuffData data);
	public abstract void RemoveBuff(Buff expiredBuff);

	protected void AddBonus(Attribute attribute, BaseAttribute bonus)
	{
		// Adds an attribute bonus to 'attribute', but checks if the bonus attribute is raw (flat) or final
		if (bonus is RawBonus)
		{
			attribute.addRawBonus(bonus as RawBonus);
			return;
		}

		if (bonus is FinalBonus)
		{
			attribute.addFinalBonus(bonus as FinalBonus);
			return;
		}

	}

	public abstract void Tick();
}
