using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Tower Attack Random", menuName = "Tower Defense/New Action/On Attack New Target")]
public class OnAttack_NewRandomTarget : ScriptableAction
{
	public override void PerformAction(GameObject obj)
	{
		Tower tower = obj.GetComponent<Tower>();
		if (!tower) return;

		// TODO: Innately incompatible with multiple targets. Need proper implementation for >1 target
		List<Enemy> withinRange = EnemyManager.GetEnemiesInRange(tower.transform.position, tower.Range);
		if (withinRange == null) return;

		Enemy newTarget = withinRange[(int)Random.Range(0, withinRange.Count)];

		if (newTarget == null) return;

		tower.targets[0] = newTarget;
	}

	public override string GenerateTooltip()
	{
		return "Attacks random enemies within range.";
	}
}
