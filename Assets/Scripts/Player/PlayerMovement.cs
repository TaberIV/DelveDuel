using System.Collections;
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
		transform.position += Vector3.right * movement.x;

		// Vertical Movement
		if (movement.y != 0)
		{
			float dir = Mathf.Sign(movement.y);

			Vector2 origin = new Vector2(
				col.bounds.min.x,
				(dir > 0 ? col.bounds.max.y : col.bounds.min.y) +
				dir * RayMargin
			);

			bool move = true;
			float raySpacing = col.bounds.size.x / (NumRays - 1);

			for (int i = 0; i < NumRays; i++)
			{
				RaycastHit2D hitInfo = Physics2D.Raycast(origin, Vector2.up * dir, Mathf.Abs(movement.y));

				if (hitInfo.collider != null)
				{
					move = false;

					float newY = dir > 0 ? (hitInfo.point.y - col.bounds.extents.y) - RayMargin : hitInfo.point.y + col.bounds.extents.y + RayMargin;

					trans.position = new Vector2(trans.position.x, newY);
					break;
				}

				origin += Vector2.right * raySpacing;
			}

			if (move)
			{
				transform.position += Vector3.up * movement.y;
			}
		}
	}
}
