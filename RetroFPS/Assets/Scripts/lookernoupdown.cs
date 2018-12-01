using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookernoupdown : MonoBehaviour
{
    private Transform target;
    private float distance;
    private Vector3 direction;
    // Use this for initialization
    void Start()
    {
        target = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);
        direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1000f);
    }
}
