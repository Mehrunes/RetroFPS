using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookernoupdown : MonoBehaviour {
    private  GameObject Cameraman;
	// Use this for initialization
	void Start () {
        Cameraman = GameObject.Find("Agent2137/Actor Eyes");
    }
    
	// Update is called once per frame
	void Update () {
        
        Vector3 targetPostition = new Vector3(Cameraman.transform.position.x,
                               this.transform.position.y,
                                Cameraman.transform.position.z);
        this.transform.LookAt(targetPostition);

    }
}
