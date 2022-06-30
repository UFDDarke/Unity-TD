using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Tower Defense/New Enemy")]
public class EnemyData : ScriptableObject
{
	public new string name = "New Enemy";
	public string description = "Short summary here";

	public int maxHealth = 5;
	public float speed = 1.0f;

	public string GetTooltipInfo()
	{
		StringBuilder builder = new StringBuilder();


		return "lel xd";
	}
}
