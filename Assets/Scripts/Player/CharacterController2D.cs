using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CharacterController2D : MonoBehaviour
{
	// Inspector Settings
	public int NumRaysX = 3;
	public int NumRaysY = 3;
	public float RayMargin = 0.001f;
	public ContactFilter2D filter2D;

	// Components
	private Transform trans;
	private Collider2D col;

	private int MaxCollisions = 100;

	void Awake()
	{
		trans = GetComponent<Transform>();
		col = GetComponent<Collider2D>();
	}

	public RaycastHit2D[, ] Move(Vector2 movement)
	{
		RaycastHit2D[, ] collisionInfo = Movement(movement);

		return collisionInfo;
	}

	private RaycastHit2D[, ] Movement(Vector2 movement)
	{

		RaycastHit2D[, ] collisionInfo = new RaycastHit2D[2, MaxCollisions];

		for (int axis = 0; axis < 2; axis++)
		{
			float move = axis == 0 ? movement.x : movement.y;

			Vector2 moveAxis = axis == 0 ? Vector2.right : Vector2.up;
			Vector2 crossAxis = axis == 0 ? Vector2.up : Vector2.right;

			RaycastHit2D[] axisCollisionInfo = new RaycastHit2D[MaxCollisions];

			if (move != 0)
			{
				float dir = Mathf.Sign(move);

				// Origin of first vector
				Vector2 origin = axis == 0 ?
					new Vector2(
						(dir > 0 ? col.bounds.max.x : col.bounds.min.x) +
						dir * RayMargin,
						col.bounds.min.y
					) :
					new Vector2(
						col.bounds.min.x + RayMargin,
						(dir > 0 ? col.bounds.max.y : col.bounds.min.y) +
						dir * RayMargin
					);

				// Raycasting
				bool collided = false;
				float raySpacing = axis == 0 ?
					(col.bounds.size.y - 2 * RayMargin) / (NumRaysX - 1) :
					(col.bounds.size.x - 2 * RayMargin) / (NumRaysY - 1);
				RaycastHit2D closestHit = new RaycastHit2D();
				closestHit.distance = Mathf.Infinity;

				for (int ray = 0; ray < NumRaysX; ray++)
				{
					int numCollisions = Physics2D.Raycast(origin, moveAxis * dir, filter2D, axisCollisionInfo, Mathf.Abs(move));

					Debug.DrawRay(origin, moveAxis * dir, Color.red);
					Debug.DrawRay(origin, moveAxis * move, Color.blue);

					for (int collision = 0; collision < numCollisions; collision++)
					{
						RaycastHit2D hitInfo = axisCollisionInfo[collision];

						if (hitInfo.collider != null)
						{
							if (hitInfo.collider.isTrigger)
							{
								hitInfo.collider.GetComponent<TriggerBehavior>().OnTriggerEnter2D(col);
							}
							else
							{
								if (hitInfo.distance < closestHit.distance)
								{
									closestHit = hitInfo;
								}

								collided = true;
							}
						}

						collisionInfo[axis, collision] = axisCollisionInfo[collision];
					}

					origin += crossAxis * raySpacing;
				}

				if (!collided) // No collisions, move normally
				{
					transform.position += (Vector3) moveAxis * move;
				}
				else // Move as close to the wall as possible
				{
					// TODO: adjust for irregular col.bounds.center

					float collisionExtent = axis == 0 ?
						col.bounds.extents.x :
						col.bounds.extents.y;

					float closestPoint = axis == 0 ?
						closestHit.point.x :
						closestHit.point.y;

					float moved = closestPoint - dir * (collisionExtent + RayMargin);
					trans.position = axis == 0 ? new Vector3(moved, trans.position.y) : new Vector3(trans.position.x, moved);
				}
			}
		}

		return collisionInfo;
	}
}
