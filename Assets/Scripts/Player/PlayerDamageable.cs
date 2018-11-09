using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamageable : Damageable
{
    public Sprite oneSlime;
    public Sprite twoSlime;

    public override void Death()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

    public override void ReceiveDamage(float damage, GameObject damager = null)
    {
        base.ReceiveDamage(damage, damager);
        Image currHealth = GameObject.Find("Health").GetComponent<Image>();

        if(Health == 40)
        {
            currHealth.sprite = twoSlime;
        }
        else if(Health == 20)
        {
            currHealth.sprite = oneSlime;
        }
    }
}
