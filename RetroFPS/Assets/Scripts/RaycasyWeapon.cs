using System;
using System.Collections;
using RetroFPS.Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class RaycasyWeapon : MonoBehaviour, Weapon {

    public Text amoText;

    public int gunDamage = 20;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;
    public float hitForce = 3000f;
    public Transform gunEnd;
    public AudioSource gunAudio;

    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds (0.07f);
    private LineRenderer laserLine;
    private float nextFire;

    void Start () {
        amoText.text = "Full";
        UpdateTextAmo();

        laserLine = GetComponent<LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        fpsCam = GetComponentInParent<Camera> ();
    }

    public void UpdateTextAmo()
    {
        amoText.text = "Full";
    }

    public void shoot () {
        if (Input.GetButtonDown ("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;

            StartCoroutine (ShotEffect ());

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, 0.0f));

            RaycastHit hit;

            laserLine.SetPosition (0, gunEnd.position);

            if (Physics.Raycast (rayOrigin, fpsCam.transform.forward, out hit, weaponRange)) {
                laserLine.SetPosition (1, hit.point);
                health health1 = hit.collider.gameObject.GetComponent<health> ();

                if (health1 != null) {
                    health1.TakeDamage (gunDamage);
                }

                if (hit.rigidbody != null) {
                    hit.rigidbody.AddForce (-hit.normal * hitForce);
                }
            } else {
                laserLine.SetPosition (1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
        }
    }

    private IEnumerator ShotEffect () {

        laserLine.enabled = true;

        yield return shotDuration;

        laserLine.enabled = false;
    }

    
}