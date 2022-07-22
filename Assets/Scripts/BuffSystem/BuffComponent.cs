using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffComponent : MonoBehaviour
{
	[SerializeField]
	private List<Buff> buffs = new List<Buff>();
	public List<Buff> Buffs
	{
		private set { buffs = value; }
		get { return buffs; }
	}

	public void ApplyBuff(BuffData newBuff)
	{
		Buff buffInstance = new Buff(newBuff, this);
		Buffs.Add(buffInstance);

		switch(buffInstance.data.behavior)
		{
			case StackBehavior.Overwrite:
				// All other stacks of this buff are cleared
				break;
			case StackBehavior.ResetDurations:
				// All stacks have their duration reset to its maximum
				foreach(Buff buff in Buffs.Where(n => n.data == newBuff))
				{
					buff.remainingDuration = newBuff.MaxDuration;
				}
				break;
			case StackBehavior.SeparateDurations:
				// All stacks have their own timer (default implementation)
				break;
			case StackBehavior.UnchangedDurations:
				// All stacks share the same timer
				break;

		}
	}

	public void RemoveBuff(Buff expiredBuff)
	{
		// NYI
		Buffs.Remove(expiredBuff);
		// Apparently, orphaned classes will be manually removed by the garbage collector. Should make sure though
	}

	public void TickBuffs()
	{
		//foreach(Buff buff in Buffs)
		//{
		//	buff.Tick(Time.deltaTime);
		//}

		for (int i = buffs.Count - 1; i >= 0; i--)
		{
			Buff buff = buffs[i];
			buff.Tick(Time.deltaTime);
		}
	}

	public void Update()
	{
		TickBuffs();
	}

	// MESSY MESSY
	// i have no clue where to even begin with this kind of tomfoolery, so i guess i will just do it messy until i figure out a proper method?
	// TODO: consult with the elder god Dillon, or perhaps Jordan
	public float processDamage(float baseDamage)
	{
		float modifiedDamage = baseDamage;

		foreach(Buff buff in Buffs)
		{
			modifiedDamage += buff.data.Stats.atkFlatMod;
		}

		foreach (Buff buff in Buffs)
		{
			if (buff.data.Stats.atkPercentMod != 0)
			{
				modifiedDamage *= buff.data.Stats.atkPercentMod;
			}
		}

		return modifiedDamage;
	}

	public float processSpeed(float baseSpeed)
	{
		float modifiedSpeed = baseSpeed;

		foreach (Buff buff in Buffs)
		{
			if (buff.data.Stats.spdPercentMod != 0)
			{
				modifiedSpeed *= buff.data.Stats.spdPercentMod;
			}
		}

		return modifiedSpeed;
	}

	public float processCritChance(float baseCritChance)
	{
		float modifiedCritChance = baseCritChance;

		foreach (Buff buff in Buffs)
		{
			modifiedCritChance += buff.data.Stats.critChaFlatMod;
		}

		foreach (Buff buff in Buffs)
		{
			if (buff.data.Stats.critChaPercentMod != 0)
			{
				modifiedCritChance *= buff.data.Stats.critChaPercentMod;
			}
		}

		return modifiedCritChance;
	}

	public float processCritDamage(float baseCritDamage)
	{
		float modifiedCritDamage = baseCritDamage;

		foreach (Buff buff in Buffs)
		{
			modifiedCritDamage += buff.data.Stats.critDamFlatMod;
		}

		foreach (Buff buff in Buffs)
		{
			if (buff.data.Stats.critDamPercentMod != 0)
			{
				modifiedCritDamage *= buff.data.Stats.critDamPercentMod;
			}
		}

		return modifiedCritDamage;
	}

	public float processRange(float baseRange)
	{
		float modifiedRange = baseRange;

		foreach (Buff buff in Buffs)
		{
			modifiedRange += buff.data.Stats.rangeFlatMod;
		}

		foreach (Buff buff in Buffs)
		{
			if(buff.data.Stats.rangePercentMod != 0)
			{
				modifiedRange *= buff.data.Stats.rangePercentMod;
			}
		}

		return modifiedRange;
	}

	public float processProjSpeed(float baseProjSpeed)
	{
		float modifiedProjSpeed = baseProjSpeed;

		foreach (Buff buff in Buffs)
		{
			modifiedProjSpeed += buff.data.Stats.projSpdFlatMod;
		}

		foreach (Buff buff in Buffs)
		{
			if (buff.data.Stats.projSpdPercentMod != 0)
			{
				modifiedProjSpeed *= buff.data.Stats.projSpdPercentMod;
			}
		}

		return modifiedProjSpeed;
	}
}
