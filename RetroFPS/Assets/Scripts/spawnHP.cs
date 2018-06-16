using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnHP : MonoBehaviour {
    public GameObject spawn;
    public GameObject actor;
    private Vector3 spawnpoint;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M) == true)
        {
            spawnpoint = actor.transform.position;
            Instantiate(spawn, transform.position + transform.forward * 2.0f, transform.rotation);
        }

    }
}
