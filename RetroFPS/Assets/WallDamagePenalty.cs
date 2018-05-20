using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDamagePenalty : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerStay(Collider col)
    {

        if (col.gameObject.name == "Aktor2137")//albo  (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<health>().TakeDamage(0.1f);

        }

    }
}
