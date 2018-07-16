using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStairs : MonoBehaviour {

	private bool triggered;
	public GameObject player;
	public Vector3 playerpos;

    public Transform spawnpoint;
    public GameObject prefab;
	// Use this for initialization

	void Start() {
		triggered = false;
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update() {
		
		playerpos = player.transform.position;
		float distance = Vector3.Distance(transform.position, playerpos);
		if (!triggered && distance <= 2.0f) {
			Instantiate (prefab, spawnpoint.position, spawnpoint.rotation); 
			triggered = true;
		}
	}


	/*
	void OnTriggerEnter (Collider col) {
        Debug.Log("SPAWN STAIR");
	
		if (col.tag == "Player" && !triggered) {
			triggered = true;
			Instantiate (prefab, spawnpoint.position, spawnpoint.rotation); 
		}
        
	}
	*/
	
}
