using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaddyBehavior : MonoBehaviour
{
	public float RecoilTime = 0.1f;
	protected float recoilTimer;

	public bool Recoil
	{
		get { return recoilTimer > 0; }

		set
		{
			recoilTimer = value ? RecoilTime : 0;
		}
	}

	protected virtual void Awake()
	{
		recoilTimer = 0;
	}
}
