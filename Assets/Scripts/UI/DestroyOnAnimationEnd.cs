using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAnimationEnd : MonoBehaviour
{
	public void DestroyParent()
	{
		// TODO: Object Pooling for Damage Texts (destroying this text is pretty expensive in unity, we should try to reuse when possible)
		GameObject parent = gameObject.transform.parent.gameObject;
		Destroy(parent);

	}
}
