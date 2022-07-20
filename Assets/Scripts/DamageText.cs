using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class DamageText
{
	private static GameObject prefab;
	static DamageText()
	{
		// On start, load the Damage Text prefab
		prefab = Resources.Load("Prefabs/Text Holder") as GameObject;
	}

	public static void CreateFloatingText(string dmgText, Vector2 pos)
	{
		GameObject newText = Object.Instantiate(prefab);
		TextMeshPro text = newText.GetComponentInChildren<TextMeshPro>();
		text.SetText(dmgText);
		newText.transform.position = pos;
	}

}
