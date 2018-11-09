using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class TurretBehavior : BaddyBehavior
{
    // Inpsector Settings
    public float RecoilSpeed = 2f;
    public GameObject ProjectilePrefab;
    public float ProjectileTimeMax = 3;
    public float ProjectileTimeMin = 1;

    // Components
    private Transform trans;
    private CharacterController2D controller;

    // Other objects
    private Transform player;

    // State
    float projectileTimer;

    protected override void Awake()
    {
        // GetComponents
        trans = GetComponent<Transform>();
        controller = GetComponent<CharacterController2D>();

        // Initialize State
        projectileTimer = ProjectileTimeMax;
        base.Awake();
    }

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        Vector3 dir = (player.position - trans.position).normalized;

        if (!Recoil)
        {
            if (projectileTimer <= 0)
            {
                GameObject projectile = Instantiate(
                    ProjectilePrefab,
                    trans.position + ((Vector3) dir),
                    Quaternion.Euler(
                        0,
                        0,
                        Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x)
                    )
                );

                projectile.GetComponent<BulletMovement>().Owner = gameObject;
                projectileTimer = Random.Range(ProjectileTimeMin, ProjectileTimeMax);
            }

            projectileTimer -= Time.deltaTime;
        }
        else
        {
            controller.Move(-dir * RecoilSpeed * Time.deltaTime);

            recoilTimer -= Time.deltaTime;
        }
    }
}
