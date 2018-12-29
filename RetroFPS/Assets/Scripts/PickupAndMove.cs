using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAndMove : MonoBehaviour {

    public Camera player;
    public Transform moveThis;
    private bool move = false;
    public GameObject tempParent;
    public Transform guide;
    Ray ray;
    RaycastHit hit;

    void FixedUpdate()
    {
        ray = player.ScreenPointToRay(Input.mousePosition);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (move == false)
            { 
                if (Physics.Raycast(ray, out hit, 1f))
                {
                    if (hit.collider.transform==moveThis)
                    {
                        moveThis.GetComponent<Rigidbody>().useGravity = false;
                        moveThis.GetComponent<Rigidbody>().isKinematic = true;
                        //moveThis.GetComponent<BoxCollider>().enabled = true;
                        moveThis.transform.position = guide.transform.position;
                        moveThis.transform.rotation = guide.transform.rotation;
                        moveThis.transform.parent = tempParent.transform;
                        move = true;
                    }
                }
            }
            else if (move == true)
            {

                moveThis.GetComponent<Rigidbody>().useGravity = true;
                moveThis.GetComponent<Rigidbody>().isKinematic = false;
                moveThis.transform.parent = null;
                move = false;
            }
        }

    }



    //public GameObject tempParent;
    //public GameObject moveObject;
    //public Transform guide;
    //private bool move = false;

    ////Update is called once per frame

    //void Update()
    //{
    //    if (move == true)
    //    {
    //        if (Input.GetKeyDown(KeyCode.F))
    //        {
    //            moveObject.GetComponent<Rigidbody>().useGravity = false;
    //            moveObject.GetComponent<Rigidbody>().isKinematic = true;
    //            moveObject.transform.position = guide.transform.position;
    //            moveObject.transform.rotation = guide.transform.rotation;
    //            moveObject.transform.parent = tempParent.transform;
    //            move = false;
    //        }
    //    }

    //}
    //void OnTriggerEnter(Collider col)
    //{
    //    if (col.name == "box")
    //    {
    //        moveObject = col.gameObject;
    //        move = true;
    //    }

    //}
    //void OnTriggerExit(Collider col)
    //{
    //    if (col.name == "box")
    //    {
    //        moveObject = null;
    //        move = false;
    //    }

    //}
}
