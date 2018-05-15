using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Changer : MonoBehaviour {

    public Sprite spFront, spLeft, spRight, spBack;
    public GameObject Cameraman;
    private Quaternion ObjectRotation;
    private Quaternion CameramanRotation;
    public float angleObjectCamera;
    private float fixedrotation;
	// Use this for initialization
	void Start () {
		
	}
	
    private float Abs(float data)
    {
        if (data < 0)
            return -data;
        else
            return data;

    }
    

	// Update is called once per frame
	void Update () {
        //katy oczu i obiektu
        ObjectRotation = this.transform.parent.rotation;
        CameramanRotation = Cameraman.transform.rotation;
        //wartosc kata miedzy aktorem a spritem nie dziala jak chuj
        angleObjectCamera = Abs(CameramanRotation.y - ObjectRotation.y);


        if (angleObjectCamera<0.25)
            this.GetComponent<SpriteRenderer>().sprite = spBack;
        if (angleObjectCamera > 0.25 && angleObjectCamera <0.5)
            this.GetComponent<SpriteRenderer>().sprite = spLeft;
        if (angleObjectCamera < 0.75 && angleObjectCamera > 0.5)
            this.GetComponent<SpriteRenderer>().sprite = spRight;
        if (angleObjectCamera >0.75)
            this.GetComponent<SpriteRenderer>().sprite = spFront;

        //obrot tylko wokol x,z
        Vector3 targetPostition = new Vector3(Cameraman.transform.position.x,
                                       this.transform.position.y,
                                       Cameraman.transform.position.z);
        this.transform.LookAt(targetPostition);

    }
}
