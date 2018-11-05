﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	// Inspector Settings
	public float MoveSpeed = 5f;
	public int NumRays = 3;
	public float RayMargin = 0.01f;

	// Components
	private Transform trans;
	private Collider2D col;

	// State
	private Vector2 velocity;

	void Awake()
	{
		// GetComponents
		trans = GetComponent<Transform>();
		col = GetComponent<Collider2D>();

		// Initialize State
		velocity = new Vector2(0, 0);
	}

	void Update()
	{
		Movement();
	}

	private void Movement()
	{
		Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		velocity = input * MoveSpeed;

		Vector2 movement = velocity * Time.deltaTime;

		// Horizontal Movement
		if (movement.x != 0)
		{
			float dir = Mathf.Sign(movement.x);

			// Origin of first vector
			Vector2 origin = new Vector2(
				(dir > 0 ? col.bounds.max.x : col.bounds.min.x) +
				dir * RayMargin,
				col.bounds.min.y
			);

			// Raycasting
			bool collided = false;
			float raySpacing = col.bounds.size.y / (NumRays - 1);
			RaycastHit2D closestHit = new RaycastHit2D();
			closestHit.distance = Mathf.Infinity;

			for (int i = 0; i < NumRays; i++)
			{
				RaycastHit2D hitInfo = Physics2D.Raycast(origin, Vector2.right * dir, Mathf.Abs(movement.x));
				Debug.DrawRay(origin, Vector2.right * dir, Color.red);
				Debug.DrawRay(origin, Vector2.right * movement.x, Color.blue);

				if (hitInfo.collider != null)
				{
					if (hitInfo.distance < closestHit.distance)
					{
						closestHit = hitInfo;
					}

					collided = true;
				}

				origin += Vector2.up * raySpacing;
			}

			if (!collided) // No collisions, move normally
			{
				transform.position += Vector3.right * movement.x;
			}
			else // Move as close to the wall as possible
			{
				float newX = closestHit.point.x - dir * (col.bounds.extents.x + RayMargin);
				trans.position = new Vector3(newX, trans.position.y);
			}
		}

		// Vertical Movement
		if (movement.y != 0)
		{
			float dir = Mathf.Sign(movement.y);

			// Origin of first vector
			Vector2 origin = new Vector2(
				col.bounds.min.x,
				(dir > 0 ? col.bounds.max.y : col.bounds.min.y) +
				dir * RayMargin
			);

			// Raycasting
			bool collided = false;
			float raySpacing = col.bounds.size.x / (NumRays - 1);
			RaycastHit2D closestHit = new RaycastHit2D();
			closestHit.distance = Mathf.Infinity;

			for (int i = 0; i < NumRays; i++)
			{
				RaycastHit2D hitInfo = Physics2D.Raycast(origin, Vector2.up * dir, Mathf.Abs(movement.y));
				Debug.DrawRay(origin, Vector2.up * dir, Color.red);
				Debug.DrawRay(origin, Vector2.up * movement.y, Color.blue);

				if (hitInfo.collider != null)
				{
					if (hitInfo.distance < closestHit.distance)
					{
						closestHit = hitInfo;
					}

					collided = true;
				}

				origin += Vector2.right * raySpacing;
			}

			if (!collided) // No collisions, move normally
			{
				transform.position += Vector3.up * movement.y;
			}
			else // Move as close to the wall as possible
			{
				float newY = closestHit.point.y - dir * (col.bounds.extents.y + RayMargin);
				trans.position = new Vector3(trans.position.x, newY);
			}
		}
	}
}
