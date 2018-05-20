	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsingInstantiate : MonoBehaviour {

	public Text ammunitionText;
	public List<Rigidbody> weapons;
	public Rigidbody currentWeapon;
	public Transform barrelEnd;
	public const int force = 5000;
	public int ammunition = 60;
	public float fireRate = 0.14f;
	public float lastShot = 0.0f;

	void Start () 
	{
		currentWeapon = weapons[0];
		ammunitionText.text = ammunition.ToString();
	}

	void Update () 
	{
		if (ammunition > 0 && (Input.GetButtonDown("Fire1") || (Input.GetButton("Fire2") && Time.time > lastShot + fireRate))) 
		{
			var rocket = Instantiate (currentWeapon, barrelEnd.position, barrelEnd.rotation);
			rocket.AddForce (barrelEnd.forward * force);
			lastShot = Time.time;
			ammunition--;
			UpdateAmmoText ();
		}

		if (Input.GetAxis ("Mouse ScrollWheel") > 0f) // forward
		{
			fireRate -= 0.01f;
		}
		
		if (Input.GetAxis ("Mouse ScrollWheel") < 0f) // backwards
		{
			fireRate += 0.01f;
		}

		AddAmmunition();
		SwitchWeapon();
	}

	public void UpdateAmmoText () 
	{
		ammunitionText.text = ammunition.ToString();
	}

	public void AddAmmunition ()
	{
		if (Input.GetKeyDown (KeyCode.R)) 
		{
			ammunition += 10;
			UpdateAmmoText();
		}
	}

	public void SwitchWeapon() 
	{
		if(Input.GetKeyDown(KeyCode.Tab)) 
		{
			currentWeapon = currentWeapon.Equals(weapons[0]) ? weapons[1] : weapons[0];
		}
	}
}