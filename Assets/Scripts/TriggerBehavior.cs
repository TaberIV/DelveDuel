using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerBehavior : MonoBehaviour
{
	public abstract void OnTriggerEnter2D(Collider2D col);
}
