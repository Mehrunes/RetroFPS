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
    public bool sawPlayer = false;


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
        Vector3 origin = new Vector3(transform.position.x, transform.position.y + 1.8f, transform.position.z);
        angleToPlayer = (Vector3.Angle(direction, transform.forward));
        Ray myRay = new Ray(origin, direction);
        RaycastHit hit;

        if (((angleToPlayer >= lookAngle * -0.5) && (angleToPlayer <= lookAngle * 0.5)) && (Physics.Raycast(myRay, out hit, lookRadius)))  // Gracz w zasięgu wzroku bota
        {
            if (hit.collider.tag == "Player")
            {
                playerInFieldOfView = true;
            }
            else
            {
                playerInFieldOfView = false;
            }
        }
        if (((distance <= lookRadius) && (playerInFieldOfView)) || (heardPlayer)) // Bot wykrył gracza (zobaczył go lub usłyszał)
        {
            agent.SetDestination(target.position);
            Action = "FollowPlayer";
            Debug.Log("FollowPlayer");
            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
            lastKnownPlayerPosition = target.position;
            if(heardPlayer)
            {
                heardPlayer = false;
            }
        }
        else if ((distance > lookRadius) || (!playerInFieldOfView))
        {
            if ((sawPlayer) || (heardPlayer))
            {
                agent.SetDestination(lastKnownPlayerPosition);
                Action = "GoToLastKnownPlayerPosition";
                Debug.Log("GoToLastKnownPlayerPosition");
                sawPlayer = false;
                heardPlayer = false;
            }
            else
            {
                Action = "Idle";
                Debug.Log("Idle");
            }
        }
        if (agent.velocity == Vector3.zero)
        {

        }
	}
    void FaceTarget()
    {
        direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
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
            Debug.Log("AttackPlayer");
            col.gameObject.GetComponent<health>().TakeDamage(0.1f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            heardPlayer = true;
            lastKnownPlayerPosition = target.position;
        }
    }
}
