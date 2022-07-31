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
	private TowerStats towerStats;
	public TowerStats TowerStats { get => towerStats; private set => towerStats = value; }


	[SerializeField]
	[Tooltip("If this buff is applied to an enemy, it will gain the listed benefits or maluses.")]
	private EnemyStats enemyStats;
	public EnemyStats EnemyStats { get => enemyStats; private set => enemyStats = value; }

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
}

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
}

public enum StackBehavior
{
	Overwrite, // Implies only one stack allowed. Overwrites a single instance of this buff with a new instance.
	ResetDurations, // Upon adding a new stack, the duration is reset to its maximum for all stacks
	UnchangedDurations, // Upon adding a new stack, the lifetime of all buffs remains the same
	SeparateDurations // All stacks have their own, separate duration
}