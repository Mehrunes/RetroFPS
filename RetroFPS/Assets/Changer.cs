using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changer : MonoBehaviour {

    public Sprite spFront, spLeft, spRight, spBack;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
            this.GetComponent<SpriteRenderer>().sprite = spFront;
        if (Input.GetKey(KeyCode.A))
            this.GetComponent<SpriteRenderer>().sprite = spLeft;
        if (Input.GetKey(KeyCode.D))
            this.GetComponent<SpriteRenderer>().sprite = spRight;
        if (Input.GetKey(KeyCode.S))
            this.GetComponent<SpriteRenderer>().sprite = spBack;
    }
}
