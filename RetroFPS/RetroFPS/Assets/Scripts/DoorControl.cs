using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour, INteracts {
    Animator animator;
    bool isOpen;

    public void OnUse() {
        if (isOpen == false) { animator.SetTrigger("Open"); isOpen = true; }

       else if(isOpen == true) { animator.SetTrigger("Close"); isOpen = false; }
    }


	void Start () {
        isOpen = false;
        animator = GetComponent<Animator>();
	}
	

	void Update () {//Tylko Do Testów
        if (Input.GetKeyUp(KeyCode.L) == true)
        {
            Debug.Log("AAAA");
            OnUse();
        }
	}
}
