using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
	// Inpsector Settings
	public float Speed = 3;

	// Components
	Transform trans;

	// Other objects
	private Transform player;

	void Awake()
	{
		trans = GetComponent<Transform>();
	}

	void Start()
	{
		player = GameObject.Find("Player").transform;
	}

	void Update()
	{
		trans.position = Vector3.MoveTowards(trans.position, player.position, 3 * Time.deltaTime);
	}
}
