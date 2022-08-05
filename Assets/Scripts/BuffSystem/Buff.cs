using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the class for the separate buff instances. It has a reference to its original BuffData, but it only tracks its own duration.
public class Buff
{
	public float remainingDuration;
	public BuffData data;
	public BuffComponent owner;
	public bool hasExpired = false;
	public bool isPermanent = false;
	public List<BaseAttribute> modifiers;

	public Buff(BuffData data_, BuffComponent owner_)
	{
		data = data_;
		owner = owner_;
		isPermanent = data.PermanentBuff;
		remainingDuration = data.MaxDuration;
		modifiers = new List<BaseAttribute>();

		// TODO: Multiple stat types

		// For each modifier in the given BuffData, add it to modifiers so we can keep track of each instance.

		foreach(BonusModifier bonusModifier in data.modifiers)
		{
			BaseAttribute newBonus;
			switch (bonusModifier.priority)
			{
				case (BonusPriority.Raw):
					newBonus = new RawBonus(bonusModifier.flatValue, bonusModifier.multiplier);
					modifiers.Add(newBonus);
					newBonus.attributeType = bonusModifier.bonusGranted;
					break;
				case BonusPriority.Final:
					newBonus = new FinalBonus(bonusModifier.flatValue, bonusModifier.multiplier);
					modifiers.Add(newBonus);
					newBonus.attributeType = bonusModifier.bonusGranted;
					break;
			}
			
		}


	}

	public void Tick(float delta)
	{
		if(!isPermanent) remainingDuration -= delta;

		//Debug.Log(remainingDuration);

		ProcessTickActions();

		if (remainingDuration <= 0) expireBuff();
	}

	private void ProcessTickActions()
	{
		// TODO: Scan through buff's actions and process them
	}

	private void expireBuff()
	{
		// This is the function called when this buff expires.
		// TODO: Buff Expire event?

		// Clean up all attribute instances
		foreach(BaseAttribute modifier in modifiers)
		{
			if(modifier is RawBonus) modifier.parent.removeRawBonus(modifier as RawBonus);

			if(modifier is FinalBonus) modifier.parent.removeFinalBonus(modifier as FinalBonus);
		}

		//Debug.Log("Buff expired");
		owner.RemoveBuff(this);
	}
}
