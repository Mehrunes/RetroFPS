using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float lookRadius = 15;
    public float lookAngle = 120;
    public float meeleAttackCooldown = 2;
    public float rangedAttackCooldown = 0.5f;
    public float rangedAttackPower = 10;
    public float meeleAttackPower = 10;
    public float rangedAttackDistance = 10;
    public float meeleAttackDistance = 2;
    public string Action = "Idle";
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
    public Rigidbody currentWeapon;
    public SphereCollider sight;
    public float sideToPlayer;
    public string side;

    private Transform target;
    public GameObject player;
    private NavMeshAgent agent;
    private float rangedAttackTime;
    private float meeleAttackTime;
    private Vector3 origin;
    private Quaternion lookRotation;
    private float shootforce = 250;

    // Use this for initialization
    void Start()
    {
        player = PlayerManager.instance.player;
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        sight = GetComponent<SphereCollider>();
        rangedAttackTime = Time.time;
        meeleAttackTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = target.position;
        distance = Vector3.Distance(target.position, transform.position);
        direction = target.position - transform.position;
        origin = new Vector3(transform.position.x, transform.position.y + 1.6f, transform.position.z);
        angleToPlayer = (Vector3.Angle(direction, transform.forward));
        sideToPlayer = (Vector3.Angle(direction, transform.right));
        if (sideToPlayer < 90) side = "right"; else side = "left";
        sight.radius = lookRadius;

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
        if ((agent.velocity == Vector3.zero) && (lastPlayerPositionDistance > (lookRadius - 1f)) && !playerInFieldOfView)  // Bot się nie rusza
        {
            Action = "Idle";
        }
    }

    void FaceTarget()
    {
        direction = (target.position - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 100f);
    }

    void FollowPlayer()
    {
        if (isRanged)
        {
            agent.stoppingDistance = rangedAttackDistance;
            if (distance >= rangedAttackDistance)
            {
                FaceTarget();
                agent.SetDestination(playerPosition);
                Action = "Walk";
                Debug.Log("Walk");
                lastKnownPlayerPosition = playerPosition;
                lastPlayerPositionDistance = Vector3.Distance(lastKnownPlayerPosition, transform.position);
            }
            else
            {
                AttackPlayer();
            }
        }
        else if (distance < meeleAttackDistance)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
            FaceTarget();
            agent.SetDestination(playerPosition);
            Debug.Log("Walk");
            Action = "Walk";
            lastKnownPlayerPosition = playerPosition;
            lastPlayerPositionDistance = Vector3.Distance(lastKnownPlayerPosition, transform.position);
        }
    }

    void LostPlayer()
    {
        agent.SetDestination(lastKnownPlayerPosition);
    }

    void AttackPlayer()
    {
        if ((isRanged) && (Time.time > rangedAttackTime + rangedAttackCooldown))
        {
            Debug.Log("AttackPlayer");
            Action = "AttackPlayer";
            var rocket = Instantiate(currentWeapon, transform.position, lookRotation);
            rocket.AddForce(direction * shootforce);
            rangedAttackTime = Time.time;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.tag == "Player") && (distance < meeleAttackDistance))
        {
            if (Time.time > meeleAttackTime + meeleAttackCooldown)
            {
                Debug.Log("AttackPlayer");
                Action = "AttackPlayer";
                player.GetComponent<health>().TakeDamage(meeleAttackPower);
                meeleAttackTime = Time.time;
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
