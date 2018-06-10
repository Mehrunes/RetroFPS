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
	public long  maxAmmunition = 420;
    public long ammunition = 60;

    public float fireRate = 0.14f;
	public float lastShot = 0.0f;

    public AudioSource audioSource;
    public AudioClip audioDamage;

    public AudioSource audioSource2;
    public AudioClip audioGun;

	void Start () 
	{
		currentWeapon = weapons[0];
		ammunitionText.text = ammunition.ToString();
	}
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update () 
	{
		if (ammunition > 0 && (Input.GetButtonDown("Fire1") || (Input.GetButton("Fire2") && Time.time > lastShot + fireRate))) 
		{
			var rocket = Instantiate (currentWeapon, barrelEnd.position, barrelEnd.rotation);
			rocket.AddForce (barrelEnd.forward * force);
			lastShot = Time.time;
            if (audioSource != null)
            {
                audioSource.PlayOneShot(audioDamage);
            }
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

    public void AddAmmunition()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            long tempAmmunition = maxAmmunition - ammunition;
            int amm = 10;
            if (amm > maxAmmunition)
            {
                ammunition += tempAmmunition;
                UpdateAmmoText();
                PlaySound();
            }
            if (ammunition < maxAmmunition)
            {
                ammunition += amm;
                UpdateAmmoText();
                PlaySound();
            }
            if (ammunition > maxAmmunition)
            {
                ammunition = maxAmmunition;
            }
        }
    }

    //public void AddAmmunition(int amm)
    //{
    //    int tempAmmunition = maxAmmunition - ammunition;

    //    if (ammunition > maxAmmunition)
    //    {
    //        amm += tempAmmunition;
    //        //PlaySound();
    //    }
    //    if (ammunition < maxAmmunition)
    //    {
    //        ammunition += amm;
    //        //PlaySound();
    //    }
    //    if (ammunition > maxAmmunition)
    //        ammunition = maxAmmunition;
    //}
    //do amunicji
    public long getMaxAmmunition()
    {
        return maxAmmunition;
    }
    public long getAmmunition()
    {
        return ammunition;
    }
    void PlaySound()
    {
        audioSource.clip = audioGun;
        audioSource.Play();

    }
    public void SwitchWeapon() 
	{
		if(Input.GetKeyDown(KeyCode.Tab)) 
		{
			currentWeapon = currentWeapon.Equals(weapons[0]) ? weapons[1] : weapons[0];
		}
	}
}