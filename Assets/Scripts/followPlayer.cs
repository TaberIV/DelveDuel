using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour {

    public GameObject player;
    public Vector3 offset;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        //offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -1);
	}
}
