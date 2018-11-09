using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{
	public enum WallSide { Top, Bottom, Left, Right }

	[Serializable]
	public class DoorInfo
	{
		public WallSide Wall;

		[Range(-1, 1)]
		public float position;
	}

	// Inspector settings
	public Vector2 RoomDimensions = new Vector2(40, 40);
	public GameObject WallPrefab;
	public GameObject DoorPrefab;
	public float WallWidth = 0.5f;
	public DoorInfo[] Doors;

	void Awake()
	{
		SpawnWalls();

		SpawnDoors();
	}

	private void SpawnWalls()
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

	private void SpawnDoors()
	{
		for (int i = 0; i < Doors.Length; i++)
		{
			DoorInfo doorInfo = Doors[i];
			GameObject door = Instantiate(DoorPrefab);

			door.transform.localScale = new Vector2(WallWidth, 1);

			if (doorInfo.Wall == WallSide.Top || doorInfo.Wall == WallSide.Bottom)
			{
				door.transform.position = new Vector2(
					(RoomDimensions.x / 2 - WallWidth) * doorInfo.position,
					((doorInfo.Wall == WallSide.Top) ? 1 : -1) * (RoomDimensions.y / 2 + WallWidth * 0.5f)
				);

				door.transform.localScale = new Vector2(1, WallWidth);
			}
			else
			{
				door.transform.position = new Vector2(
					((doorInfo.Wall == WallSide.Right) ? 1 : -1) * (RoomDimensions.x / 2 + WallWidth * 0.5f),
					(RoomDimensions.y / 2 - WallWidth) * doorInfo.position
				);

				door.transform.localScale = new Vector2(WallWidth, 1);
			}
		}
	}
}
