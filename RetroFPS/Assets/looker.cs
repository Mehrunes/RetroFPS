using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class looker : MonoBehaviour {
    public Transform target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(target);
	}
}
