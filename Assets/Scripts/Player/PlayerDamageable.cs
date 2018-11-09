using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDamageable : Damageable
{
    public Sprite oneSlime;
    public Sprite twoSlime;
    public Sprite threeSlime;

    private Image currHealth;

    protected override void Awake()
    {
        base.Awake();
        currHealth = GameObject.Find("Health").GetComponent<Image>();

        currHealth.sprite = threeSlime;
    }

    public override void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public override void ReceiveDamage(float damage, GameObject damager = null)
    {
        base.ReceiveDamage(damage, damager);

        if (Health == 40)
        {
            currHealth.sprite = twoSlime;
        }
        else if (Health == 20)
        {
            currHealth.sprite = oneSlime;
        }
    }
}
