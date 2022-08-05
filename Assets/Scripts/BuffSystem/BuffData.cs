using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Buff", menuName = "Tower Defense/New Buff")]
public class BuffData : ScriptableObject
{
	[Header("Duration")]
	[SerializeField]
	private bool permanentBuff;
	[SerializeField]
	private float maxDuration;

	[Header("Stacking")]
	[SerializeField]
	public StackBehavior behavior;


	[SerializeField]
	[Range(0, 512)]
	private int maxStacks;



	[SerializeField]
	private bool isBuffHarmful;

	[Header("Stat Modifiers")]
	[SerializeField]
	[Tooltip("If this buff is applied to a tower, it will gain the listed benefits or maluses.")]
	//public List<BaseAttribute> modifiers = new List<BaseAttribute>();
	public List<BonusModifier> modifiers = new List<BonusModifier>();

	/*
	private TowerStats towerStats;
	public TowerStats TowerStats { get => towerStats; private set => towerStats = value; }


	[SerializeField]
	[Tooltip("If this buff is applied to an enemy, it will gain the listed benefits or maluses.")]
	private EnemyStats enemyStats;
	public EnemyStats EnemyStats { get => enemyStats; private set => enemyStats = value; }
	*/
	public bool PermanentBuff
	{
		get { return permanentBuff; }
		private set { permanentBuff = value; }
	}

	public float MaxDuration
	{
		get { return maxDuration; }
		private set { maxDuration = value; }
	}

	public float GetFlatModifier(AttributeType type)
	{
		float totalModifier = 0;

		foreach (BonusModifier modifier in modifiers)
		{
			if (modifier.bonusGranted == type) totalModifier += modifier.flatValue;
		}

		return totalModifier;
	}

	public float GetPercentModifier(AttributeType type)
	{
		float totalModifier = 0;

		foreach (BonusModifier modifier in modifiers)
		{
			if (modifier.bonusGranted == type) totalModifier += modifier.multiplier;
		}

		return totalModifier;
	}
}

// TODO: Add a GetModifier method to extract the value of a specific attribute type
// Examples: BuffData.GetFlatModifier(AttributeType type)
//           BuffData.GetPercentModifier(AttributeType type)





/*
[System.Serializable]
public class TowerStats
{
	public float spdPercentMod = 0f;

	public float atkFlatMod = 0f;
	public float atkPercentMod = 0f;

	public float rangeFlatMod = 0f;
	public float rangePercentMod = 0f;

	public float critChaFlatMod = 0f;
	public float critChaPercentMod = 0f;

	public float critDamFlatMod = 0f;
	public float critDamPercentMod = 0f;

	public float projSpdFlatMod = 0f;
	public float projSpdPercentMod = 0f;

	public int maxTargetsFlatMod = 0;
}

[System.Serializable]
public class EnemyStats
{
	public float dmgTakenFlatMod = 0f;
	public float dmgTakenPercentMod = 0f;

	public float moveSpeedFlatMod = 0f;
	public float moveSpeedPercentMod = 0f;
}*/

[System.Serializable]
public class BonusModifier
{
	public AttributeType bonusGranted;
	[Tooltip("Should the buff be applied early during calculations (raw), or after all other calculations? (final)")]
	public BonusPriority priority;
	public float flatValue;
	public float multiplier;
}


public enum StackBehavior
{
	Overwrite, // Implies only one stack allowed. Overwrites a single instance of this buff with a new instance.
	ResetDurations, // Upon adding a new stack, the duration is reset to its maximum for all stacks
	UnchangedDurations, // Upon adding a new stack, the lifetime of all buffs remains the same
	SeparateDurations // All stacks have their own, separate duration
}

public enum BonusPriority
{
	Raw, // Behaves similarly to the 'increased' modifier in PoE
	Final // Applied after all other lower priority bonuses, behaving closely like the 'more' modifier in PoE
}