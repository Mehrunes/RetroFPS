using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Changer : MonoBehaviour {

    public Sprite spFront, spLeft, spRight, spBack;
    public GameObject Cameraman;
    public GameObject parentObject;
    private float ObjectRotation;
    private float CameramanRotation;
    public float angleObjectCamera;

	//absolute value
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
        ObjectRotation = parentObject.transform.rotation.eulerAngles.y +180;
        CameramanRotation = Cameraman.transform.rotation.eulerAngles.y +180;
        //wartosc kata miedzy aktorem a spritem nie dziala jak chuj
        angleObjectCamera = Abs(CameramanRotation - ObjectRotation);


            this.GetComponent<SpriteRenderer>().sprite = spBack;
        if (angleObjectCamera > 45 && angleObjectCamera <135)
            this.GetComponent<SpriteRenderer>().sprite = spLeft;
        if (angleObjectCamera < 225 && angleObjectCamera > 135)
            this.GetComponent<SpriteRenderer>().sprite = spFront;
        if (angleObjectCamera >225 && angleObjectCamera <315)
            this.GetComponent<SpriteRenderer>().sprite = spRight;

        //obrot tylko wokol x,z
        Vector3 targetPostition = new Vector3(Cameraman.transform.position.x,
                                       this.transform.position.y,
                                       Cameraman.transform.position.z);
        this.transform.LookAt(targetPostition);

    }
}
