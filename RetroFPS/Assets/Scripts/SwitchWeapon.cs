using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RetroFPS.Assets.Scripts 
{
    public class SwitchWeapon : MonoBehaviour 
    {
        public Weapon currentWeapon;
        public Image selectedWeaponSprite;
        public List<Weapon> weapons = new List<Weapon>();
        public List<Sprite> weaponSprites = new List<Sprite>();
        public void switchWeapon () 
        {
            if (Input.GetKeyDown ("1")) 
            {
                currentWeapon= GetComponent<RaycasyWeapon>();
                selectedWeaponSprite.sprite = weaponSprites[0];
            } 
            else if (Input.GetKeyDown ("2")) 
            {
                currentWeapon= GetComponent<Pistol>();
                selectedWeaponSprite.sprite = weaponSprites[1];
            } 
            else if (Input.GetKeyDown ("3")) 
            {
                currentWeapon = GetComponent<RocketLauncher>();
                selectedWeaponSprite.sprite = weaponSprites[2];
            }
        }

        void Start () 
        {
            currentWeapon = GetComponent<RaycasyWeapon>();
            selectedWeaponSprite.sprite = weaponSprites[0];
        }

        void Update () 
        {
            currentWeapon.shoot();
            switchWeapon ();
        }
    }
}