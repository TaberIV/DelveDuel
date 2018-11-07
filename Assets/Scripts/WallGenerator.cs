using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{
	// Inspector settings
	public Vector2 RoomDimensions;
	public GameObject WallPrefab;
	public float WallWidth;

	void Awake()
	{
		GameObject top = Instantiate(WallPrefab, new Vector2(0, -RoomDimensions.y / 2));
	}
}
