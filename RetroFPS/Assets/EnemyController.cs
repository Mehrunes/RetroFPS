using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float lookRadius = 10f;
    public float meeleRange = 2f;
    public float lookAngle = 110f;
    public bool playerInSight;
    public float distance;
    Vector3 lastKnownPlayerPosition;


    private Transform target;
    private NavMeshAgent agent;
    private SphereCollider col;


    string Action = "Idle";

	// Use this for initialization
	void Start () {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            Action = "FollowPlayer";
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
            lastKnownPlayerPosition = target.position;
        }
        if (distance > lookRadius)
        {
            agent.SetDestination(lastKnownPlayerPosition);
            Action = "Idle";
        }
	}
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    void OnTriggerStay(Collider col)
    {
        if ((col.gameObject.tag == "Player") && (distance < meeleRange))
        {
            Action = "AttackPlayer";
            col.gameObject.GetComponent<health>().TakeDamage(0.1f);
        }
    }
}
