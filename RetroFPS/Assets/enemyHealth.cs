using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour {

	// Use this for initialization
    public float hitpoint = 100;
    private float maxHitpoint = 100;

	// Update is called once per frame
	void Start()
	{
		Debug.Log(hitpoint);	
	}
	public void TakeDamage(float damage)
    {
		print(damage);
        hitpoint -= damage;
        int hitpoint_int = (int)hitpoint;
        if (hitpoint_int <= 0)
        {
            hitpoint = 0;
            hitpoint_int = 0;
            Destroy(gameObject);
            Debug.Log("Dead!");
        }
    }
}
