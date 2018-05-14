using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingInstantiate : MonoBehaviour {

	public Rigidbody rocketPrefab;
	public Transform barrelEnd;
	public const int force = 5000;
	public float fireRate = 0.14f;
	public float lastShot = 0.0f;

	void Update () {
		if(Input.GetButtonDown("Fire1") || (Input.GetButton("Fire2") && Time.time > lastShot + fireRate )) {
			var rocket = Instantiate(rocketPrefab, barrelEnd.position, barrelEnd.rotation) as Rigidbody;
			rocket.AddForce(barrelEnd.forward * force);
			lastShot = Time.time;
		}

		if (Input.GetAxis("Mouse ScrollWheel") > 0f ) // forward
         {
             fireRate -= 0.01f;
         }
         if (Input.GetAxis("Mouse ScrollWheel") < 0f ) // backwards
         {
             fireRate += 0.01f;
         }
	}
}
