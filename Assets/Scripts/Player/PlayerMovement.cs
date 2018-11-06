using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
    // Inspector Settings
    // Movement Settings
    public float MoveSpeed = 5f;
    public float MoveForce = 30f;
    public float Friction = 50f;

    // Collision Settings
    public int NumRays = 3;
    public float RayMargin = 0.001f;

    // Components
    private Transform trans;
    private Collider2D col;

    // State
    private Vector2 moveInput;
    private Vector2 velocity;
    private ArrayList CollisionTags = new ArrayList();

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
            float raySpacing = col.bounds.size.y / (NumRays - 1);
            RaycastHit2D closestHit = new RaycastHit2D();
            closestHit.distance = Mathf.Infinity;

            for (int i = 0; i < NumRays; i++)
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(origin, Vector2.right * dir, Mathf.Abs(movement.x));
                Debug.DrawRay(origin, Vector2.right * dir, Color.red);
                Debug.DrawRay(origin, Vector2.right * movement.x, Color.blue);

                if (hitInfo.collider != null && CollisionTags.Contains(hitInfo.collider.tag))
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

                if (hitInfo.collider != null && CollisionTags.Contains(hitInfo.collider.tag))
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

    void Awake()
    {
        // GetComponents
        trans = GetComponent<Transform>();
        col = GetComponent<Collider2D>();

        // Initialize State
        velocity = new Vector2(0, 0);
        moveInput = new Vector2(0, 0);

        CollisionTags.Add("Wall");
    }

    void Update()
    {
        GetInput();

        Movement();
    }

    private void GetInput()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (moveInput.magnitude > 1)
        {
            moveInput.Normalize();
        }
    }

    private void Movement()
    {
        ApplyFriction();

        Vector2 targetVelocity = GetTargetVelocity();
        Acclerate(targetVelocity);

        Vector2 movement = velocity * Time.deltaTime;
        Move(movement);
    }

    private Vector2 GetTargetVelocity()
    {
        return moveInput * MoveSpeed;
    }

    private void ApplyFriction()
    {
        // Only apply friction on moving character
        if (velocity.magnitude > 0)
        {
            float newSpeed = velocity.magnitude - Friction * Time.deltaTime;

            // Makes sure friction doesn't send player backwards
            if (newSpeed < 0)
            {
                newSpeed = 0;
            }

            // Scale velocity vector such that velocity.magnitude = newSpeed
            float scale = newSpeed / velocity.magnitude;
            velocity *= scale;
        }
    }

    private void Acclerate(Vector2 targetVelocity)
    {
        Vector2 targetDirection = targetVelocity.normalized;
        float targetSpeed = targetVelocity.magnitude;

        // Gets current speed that is in the desired direction
        float currentSpeed = Vector2.Dot(velocity, targetDirection);
        float addSpeed = targetSpeed - currentSpeed;

        // If player is slowing down or already going max
        // speed in desired direction, do not accelerate.
        if (addSpeed <= 0)
        {
            return;
        }

        float deltaSpeed = (MoveForce + Friction) * Time.deltaTime;

        // Don't overshoot targetVelocity
        if (deltaSpeed > addSpeed)
        {
            deltaSpeed = addSpeed;
        }

        velocity += deltaSpeed * targetDirection;

        // TODO find a way to reduce move speed
        if (velocity.magnitude > MoveSpeed)
        {
            velocity = velocity.normalized * MoveSpeed;
        }
    }
}
