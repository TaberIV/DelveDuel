using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class BulletMovement : MonoBehaviour
{
    // Inspector Settings
    public float MoveSpeed = 5f;
    public float LiveTime = 2f;

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
        controller.Move(velocity * Time.deltaTime);
    }
}
