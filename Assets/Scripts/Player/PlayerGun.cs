using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
	// Inspector Settings
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	public float Radius = 0.5f;
	public float MouseMin = 0.5f;

	// Components
	private Transform trans;

	// State
	private Vector2 aim;

	void Awake()
	{
		// GetComponents
		trans = GetComponent<Transform>();

		// Initialize State
		aim = Vector2.right;
		Rotate();
	}

	void Update()
	{
		GetInput();
		Rotate();

		if (Input.GetButton("Fire"))
		{
			Fire();
		}
	}

	private void GetInput()
	{
		Vector2 aimInput = new Vector2(
			Input.GetAxis("AimHorizontal"),
			Input.GetAxis("AimVertical")
		);

		if (aimInput.magnitude != 0)
		{
			aim = aimInput.normalized;
		}
		// Intended for aiming at mouse
		else
		{
			Vector2 mouseInput = new Vector2(
				Input.GetAxis("Mouse X"),
				Input.GetAxis("Mouse Y")
			);

			if (mouseInput.magnitude > MouseMin)
			{
				aim = mouseInput.normalized;
			}
		}
	}

	private void Rotate()
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
