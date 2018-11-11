using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class BulletMovement : MonoBehaviour
{
    // Inspector Settings
    public float MoveSpeed = 5f;
    public float LiveTime = 2f;
    public float Damage = 20;

    //
    public GameObject Owner;

    // Components
    private Transform trans;
    private CharacterController2D controller;

    // State
    private Vector2 velocity;

    void Awake()
    {
        trans = GetComponent<Transform>();
        controller = GetComponent<CharacterController2D>();
    }

    void Start()
    {
        velocity = trans.right * MoveSpeed;
        Destroy(gameObject, LiveTime);
    }

    void Update()
    {
        RaycastHit2D[, ] collisionInfo = controller.Move(velocity * Time.deltaTime);
        RaycastHit2D hitInfo = collisionInfo[0, 0];

        if (hitInfo.collider != null)
        {
            Damageable damageable = hitInfo.collider.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.ReceiveDamage(Damage, Owner);
            }

            Destroy(gameObject);
        }
    }
}
