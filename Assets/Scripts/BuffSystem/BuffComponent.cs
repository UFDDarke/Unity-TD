using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffComponent : MonoBehaviour
{
	public abstract void ApplyBuff(BuffData data);
	public abstract void RemoveBuff(Buff expiredBuff);

	public abstract void Tick();
}
