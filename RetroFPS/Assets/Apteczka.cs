using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apteczka : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.name == "Agent2137")//albo  (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<health>().TakeHealth(20.0f);
            Destroy(gameObject);
        }

    }
}
