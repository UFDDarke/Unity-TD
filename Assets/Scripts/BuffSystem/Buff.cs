using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the class for the separate buff instances. It has a reference to its original BuffData, but it only tracks its own duration.
public class Buff
{
	public float remainingDuration;
	public BuffData data;
	public BuffComponent owner;
	public bool hasExpired = false;
	public bool isPermanent = false;
	public Buff(BuffData data_, BuffComponent owner_)
	{
		data = data_;
		owner = owner_;
		isPermanent = data.PermanentBuff;
		remainingDuration = data.MaxDuration;
	}

	public void Tick(float delta)
	{
		if(!isPermanent) remainingDuration -= delta;

		ProcessTickActions();

		if (remainingDuration <= 0) expireBuff();
	}

	private void ProcessTickActions()
	{
		// TODO: Scan through buff's actions and process them
	}

	private void expireBuff()
	{
		// This is the function called when this buff expires.
		// TODO: Buff Expire event?

		//Debug.Log("Buff expired");
		owner.RemoveBuff(this);
	}
}
