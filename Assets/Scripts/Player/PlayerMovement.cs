using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float MoveSpeed = 5;

	private Transform trans;

	void Awake()
	{
		trans = GetComponent<Transform>();
	}

	void Update()
	{
		Movement();
	}

	private void Movement()
	{
		Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		Vector2 pos = trans.position;
		pos += input * MoveSpeed * Time.deltaTime;

		trans.position = pos;
	}
}
