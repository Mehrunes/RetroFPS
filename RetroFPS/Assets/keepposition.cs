using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepposition : MonoBehaviour {
	// Use this for initialization
	// Update is called once per frame
	void Update () {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        transform.localScale = new Vector3(1, 1, 1);


    }
}
