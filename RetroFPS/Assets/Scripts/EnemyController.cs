using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float lookRadius = 20f;
    public float meeleRange = 2f;
    public float rangedRange = 15f;
    public float lookAngle = 120f;
    public bool playerInFieldOfView = false;
    public float distance;
    public Vector3 direction;
    public Vector3 lastKnownPlayerPosition;
    public Vector3 agentStartPosition;
    public float angleToPlayer;
    public bool alert;
    public bool heardPlayer = false;
    public bool sawPlayer = false;
    public bool noAmmo = false;


    private Transform target;
    private NavMeshAgent agent;
    private Transform agentTransform;
    private SphereCollider col;


    string Action = "Idle";

	// Use this for initialization
	void Start () {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        agentTransform = GetComponent<Transform>();
        col = GetComponent<SphereCollider>();
        agentStartPosition = agentTransform.position;
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
        if (((distance <= lookRadius) && (playerInFieldOfView)) || (heardPlayer))                       // Bot widzi lub słyszy gracza
        {
            agent.SetDestination(target.position);
            sawPlayer = true;
            lastKnownPlayerPosition = target.position;
            Action = "FollowPlayer";
            Debug.Log("Follow Player");
            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
                heardPlayer = false;
        }
        else if (((distance > lookRadius) || (!playerInFieldOfView)) && (!heardPlayer))                 // Bot nie widzi i nie słyszy gracza
        {
            agent.SetDestination(lastKnownPlayerPosition);
            Action = "GoToLastKnownPlayerPosition";
            Debug.Log("Go To Last Known Player Position");
        }
        else if ((agent.velocity == Vector3.zero) && (agentTransform.position == agentStartPosition))   // Bot nie widzi i nie słyszy gracza i stoi na swojej początkowej pozycji
        {
            Action = "Idle";
            Debug.Log("Idle");
        }
        else                                                                                            // Bot nie widzi i nie słyszy gracza i nie stoi na swojej początkowej pozycji
        {
            WaitForSeconds SearchingForPlayer = new WaitForSeconds(5);
            agent.SetDestination(agentStartPosition);
            Action = "GoToStartingPosition";
            Debug.Log("GoToStartingPosition");
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
            agent.SetDestination(target.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerSound")
        {
            heardPlayer = true;
            lastKnownPlayerPosition = target.position;
        }
    }
}
