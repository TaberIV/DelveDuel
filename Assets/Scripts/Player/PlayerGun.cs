using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
	// Inspector Settings
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	public float Radius = 0.5f;

	// Components
	private Transform trans;

	// State
	private Vector2 aim;

	void Awake()
	{
		// GetComponents
		trans = GetComponent<Transform>();

		// Initialize State
		Rotate(Vector2.right);
	}

	void Update()
	{
		aim = new Vector2(
			Input.GetAxis("AimHorizontal"),
			Input.GetAxis("AimVertical")
		);

		if (aim.magnitude != 0)
		{
			aim.Normalize();
			Rotate(aim);
		}

		if (Input.GetButton("Fire"))
		{
			Fire();
		}
	}

	private void Rotate(Vector2 aim)
	{
		trans.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(aim.y, aim.x));
		trans.localPosition = aim * Radius;
	}

	private void Fire()
	{
		GameObject bullet = Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation
		);
	}
}
