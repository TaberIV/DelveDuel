using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class EnemyBehavior : BaddyBehavior
{
	// Inpsector Settings
	public float BurstSpeed = 3;
	public float DriftSpeed = 1;
	public float BurstTimeMax = 3;
	public float BurstTimeMin = 1;

	// Components
	private Transform trans;
	private CharacterController2D controller;

	// Other objects
	private Transform player;

	// State
	private float burstTimer;
	private bool burst;

	protected override void Awake()
	{
		// GetComponents
		trans = GetComponent<Transform>();
		controller = GetComponent<CharacterController2D>();

		// Initialize State
		burst = Random.Range(0, 1) > 0.5;
		burstTimer = Random.Range(BurstTimeMin, BurstTimeMax);
		base.Awake();
	}

	void Start()
	{
		player = GameObject.Find("Player").transform;
	}

	void Update()
	{
		Vector3 dir = (player.position - trans.position).normalized;

		if (!Recoil)
		{
			float speed = burst ? BurstSpeed : DriftSpeed;

			RaycastHit2D hitInfo = controller.Move(dir * speed * Time.deltaTime);

			if (hitInfo.collider != null && hitInfo.collider.gameObject.name == "Player")
			{
				hitInfo.collider.gameObject.GetComponent<Damageable>()
					.ReceiveDamage(20, gameObject);

				GetComponent<Damageable>().ReceiveDamage(20, hitInfo.collider.gameObject);
			}

			burstTimer -= Time.deltaTime;

			if (burstTimer <= 0)
			{
				burstTimer = Random.Range(BurstTimeMin, BurstTimeMax);
				burst = !burst;
			}
		}
		else
		{
			controller.Move(-dir * BurstSpeed * Time.deltaTime);

			recoilTimer -= Time.deltaTime;
		}
	}
}
