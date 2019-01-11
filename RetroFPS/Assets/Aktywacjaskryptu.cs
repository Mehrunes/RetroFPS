using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aktywacjaskryptu : MonoBehaviour {
    public GameObject Skrypcik;

    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        Skrypcik.SetActive(true);
    }
}
