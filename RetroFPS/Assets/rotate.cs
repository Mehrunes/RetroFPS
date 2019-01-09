using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {
    // Update is called once per frame

    public int speed = 10;
	void Update () {
        this.transform.Rotate(Vector3.up * Time.deltaTime * speed);
    }
}
