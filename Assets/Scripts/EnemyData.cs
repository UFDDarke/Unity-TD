using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum SizeClass { Mass, Normal, Champion, Boss };

[CreateAssetMenu(fileName = "New Enemy", menuName = "Tower Defense/New Enemy")]
public class EnemyData : ScriptableObject
{
	public new string name = "New Enemy";
	public string description = "Short summary here";

	public float healthModifier = 1;
	public float speed = 1.0f;
	
	public SizeClass sizeClass = SizeClass.Normal;
	public Color enemyColor = Color.red;


	public string GetTooltipInfo()
	{
		StringBuilder builder = new StringBuilder();


		return "lel xd";
	}
}