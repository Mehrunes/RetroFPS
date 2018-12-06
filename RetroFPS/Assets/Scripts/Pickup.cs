using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour,INteracts {
    public bool picked = false;

    public Transform Targetlocation;

    // Update is called once per frame
    void Update () {
		if(picked == true)
        {
            this.transform.position = Targetlocation.position;
        }
	}

    public void OnUse()
    {
        if (picked == false)
        {
            picked = true;
        }
        else {
            picked = false;
        }
    }
}
