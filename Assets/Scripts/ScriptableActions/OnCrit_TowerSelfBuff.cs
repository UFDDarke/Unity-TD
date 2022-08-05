using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Tower Self Buff", menuName = "Tower Defense/New Action/On Crit Buff Self")]
public class OnCrit_TowerSelfBuff : ScriptableAction
{
	[SerializeField]
	private BuffData teslaBuff;

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
		//tower.atkSpeed *= spdModifier;
		tower.buffs.ApplyBuff(teslaBuff);
	}

	public override string GenerateTooltip()
	{
		// TODO: Convert to new system
		//return "Upon crit, tower gains <b>+" + Mathf.Round((1 - (1 * teslaBuff.TowerStats.spdPercentMod)) * 1000) / 1000f + "%</b> attack speed permanently.";
		return "Upon crit, tower gains <b>+" + teslaBuff.GetFlatModifier(AttributeType.AttackSpeed) * 100f + "%</b> attack speed permanently.";
	}
}
