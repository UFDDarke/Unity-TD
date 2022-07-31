using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Tower Debuff Onhit", menuName = "Tower Defense/New Action/On Hit Debuff Enemy")]
public class OnHit_DebuffEnemy : ScriptableAction
{
	[SerializeField]
	private BuffData debuff;

	[SerializeField]
	[Range(0f, 100f)]
	private float chanceToDebuff = 100f;

	public override void PerformAction(GameObject obj)
	{
		Tower tower = obj.GetComponent<Tower>();
		if (!tower) return;

		Enemy enemy = tower.lastDamageInfo.victim;
		if (!enemy) return;

		if(Random.Range(0f, 100f) < chanceToDebuff)
		{
			Debuff(enemy);
		}

	}

	public void Debuff(Enemy enemy)
	{
		enemy.buffs.ApplyBuff(debuff);
	}

	public override string GenerateTooltip()
	{
		if(chanceToDebuff == 100f) return "Applies a debuff to struck enemies";

		return chanceToDebuff + "% chance to apply a debuff to struck enemies";

	}
}
