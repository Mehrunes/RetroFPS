using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour 
{
	public const float reach = 10f;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.E)) 
		{
			var ray = new Ray(transform.position, transform.forward);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, reach))
			{
				if(hit.collider.CompareTag("Door")) 
				{
					hit.collider.transform.parent.parent.GetComponent<DoorScript>().ChangeDoorState();
				}
			}
		}
	}
}
