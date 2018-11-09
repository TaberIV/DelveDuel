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
	public bool Recoil;
	public float RecoilTime;

	// Components
	private Transform trans;
	private CharacterController2D controller;

	// Other objects
	private Transform player;

	// State
	private float burstTimer;
	private bool burst;
	private float recoilTimer;

	void Awake()
	{
		// GetComponents
		trans = GetComponent<Transform>();
		controller = GetComponent<CharacterController2D>();

		// Initialize State
		burst = Random.Range(0, 1) > 0.5;
		burstTimer = Random.Range(BurstTimeMin, BurstTimeMax);
	}

	void Start()
	{
		player = GameObject.Find("Player").transform;
	}

	void Update()
	{
		Vector3 dir = (player.position - trans.position).normalized;
		float speed = burst ? BurstSpeed : DriftSpeed;

		controller.Move(dir * speed * Time.deltaTime);

		burstTimer -= Time.deltaTime;

		if (burstTimer <= 0)
		{
			burstTimer = Random.Range(BurstTimeMin, BurstTimeMax);
			burst = !burst;
		}
	}
}
