using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTheDoor : MonoBehaviour {

    public float distance = 2f;
    public GameObject door;

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, door.transform.position);


        if (Input.GetKeyDown(KeyCode.O))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, distance))
            {
                if (hit.collider.CompareTag("door"))
                {
                    hit.collider.transform.parent.GetComponent<DoorScript>().ChangeDoorState();
                }
            }
        }

    }


}