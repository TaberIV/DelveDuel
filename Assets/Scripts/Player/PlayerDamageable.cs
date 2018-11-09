using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamageable : Damageable
{
	public override void Death()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
