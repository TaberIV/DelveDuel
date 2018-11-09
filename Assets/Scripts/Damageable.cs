using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
	// Inspector Settings
	public float HealthMax = 100;

	// State 
	private float health;
	public float Health
	{
		get
		{
			return health;
		}
	}

	void Awake()
	{
		health = HealthMax;
	}

	public virtual void ReceiveDamage(float damage, GameObject damager = null)
	{
		health -= damage;

		if (health <= 0)
		{
			Death();
		}
	}

	public virtual void Death()
	{
		Destroy(gameObject);
	}
}
