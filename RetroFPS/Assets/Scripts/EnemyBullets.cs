using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullets : MonoBehaviour
{
    public float DamageValue;
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            health health = other.gameObject.GetComponent<health>();
            other.gameObject.GetComponent<health>().TakeDamage(DamageValue);
        }
    }
}