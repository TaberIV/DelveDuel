using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{
	// Inspector settings
	public Vector2 RoomDimensions = new Vector2(40, 40);
	public GameObject WallPrefab;
	public float WallWidth = 0.5f;

	void Awake()
	{
		GameObject top = Instantiate(
			WallPrefab,
			new Vector2(0, (RoomDimensions.y + WallWidth) / 2),
			new Quaternion()
		);
		top.transform.localScale = new Vector2(RoomDimensions.x + WallWidth * 2, WallWidth);

		GameObject bottom = Instantiate(
			WallPrefab,
			new Vector2(0, -(RoomDimensions.y + WallWidth) / 2),
			new Quaternion()
		);
		bottom.transform.localScale = new Vector2(RoomDimensions.x + WallWidth * 2, WallWidth);

		GameObject right = Instantiate(
			WallPrefab,
			new Vector2((RoomDimensions.x + WallWidth) / 2, 0),
			new Quaternion()
		);
		right.transform.localScale = new Vector2(WallWidth, RoomDimensions.y + WallWidth * 2);

		GameObject left = Instantiate(
			WallPrefab,
			new Vector2(-(RoomDimensions.x + WallWidth) / 2, 0),
			new Quaternion()
		);
		left.transform.localScale = new Vector2(WallWidth, RoomDimensions.y + WallWidth * 2);
	}
}
