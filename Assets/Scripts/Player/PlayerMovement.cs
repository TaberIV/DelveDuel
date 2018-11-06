using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovement : MonoBehaviour
{
    // Inspector Settings
    public float MoveSpeed = 5f;
    public float MoveForce = 30f;
    public float Friction = 50f;

    // Components
    private CharacterController2D controller;

    // State
    private Vector2 moveInput;
    private Vector2 velocity;

    void Awake()
    {
        // GetComponents
        controller = GetComponent<CharacterController2D>();

        // Initialize State
        velocity = new Vector2(0, 0);
        moveInput = new Vector2(0, 0);
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
        controller.Move(movement);
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
