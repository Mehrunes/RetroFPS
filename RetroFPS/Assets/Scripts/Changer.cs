using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Changer : MonoBehaviour {
    public int timer=0;
    public int FrameNum=0;

    public Sprite[] Atak = new Sprite[3];
    public Sprite[] Lewo = new Sprite[3];
    public Sprite[] Prawo = new Sprite[3];
    public Sprite[] Przod = new Sprite[3];
    public Sprite[] Tyl = new Sprite[3];
    public Sprite idlePrzod;
    public Sprite idleTyl;
    public Sprite idleLewo;
    public Sprite idlePrawo;

    public String Action;

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
        timer++;
        if (timer > 15) {
            FrameNum++;
            if (FrameNum > 2) { FrameNum = 0; }

            timer = 0;
        }
        //katy oczu i obiektu
        ObjectRotation = parentObject.transform.rotation.eulerAngles.y +180;
        CameramanRotation = Cameraman.transform.rotation.eulerAngles.y +180;
        //wartosc kata miedzy aktorem a spritem nie dziala jak chuj
        angleObjectCamera = Abs(CameramanRotation - ObjectRotation);

        /*
            this.GetComponent<SpriteRenderer>().sprite = spBack;//Przod
        if (angleObjectCamera > 45 && angleObjectCamera <135)
            this.GetComponent<SpriteRenderer>().sprite = spLeft;
        if (angleObjectCamera < 225 && angleObjectCamera > 135)
            this.GetComponent<SpriteRenderer>().sprite = spFront;//Tyl
        if (angleObjectCamera >225 && angleObjectCamera <315)
            this.GetComponent<SpriteRenderer>().sprite = spRight;
            */
        Action = GetComponentInParent<EnemyController>().Action;

        if (Action == "Idle") {
                this.GetComponent<SpriteRenderer>().sprite = idlePrzod;//Przod (w sensie patrzy w przod)
            if (angleObjectCamera > 45 && angleObjectCamera < 135)
                this.GetComponent<SpriteRenderer>().sprite = idleLewo;
            if (angleObjectCamera < 225 && angleObjectCamera > 135)
                this.GetComponent<SpriteRenderer>().sprite = idleTyl;//Tyl
            if (angleObjectCamera > 225 && angleObjectCamera < 315)
                this.GetComponent<SpriteRenderer>().sprite = idlePrawo;

        }

        if (Action == "Walk")
        {
                this.GetComponent<SpriteRenderer>().sprite = Przod[FrameNum];//Przod
            if (angleObjectCamera > 45 && angleObjectCamera < 135)
                this.GetComponent<SpriteRenderer>().sprite = Lewo[FrameNum];
            if (angleObjectCamera < 225 && angleObjectCamera > 135)
                this.GetComponent<SpriteRenderer>().sprite = Tyl[FrameNum];//Tyl
            if (angleObjectCamera > 225 && angleObjectCamera < 315)
                this.GetComponent<SpriteRenderer>().sprite = Prawo[FrameNum];
        }

        if (Action == "AttackPlayer")
        {
            this.GetComponent<SpriteRenderer>().sprite = Atak[FrameNum];//Przod

        }

        //obrot tylko wokol x,z
        Vector3 targetPostition = new Vector3(Cameraman.transform.position.x,
                                       this.transform.position.y,
                                       Cameraman.transform.position.z);
        this.transform.LookAt(targetPostition);

    }
}
