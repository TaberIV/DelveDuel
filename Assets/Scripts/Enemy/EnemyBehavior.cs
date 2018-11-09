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

	// Components
	Transform trans;
	CharacterController2D controller;

	// Other objects
	private Transform player;

	// State
	private Vector2 velocity;
	private float burstTimer;
	private bool burst;

	void Awake()
	{
		// GetComponents
		trans = GetComponent<Transform>();
		controller = GetComponent<CharacterController2D>();

		// Initialize State
		velocity = new Vector2();

		burst = Random.Range(0, 1) > 0.5;
		burstTimer = Random.Range(BurstTimeMin, BurstTimeMax);
	}

	void Start()
	{
		player = GameObject.Find("Player").transform;
	}

	void Update()
	{
		if (burstTimer > 0)
		{
			Vector3 diff = (player.position - trans.position).normalized;

			float speed = burst ? BurstSpeed : DriftSpeed;
			controller.Move(diff * speed * Time.deltaTime);

			burstTimer -= Time.deltaTime;
		}
		else
		{
			burstTimer = Random.Range(BurstTimeMin, BurstTimeMax);
			burst = !burst;
		}
	}
}
