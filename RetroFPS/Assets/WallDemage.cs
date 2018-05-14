using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDemage : MonoBehaviour {

	// Update is called once per frame
	void Update ()
    {

	}

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.name == "Aktor1337")//albo  (col.gameObject.tag == "Plaayer")
        {
            col.gameObject.GetComponent<health>().TakeDamage(9);

        }

    }
}
