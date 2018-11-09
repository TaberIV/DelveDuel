using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehavior))]
public class EnemyDamageable : Damageable
{
	// Components
	private EnemyBehavior behavior;

	protected override void Awake()
	{
		base.Awake();

		behavior = GetComponent<EnemyBehavior>();
	}

	public override void ReceiveDamage(float damage, GameObject damager = null)
	{
		if (!behavior.Recoil)
		{
			base.ReceiveDamage(damage, damager);
			behavior.Recoil = true;
		}
	}
}
