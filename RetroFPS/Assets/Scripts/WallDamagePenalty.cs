﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDamagePenalty : MonoBehaviour {


    public float DamageValue;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerStay(Collider col)
    {

        if (col.gameObject.tag == "Player")//albo  (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<health>().TakeDamage(DamageValue);

        }

    }
}
