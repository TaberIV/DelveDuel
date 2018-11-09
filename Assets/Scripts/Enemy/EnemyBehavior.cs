using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class EnemyBehavior : MonoBehaviour
{
	// Inpsector Settings
	public float BurstSpeed = 3;
	public float DriftSpeed = 1;
	public float BurstTimeMax = 3;
	public float BurstTimeMin = 1;
	public float RecoilTime = 0.1f;

	// Components
	private Transform trans;
	private CharacterController2D controller;

	// Other objects
	private Transform player;

	// State
	private float burstTimer;
	private bool burst;
	private float recoilTimer;

	public bool Recoil
	{
		get { return recoilTimer > 0; }

		set
		{
			recoilTimer = value ? RecoilTime : 0;
		}
	}

	void Awake()
	{
		// GetComponents
		trans = GetComponent<Transform>();
		controller = GetComponent<CharacterController2D>();

		// Initialize State
		burst = Random.Range(0, 1) > 0.5;
		burstTimer = Random.Range(BurstTimeMin, BurstTimeMax);
		recoilTimer = 0;
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

			controller.Move(dir * speed * Time.deltaTime);

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
