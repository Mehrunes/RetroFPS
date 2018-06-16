using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDamage : MonoBehaviour {

    const float damage = 15f;
	
	void Start () 
	{
		
	}
	
	
	void Update () 
	{
		
	}

    public void OnCollisionEnter(Collision col)
    {
        var center = col.gameObject.transform;
        var radiusF = 5f;
        Destroy(gameObject);
        ExplosionDamage(center.position, radiusF);
    }

    public void ExplosionDamage(Vector3 center, float radius)
    {
        var hitColliders = Physics.OverlapSphere(center, radius);
        foreach(var collider in hitColliders)
        {
            if(collider != null && collider.gameObject.GetComponent<health>() != null) 
            {
                var proximity = (center - collider.gameObject.transform.position).magnitude;
                var effect = 1 - (proximity / radius);
                Debug.Log((uint) (effect * damage));
                collider.gameObject.GetComponent<health>().TakeDamage((uint)(effect * damage));
            }
        }
    }
}
