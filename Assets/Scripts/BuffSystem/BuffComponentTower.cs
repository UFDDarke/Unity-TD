using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffComponentTower : BuffComponent
{
	[SerializeField]
	private List<Buff> buffs = new List<Buff>();
	public List<Buff> Buffs
	{
		private set { buffs = value; }
		get { return buffs; }
	}

	private Tower tower;

	public override void ApplyBuff(BuffData data)
	{
		Buff buffInstance = new Buff(data, this);
		Buffs.Add(buffInstance);

		switch(buffInstance.data.behavior)
		{
			case StackBehavior.Overwrite:
				// All other stacks of this buff are cleared
				break;
			case StackBehavior.ResetDurations:
				// Find all buffs with the same 'BuffData' and reset their durations
				foreach(Buff buff in Buffs.Where(n => n.data == data))
				{
					buff.remainingDuration = data.MaxDuration;
				}
				break;
			case StackBehavior.SeparateDurations:
				// All stacks have their own timer (default implementation)
				break;
			case StackBehavior.UnchangedDurations:
				// All stacks share the same timer
				break;

		}


		foreach(BaseAttribute attributeBonus in buffInstance.modifiers)
		{
			switch(attributeBonus.attributeType)
			{
				case AttributeType.AttackSpeed:
					AddBonus(tower.AtkSpeed, attributeBonus);
					break;

				case AttributeType.Damage:
					AddBonus(tower.Damage, attributeBonus);
					break;

				case AttributeType.Range:
					AddBonus(tower.Range, attributeBonus);
					break;

				case AttributeType.ProjectileSpeed:
					AddBonus(tower.ProjectileSpeed, attributeBonus);
					break;

				case AttributeType.CriticalChance:
					AddBonus(tower.CriticalChance, attributeBonus);
					break;

				case AttributeType.CriticalDamage:
					AddBonus(tower.CriticalDamage, attributeBonus);
					break;
				default:
					Debug.LogWarning("Failed to apply attribute bonus of type: " + attributeBonus.attributeType.ToString() + " to tower.");
					break;
			}
		}



	}
	public override void RemoveBuff(Buff expiredBuff)
	{
		// NYI
		Buffs.Remove(expiredBuff);
		// Apparently, orphaned classes will be removed by the garbage collector. Should make sure though
	}

	public override void Tick()
	{
		for (int i = buffs.Count - 1; i >= 0; i--)
		{
			Buff buff = buffs[i];
			buff.Tick(Time.deltaTime);
		}
	}

	private void Update()
	{
		Tick();
	}

	private void Start()
	{
		tower = GetComponentInParent<Tower>();
		if (!tower) Debug.LogError("BuffComponentTower couldn't find a Tower component.");
	}

	// MESSY MESSY
	// i have no clue where to even begin with this kind of tomfoolery, so i guess i will just do it messy until i figure out a proper method?
	// TODO: consult with the elder god Dillon, or perhaps Jordan

	// EDIT: thanks Jordan, I will keep this as a monument of shame for a while until I decide to remove it

	/*

	public float processDamage(float baseDamage)
	{
		float modifiedDamage = baseDamage;

		foreach(Buff buff in Buffs)
		{
			modifiedDamage += buff.data.TowerStats.atkFlatMod;
		}

		foreach (Buff buff in Buffs)
		{
			if (buff.data.TowerStats.atkPercentMod != 0)
			{
				modifiedDamage *= buff.data.TowerStats.atkPercentMod;
			}
		}

		return modifiedDamage;
	}

	public float processSpeed(float baseSpeed)
	{
		float modifiedSpeed = baseSpeed;

		foreach (Buff buff in Buffs)
		{
			if (buff.data.TowerStats.spdPercentMod != 0)
			{
				modifiedSpeed *= buff.data.TowerStats.spdPercentMod;
			}
		}

		return modifiedSpeed;
	}

	public float processCritChance(float baseCritChance)
	{
		float modifiedCritChance = baseCritChance;

		foreach (Buff buff in Buffs)
		{
			modifiedCritChance += buff.data.TowerStats.critChaFlatMod;
		}

		foreach (Buff buff in Buffs)
		{
			if (buff.data.TowerStats.critChaPercentMod != 0)
			{
				modifiedCritChance *= buff.data.TowerStats.critChaPercentMod;
			}
		}

		return modifiedCritChance;
	}

	public float processCritDamage(float baseCritDamage)
	{
		float modifiedCritDamage = baseCritDamage;

		foreach (Buff buff in Buffs)
		{
			modifiedCritDamage += buff.data.TowerStats.critDamFlatMod;
		}

		foreach (Buff buff in Buffs)
		{
			if (buff.data.TowerStats.critDamPercentMod != 0)
			{
				modifiedCritDamage *= buff.data.TowerStats.critDamPercentMod;
			}
		}

		return modifiedCritDamage;
	}

	public float processRange(float baseRange)
	{
		float modifiedRange = baseRange;

		foreach (Buff buff in Buffs)
		{
			modifiedRange += buff.data.TowerStats.rangeFlatMod;
		}

		foreach (Buff buff in Buffs)
		{
			if(buff.data.TowerStats.rangePercentMod != 0)
			{
				modifiedRange *= buff.data.TowerStats.rangePercentMod;
			}
		}

		return modifiedRange;
	}

	public float processProjSpeed(float baseProjSpeed)
	{
		float modifiedProjSpeed = baseProjSpeed;

		foreach (Buff buff in Buffs)
		{
			modifiedProjSpeed += buff.data.TowerStats.projSpdFlatMod;
		}

		foreach (Buff buff in Buffs)
		{
			if (buff.data.TowerStats.projSpdPercentMod != 0)
			{
				modifiedProjSpeed *= buff.data.TowerStats.projSpdPercentMod;
			}
		}

		return modifiedProjSpeed;
	}*/
}
