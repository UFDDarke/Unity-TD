using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Tower Attack Again", menuName = "Tower Defense/New Action/On Hit Fire Again")]
public class OnHit_FireAgain : ScriptableAction
{
	[SerializeField]
	private int chanceFireAgain = 5;
	public override void PerformAction(GameObject obj)
	{
		Tower tower = obj.GetComponent<Tower>();
		if (!tower) return;
		if (Random.Range(0, 100) >= chanceFireAgain) return;

		if(!tower.lastDamageInfo.wasLethal)
		{
			tower.fire(tower.lastDamageInfo.victim);
		}
	}

	public override string GenerateTooltip()
	{
		return (chanceFireAgain + "% chance to fire an additional projectile upon hit.");
	}
}
