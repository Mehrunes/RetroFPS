using UnityEngine;

namespace RetroFPS.Assets.Scripts {
    public class RocketLauncher : MonoBehaviour,  Weapon {

    public Rigidbody currentWeapon;
	public Transform barrelEnd;
	public const int force = 5000;
	public long  maxAmmunition = long.MaxValue;
    public long ammunition = long.MaxValue;

    public float fireRate = 0.14f;
	public float lastShot = 0.0f;

    public AudioSource audioSource;
    public AudioClip audioDamage;

    public AudioSource audioSource2;
    public AudioClip audioGun;

	void Start () 
	{

	}
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
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