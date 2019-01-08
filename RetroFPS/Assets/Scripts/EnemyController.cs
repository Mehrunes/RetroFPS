using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float lookRadius;
    public float lookAngle;
    public float meeleAttackCooldown;
    public float rangedAttackCooldown;
    public float rangedAttackPower;
    public float meeleAttackPower;
    public float rangedAttackDistance;
    public float distance;
    public float lastPlayerPositionDistance;
    public Vector3 direction;
    public Vector3 playerPosition;
    public Vector3 lastKnownPlayerPosition;
    public float angleToPlayer;
    public bool alert;
    public bool heardPlayer;
    public bool isRanged;
    public bool playerInFieldOfView;
    public string Action;

    private Transform target;
    private GameObject player;
    private NavMeshAgent agent;
    private float rangedAttackTime;
    private float meeleAttackTime;
    private SphereCollider sight;



    // Use this for initialization
    void Start()
    {
        player = PlayerManager.instance.player;
        target = PlayerManager.instance.player.transform;
        agent = this.GetComponent<NavMeshAgent>();
        sight = this.GetComponent<SphereCollider>();
        rangedAttackTime = Time.time;
        meeleAttackTime = Time.time;
        sight.radius = lookRadius;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = target.position;
        distance = Vector3.Distance(target.position, transform.position);
        lastPlayerPositionDistance = Vector3.Distance(lastKnownPlayerPosition, transform.position);
        direction = target.position - transform.position;
        Vector3 origin = new Vector3(transform.position.x, transform.position.y + 1.6f, transform.position.z);
        angleToPlayer = (Vector3.Angle(direction, transform.forward));

        if (((distance <= lookRadius) && (playerInFieldOfView)) || (heardPlayer))  // Bot widzi lub słyszy gracza
        {
            FollowPlayer();
        }
        if (((distance > lookRadius) || (!playerInFieldOfView)) && (!heardPlayer) && (lastKnownPlayerPosition != Vector3.zero))                 // Bot nie widzi i nie słyszy gracza
        {
            LostPlayer();
        }

        if (alert && (distance <= lookRadius))
        {
            FollowPlayer();
        }
        if ((agent.velocity == Vector3.zero) && (lastPlayerPositionDistance < 1f))   // Bot się nie rusza
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
        if (isRanged)
        {
            if (distance >= rangedAttackDistance)
            {
                FaceTarget();
                agent.SetDestination(playerPosition);
                Action = "FollowPlayer";
                Debug.Log("FollowPlayer");
                lastKnownPlayerPosition = playerPosition;
            }
            else
            {
                AttackPlayer();
            }
        }
        else
        {
            FaceTarget();
            agent.SetDestination(playerPosition);
            Debug.Log("FollowPlayer");
            Action = "FollowPlayer";
            lastKnownPlayerPosition = playerPosition;
        }
    }

    void LostPlayer()
    {
        agent.SetDestination(lastKnownPlayerPosition);
        Debug.Log("LostPlayer");
        Action = "LostPlayer";
    }

    void AttackPlayer()
    {
        Debug.Log("AttackPlayer");
        Action = "AttackPlayer";
        if (isRanged)
        {
            agent.stoppingDistance = rangedAttackDistance;
            if (rangedAttackTime <= Time.time)
            {
                player.gameObject.GetComponent<health>().TakeDamage(rangedAttackPower);
                rangedAttackTime = Time.time + rangedAttackCooldown;
            }
        }
        else
        {
           if (meeleAttackTime <= Time.time)
           {
                agent.stoppingDistance = 1f;
                player.gameObject.GetComponent<health>().TakeDamage(meeleAttackPower);
           }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player") && (angleToPlayer > lookAngle * 0.5))
        {
            playerInFieldOfView = true;
            alert = true;
        }
        if (other.gameObject.tag == "Playernoize")
        {
            heardPlayer = true;
            playerInFieldOfView = true;
            FollowPlayer();
        }
        if ((other.gameObject.tag == "Bullet") || (alert))
        {
            if (other.gameObject.tag == "NPC") 
            {
                other.gameObject.GetComponent<EnemyController>().FollowPlayer();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Playernoize")
        {
            heardPlayer = false;
        }
        if (other.gameObject.tag == "Player")
        {
            playerInFieldOfView = false;
        }
    }
}
