using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : TriggerBehavior
{
	public float Damage = 20;
	public GameObject Owner;

	public override void OnTriggerEnter2D(Collider2D col)
	{
		Damageable damageable = col.gameObject.GetComponent<Damageable>();
		if (damageable != null)
		{
			damageable.ReceiveDamage(Damage, Owner);
		}

		Destroy(gameObject);
	}
}
