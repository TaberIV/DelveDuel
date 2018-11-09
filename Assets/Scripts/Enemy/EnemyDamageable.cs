using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaddyBehavior))]
public class EnemyDamageable : Damageable
{
	// Components
	private BaddyBehavior behavior;

	protected override void Awake()
	{
		base.Awake();

		behavior = GetComponent<BaddyBehavior>();
	}

	public override void ReceiveDamage(float damage, GameObject damager = null)
	{
		if (!behavior.Recoil && damager.name == "Player")
		{
			base.ReceiveDamage(damage, damager);
			behavior.Recoil = true;
		}
	}
}
