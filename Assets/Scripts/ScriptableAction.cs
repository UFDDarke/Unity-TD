using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ScriptableAction : ScriptableObject
{
	public abstract void PerformAction(GameObject obj);

	public virtual string GenerateTooltip()
	{
		return null;
	}
}
