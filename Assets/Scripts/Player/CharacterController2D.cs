using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CharacterController2D : MonoBehaviour
{
	// Collision Settings
	public int NumRaysX = 3;
	public int NumRaysY = 3;
	public float RayMargin = 0.001f;
	public string[] CollisionTags;

	// Components
	private Transform trans;
	private Collider2D col;

	private ArrayList collisionTags = new ArrayList();

	void Awake()
	{
		trans = GetComponent<Transform>();
		col = GetComponent<Collider2D>();

		foreach (string tag in CollisionTags)
		{
			collisionTags.Add(tag);
		}
	}

	public void Move(Vector2 movement)
	{
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
			float raySpacing = col.bounds.size.y / (NumRaysX - 1);
			RaycastHit2D closestHit = new RaycastHit2D();
			closestHit.distance = Mathf.Infinity;

			for (int i = 0; i < NumRaysX; i++)
			{
				RaycastHit2D hitInfo = Physics2D.Raycast(origin, Vector2.right * dir, Mathf.Abs(movement.x));
				Debug.DrawRay(origin, Vector2.right * dir, Color.red);
				Debug.DrawRay(origin, Vector2.right * movement.x, Color.blue);

				if (hitInfo.collider != null && collisionTags.Contains(hitInfo.collider.tag))
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
			float raySpacing = col.bounds.size.x / (NumRaysY - 1);
			RaycastHit2D closestHit = new RaycastHit2D();
			closestHit.distance = Mathf.Infinity;

			for (int i = 0; i < NumRaysY; i++)
			{
				RaycastHit2D hitInfo = Physics2D.Raycast(origin, Vector2.up * dir, Mathf.Abs(movement.y));
				Debug.DrawRay(origin, Vector2.up * dir, Color.red);
				Debug.DrawRay(origin, Vector2.up * movement.y, Color.blue);

				if (hitInfo.collider != null && collisionTags.Contains(hitInfo.collider.tag))
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
