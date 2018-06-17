using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RetroFPS.Assets.Scripts
{
    public class RaycastShooting : MonoBehaviour {

    public int gunDamage = 1;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;
    public float hitForce = 3000f;   
    public Transform gunEnd;

    private Camera fpsCam;                                              
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private AudioSource gunAudio;
 
    private float nextFire;


    void Start () 
    {
        gunAudio = GetComponent<AudioSource>();

        fpsCam = GetComponentInParent<Camera>();
    }
    

    void Update () 
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire) 
        {
            nextFire = Time.time + fireRate;

            StartCoroutine (ShotEffect());

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f));

            RaycastHit hit;

           // laserLine.SetPosition (0, gunEnd.position);

            Debug.Log("WYWOLANO DMAGE");

            if (Physics.Raycast (rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
               // laserLine.SetPosition (1, hit.point);
                health health1 = hit.collider.gameObject.GetComponent<health>();

                if (health1 != null)
                {
                    health1.TakeDamage (gunDamage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce (-hit.normal * hitForce);
                }
            }
            else
            {
               // laserLine.SetPosition (1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
        }
    }


    private IEnumerator ShotEffect()
    {
        gunAudio.Play ();

       // laserLine.enabled = true;

        yield return shotDuration;

       // laserLine.enabled = false;
    }
    }
}