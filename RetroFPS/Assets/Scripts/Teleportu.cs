using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportu : MonoBehaviour {


    public Vector3 testmap = new Vector3(0f,0f,0f);
    public Vector3 playmap = new Vector3 (-11.6f, 100.08f, 453.8f);
    private bool existance = true; //true dla testmapy false dla prezentacyjnej
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(existance == true)
            {
                transform.position = playmap;
                existance = false;
            }
            if (existance == false)
            {
                transform.position = playmap;
                existance = true;
            }
        }
	}
}
