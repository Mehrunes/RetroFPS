using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifespan : MonoBehaviour {

    public int length;
    public int current;
    void Update () {

        if (length <= current)
        {
            Destroy(this.gameObject);
        }
        current++;
	}
}
