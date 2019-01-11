using UnityEngine;
using UnityEngine.UI;

namespace RetroFPS.Assets.Scripts {
    public class Pistol : MonoBehaviour,  Weapon {


    public Text amoText;

    public Rigidbody currentWeapon;
	public Transform barrelEnd;
	public const int force = 5000;
	public long  maxAmmunition = long.MaxValue;
    public long ammunition = long.MaxValue;

    public float fireRate = 0.10f;
	public float lastShot = 0.0f;

    public AudioSource audioSource;
    public AudioClip audioDamage;
        
    public AudioSource audioSource2;
    public AudioClip audioGun;

	void Start () 
	{
            amoText.text = "";
            UpdateTextAmo(); 

    }
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
    }
        // Update is called once per frame
    public void UpdateTextAmo()
    {   
        if (ammunition >= 0)
        {
            amoText.text = ammunition.ToString()+"/"+maxAmmunition.ToString();
        }
    }
    public void shoot () 
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
                UpdateTextAmo();
        }

	}

	public void UpdateAmmoText () 
	{
	
	}

    public long getMaxAmmunition()
    {
        return maxAmmunition;
    }
    public long getAmmunition()
    {
        return ammunition;
    }

    }
}