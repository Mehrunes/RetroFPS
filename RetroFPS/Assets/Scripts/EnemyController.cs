using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float lookRadius = 30f;
    public float lookAngle = 120f;
    public float meeleAttackCooldown = 2f;
    public float rangedAttackCooldown = 2f;
    public float rangedAttackPower = 10f;
    public float meeleAttackPower = 20f;
    public float distance;
    public Vector3 direction;
    public Vector3 playerPosition;
    public Vector3 lastKnownPlayerPosition;
    public Vector3 agentStartPosition;
    public float angleToPlayer;
    public bool alert;
    public bool heardPlayer;
    public bool isRanged;
    public bool playerInFieldOfView;
    public string Action = "Idle";

    private Transform target;
    private NavMeshAgent agent;
    private SphereCollider col;
    private float rangedAttackTime = 0;
    private float meeleAttackTime = 0;



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
        playerPosition = target.position;
        distance = Vector3.Distance(target.position, transform.position);
        direction = target.position - transform.position;
        Vector3 origin = new Vector3(transform.position.x, transform.position.y + 1.8f, transform.position.z);
        angleToPlayer = (Vector3.Angle(direction, transform.forward));
        Ray myRay = new Ray(origin, direction);
        RaycastHit hit;

        if (((angleToPlayer >= lookAngle * -0.5) && (angleToPlayer <= lookAngle * 0.5)) && (Physics.Raycast(myRay, out hit, lookRadius)))  // Gracz w zasięgu wzroku bota
        {
            playerInFieldOfView = true;
        }
        else
        {
            playerInFieldOfView = false;
        }
        if ((((distance <= lookRadius)) && (playerInFieldOfView)) || (heardPlayer))  // Bot widzi lub słyszy gracza
        {
            alert = true;
            FollowPlayer();
            Action = "Walk";
        }
        if (((distance > lookRadius) || (!playerInFieldOfView)) && (!heardPlayer) && alert)                 // Bot nie widzi i nie słyszy gracza
        {
            if (lastKnownPlayerPosition != Vector3.zero)
            {
                agent.SetDestination(lastKnownPlayerPosition);
                Action = "Walk";
            }
            else
            {
                Action = "Idle";
            }
        }
        if (agent.velocity == Vector3.zero && !alert)   // Bot się nie rusza
        {
            Action = "Idle";
        }
	}

    void FaceTarget()
    {
        direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 100f);
    }

    void FollowPlayer()
    {
        FaceTarget();
        agent.SetDestination(playerPosition);
        lastKnownPlayerPosition = playerPosition;
        Action = "FollowPlayer";
        if ((!isRanged) && (distance <= 3f) && (Time.time >= meeleAttackTime))              // Bot może atakować z bliska
        {
            Action = "AttackPlayer";
            col.gameObject.GetComponent<health>().TakeDamage(meeleAttackPower);
            FollowPlayer();
            meeleAttackTime = Time.time + meeleAttackCooldown;
        }
        if (isRanged && (playerInFieldOfView) && (Time.time > rangedAttackTime))                    // Bot może atakować zasięgowo
        {
            Action = "AttackPlayer";
            target.GetComponent<health>().TakeDamage(rangedAttackPower);
            FollowPlayer();
            rangedAttackTime = Time.time + rangedAttackCooldown;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Playernoize")
        {
            heardPlayer = true;
            FollowPlayer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Playernoize")
        {
            heardPlayer = false;
        }
    }
}
