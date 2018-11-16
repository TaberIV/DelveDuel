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
	private int MaxCollisions = 10;

	// Components
	private Transform trans;
	private Collider2D col;

	// State
	private RaycastHit2D[] solidCollisions = new RaycastHit2D[2];
	private List<RaycastHit2D> triggerCollisions;

	void Awake()
	{
		trans = GetComponent<Transform>();
		col = GetComponent<Collider2D>();
	}

	public void Move(Vector2 movement)
	{
		for (int axis = 0; axis < 2; axis++)
		{
			float move = axis == 0 ? movement.x : movement.y;

			Vector2 moveAxis = axis == 0 ? Vector2.right : Vector2.up;
			Vector2 crossAxis = axis == 0 ? Vector2.up : Vector2.right;

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
					RaycastHit2D[] collisionInfo = new RaycastHit2D[MaxCollisions];
					int numCollisions =
						Physics2D.Raycast(
							origin,
							moveAxis * dir,
							filter2D,
							collisionInfo,
							Mathf.Abs(move)
						);

					Debug.DrawRay(origin, moveAxis * dir, Color.red);
					Debug.DrawRay(origin, moveAxis * move, Color.blue);

					for (int colI = 0; colI < numCollisions; colI++)
					{
						RaycastHit2D hitInfo = collisionInfo[colI];

						if (hitInfo.collider != null &&
							hitInfo.distance < closestHit.distance)
						{
							if (!hitInfo.collider.isTrigger)
							{
								collided = true;
								closestHit = hitInfo;
								solidCollisions[axis] = collisionInfo[colI];
							}
							else
							{
								// TODO filter out any triggers that collide after final closestHit
								triggerCollisions.Add(hitInfo);
							}

						}
					}

					// Next ray
					origin += crossAxis * raySpacing;
				}

				if (!collided) // No collisions, move normally
				{
					transform.position += (Vector3) moveAxis * move;
				}
				else // Move as close to the wall as possible
				{
					// TODO: adjust for irregular col.bounds.center

					float colExtent = axis == 0 ?
						col.bounds.extents.x :
						col.bounds.extents.y;

					float closestPoint = axis == 0 ?
						closestHit.point.x :
						closestHit.point.y;

					float moved = closestPoint - dir * (colExtent + RayMargin);

					trans.position = axis == 0 ?
						new Vector3(moved, trans.position.y) :
						new Vector3(trans.position.x, moved);
				}
			}
		}
	}
}
