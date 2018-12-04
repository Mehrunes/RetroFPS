using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float lookRadius = 20f;
    public float meeleRange = 3f;
    public float lookAngle = 120f;
    public float meeleAttackCooldown = 1f;
    public float rangedAttackCooldown = 2f;
    public float rangedAttackPower = 15f;
    public float meeleAttackPower = 10f;
    public bool playerInFieldOfView = false;
    public float distance;
    public Vector3 direction;
    public Vector3 lastKnownPlayerPosition;
    public Vector3 agentStartPosition;
    public float angleToPlayer;
    public bool alert;
    public bool heardPlayer = false;
    public bool inRange = false;
    public bool isRanged = false;


    private Transform target;
    private NavMeshAgent agent;
    private SphereCollider col;
    private float rangedAttackTime;
    private float meeleAttackTime;


    string Action = "Idle";

	// Use this for initialization
	void Start () {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        meeleAttackTime = Time.time;
        rangedAttackTime = Time.time;
        agentStartPosition = transform.position;
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
        if ((((distance <= lookRadius)) && (playerInFieldOfView)) || (heardPlayer))  // Bot widzi lub słyszy gracza
        {
            FaceTarget();
            agent.SetDestination(target.position);
            lastKnownPlayerPosition = target.position;
            Action = "Walk";
            if ((playerInFieldOfView) && (Time.time > rangedAttackTime))                                          // Bot może atakować
            {
                Action = "AttackPlayer";
                target.GetComponent<health>().TakeDamage(rangedAttackPower);
                rangedAttackTime = Time.time + rangedAttackCooldown;
            }
        }
        else if ((((distance > lookRadius)) || (!playerInFieldOfView)) && (!heardPlayer))                 // Bot nie widzi i nie słyszy gracza
        {
            agent.SetDestination(lastKnownPlayerPosition);
            Action = "Walk";
        }
        else if ((agent.velocity == Vector3.zero) && (transform.position == agentStartPosition))   // Bot nie widzi i nie słyszy gracza i stoi na swojej początkowej pozycji
        {
            Action = "Idle";
        }
        else                                                                                            // Bot nie widzi i nie słyszy gracza i nie stoi na swojej początkowej pozycji
        { 
            agent.SetDestination(agentStartPosition);
            Action = "Walk";
        }
	}
    void FaceTarget()
    {
        direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    void FollowPlayer()
    {
        agent.SetDestination(target.position);
        Action = "FollowPlayer";
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    void OnTriggerStay(Collider col)
    {
        if ((!isRanged) && (col.gameObject.tag == "Player") && (distance < meeleRange))
        {
            if (Time.time > meeleAttackTime)
            {
                Action = "AttackPlayer";
                col.gameObject.GetComponent<health>().TakeDamage(meeleAttackPower);
                agent.SetDestination(target.position);
                meeleAttackTime = Time.time + meeleAttackCooldown;
            }
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
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerSound")
        {
            heardPlayer = false;
        }
    }
}
