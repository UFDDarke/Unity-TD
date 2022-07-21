using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
	POSITIVE,
	NEGATIVE
}

public abstract class Buff : ScriptableObject
{
	[SerializeField]
	private float maxDuration;
	[SerializeField]
	private float currentDuration;
	[SerializeField]
	private bool refreshDuration;



	public abstract void ApplyBuff();
	public abstract void RemoveBuff();
}
