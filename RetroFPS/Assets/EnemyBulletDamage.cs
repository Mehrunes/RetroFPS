using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletDamage : MonoBehaviour
{

    public float DamageValue;

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Player")
        {
            health health1 = col.gameObject.GetComponent<health>();
            col.gameObject.GetComponent<health>().TakeDamage(DamageValue);

        }
        Destroy(this, 0.2f);
    }
}
