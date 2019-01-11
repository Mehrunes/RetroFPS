using System.Collections;
using System.Collections.Generic;
using RetroFPS.Assets.Scripts;
using UnityEngine;

public class RocketLauncherAmmunition : MonoBehaviour {

    // Use this for initializatio
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
    void OnTriggerEnter (Collider col) {
        if (col.gameObject.name == "Player") {
            var eyes = col.gameObject.transform.GetChild (0).gameObject;
            var gun = eyes.gameObject.transform.GetChild (0).gameObject;
            var currentWeapon = gun.GetComponent<SwitchWeapon> ().currentWeapon;
            var weapon = gun.GetComponent<SwitchWeapon> ().weapons[2] as RocketLauncher;

            if (weapon.ammunition < weapon.maxAmmunition) {
                weapon.ammunition += 15;
                if (weapon.ammunition > weapon.maxAmmunition) {
                    weapon.ammunition = weapon.maxAmmunition;
                }
            }
            if(currentWeapon == weapon) {
                weapon.UpdateTextAmo ();
            }
            Destroy (gameObject);
        }
    }
}