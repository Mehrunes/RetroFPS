using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usuwarka : MonoBehaviour
{
    public GameObject[] obiekty = new GameObject[4];

    // Use this for initialization


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < 4; i++) {
                Destroy(obiekty[i]);
            }
        }
    }
}