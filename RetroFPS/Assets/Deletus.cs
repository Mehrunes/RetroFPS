using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deletus : MonoBehaviour {

    public GameObject bulki;
    public Mission mission;
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "Player")
        {
            mission.GetComponent<Mission>();
            mission.NextObjective();
            Destroy(bulki);
       
        }
    }
}
