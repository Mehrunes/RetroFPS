using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {
    public Transform targetlocation;
    public GameObject whatToMove;
    public GameObject Player;
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        whatToMove.SetActive(true);
        Player.transform.position = targetlocation.position;
        Player.transform.rotation = targetlocation.rotation;
    }
}
