using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float lookRadius = 10f;
    public float meeleRange = 2f;
    public float lookAngle = 120f;
    public bool playerInFieldOfView = false;
    public float distance;
    public Vector3 direction;
    public Vector3 lastKnownPlayerPosition = Vector3.zero;
    public float angleToPlayer;
    public bool alert;
    public bool heardPlayer = false;


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
        direction = target.position - transform.position;
        angleToPlayer = (Vector3.Angle(direction, transform.forward));
        if ((angleToPlayer >= lookAngle * -0.5) && (angleToPlayer <= lookAngle * 0.5))
        {
            playerInFieldOfView = true;
        }
        else
        {
            playerInFieldOfView = false;
        }

        if (((distance <= lookRadius) && (playerInFieldOfView)) || (heardPlayer))
        {
            Action = "FollowPlayer";
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
            lastKnownPlayerPosition = target.position;
        }
        if ((distance > lookRadius) || (!playerInFieldOfView))
        {
            if (alert)
            {
                agent.SetDestination(lastKnownPlayerPosition);
                Action = "GoToLastKnownPlayerPosition";
            }
            else
            {
                Action = "Idle";
            }
        }
	}
    void FaceTarget()
    {
        direction = (target.position - transform.position).normalized;
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
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            heardPlayer = true;
        }
    }
}
