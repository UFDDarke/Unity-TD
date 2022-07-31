using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffComponentEnemy : BuffComponent
{
	[SerializeField]
	private List<Buff> buffs = new List<Buff>();
	public List<Buff> Buffs
	{
		private set { buffs = value; }
		get { return buffs; }
	}

	public override void ApplyBuff(BuffData data)
	{
		Buff buffInstance = new Buff(data, this);
		Buffs.Add(buffInstance);

		switch (buffInstance.data.behavior)
		{
			case StackBehavior.Overwrite:
				// All other stacks of this buff are cleared
				break;
			case StackBehavior.ResetDurations:
				// Find all buffs with the same 'BuffData' and reset their durations
				foreach (Buff buff in Buffs.Where(n => n.data == data))
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
	}


	public override void RemoveBuff(Buff expiredBuff)
	{
		Buffs.Remove(expiredBuff);
	}

	public override void Tick()
	{
		for (int i = buffs.Count - 1; i >= 0; i--)
		{
			Buff buff = buffs[i];
			buff.Tick(Time.deltaTime);
		}
	}

	// TODO: Process enemy buff data
	public float ModifyDamage(float baseDamage)
	{
		float modifiedDamage = baseDamage;

		foreach (Buff buff in Buffs)
		{
			modifiedDamage += buff.data.EnemyStats.dmgTakenFlatMod;
		}

		foreach (Buff buff in Buffs)
		{
			if (buff.data.EnemyStats.dmgTakenPercentMod != 0f)
			{
				modifiedDamage *= buff.data.EnemyStats.dmgTakenPercentMod;
			}
		}

		// TODO: Enemies cannot mitigate more than 98% of incoming damage
		return Mathf.Max(0, modifiedDamage);
	}

	public float ModifySpeed(float baseSpeed)
	{
		float modifiedSpeed = baseSpeed;

		foreach (Buff buff in Buffs)
		{
			modifiedSpeed += buff.data.EnemyStats.moveSpeedFlatMod;
		}

		foreach (Buff buff in Buffs)
		{
			if (buff.data.EnemyStats.moveSpeedPercentMod != 0f)
			{
				modifiedSpeed *= buff.data.EnemyStats.moveSpeedPercentMod;
			}
		}

		return Mathf.Max(0.1f, modifiedSpeed);
	}
}
