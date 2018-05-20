using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDamage : MonoBehaviour {

	// Update is called once per frame
	void Update ()
    {

	}

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.name == "Aktor2137" || col.gameObject.name == "Testsprite")
        {
            col.gameObject.GetComponent<health>().TakeDamage(9);

        }

    }
}
