using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDamage : MonoBehaviour {


    public float DamageValue;
	// Update is called once per frame
	void Update ()
    {

	}

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Player" || col.gameObject.tag == "NPC")
        {
            health health1 = col.gameObject.GetComponent<health>();
            col.gameObject.GetComponent<health>().TakeDamage(DamageValue);

        }

    }
}
