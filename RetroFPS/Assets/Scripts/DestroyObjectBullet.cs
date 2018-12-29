using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectBullet : MonoBehaviour {

    public GameObject destroyObject;

    void Update ()
    {

	}
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Debug.Log("strzelam");
            GameObject destroy= Instantiate(destroyObject, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(destroy, 5f);
        }
}
}
