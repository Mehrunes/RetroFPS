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

        if (col.gameObject.name == "Player")//albo  (col.gameObject.tag == "Player")
        {
            if (!(col.gameObject.GetComponent<health>().getHiPoint() == col.gameObject.GetComponent<health>().getMaxHiPoint()))
            {
                col.gameObject.GetComponent<health>().TakeHealth(20.0f);
                Destroy(gameObject);
            }
     
        }

    }

}
