using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Tower Self Buff", menuName = "Tower Defense/New Action/On Crit Buff Self")]
public class OnCrit_TowerSelfBuff : ScriptableAction
{
	[SerializeField]
	[Range(0.01f, 10f)]
	private float spdModifier = 0.995f;

	public override void PerformAction(GameObject obj)
	{
		Tower tower = obj.GetComponent<Tower>();
		if (!tower) return;

		// Check if we should grant the buff
		if(tower.lastDamageInfo.wasCritical)
		{
			GrantBuff(tower);
		}
	}

	public void GrantBuff(Tower tower)
	{
		// Grants attack speed buff to tower
		tower.atkSpeed *= spdModifier;
	}

	public override string GenerateTooltip()
	{
		return "Upon crit, tower gains <b>+" + Mathf.Round((1 - (1 * spdModifier)) * 1000) / 1000f + "%</b> attack speed permanently.";
	}
}
