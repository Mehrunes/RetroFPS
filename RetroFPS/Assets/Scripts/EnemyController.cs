using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 15;
    public float lookAngle = 120;
    public float meeleAttackPower = 7f;
    public float rangedAttackDistance = 10f;
    public float meeleAttackDistance = 2;
    public string Action = "Idle";
    public float distance;
    public float lastPlayerPositionDistance;
    public float playerSpawnPosition;
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
    public GameObject player;
    public float sideToPlayer;
    public string side;

    private float rangedAttackCooldown = 0.33f;
    private float meeleAttackCooldown = 0.33f;
    private Transform target;
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
        lastKnownPlayerPosition = Vector3.zero;
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
            alert = false;
        }
        if ((isRanged) && (Action == "AttackPlayer") && (distance > rangedAttackDistance))
        {
            if ((agent.velocity == Vector3.zero) && (playerInFieldOfView))
            {
                Action = "Walk";
                FollowPlayer();
            }
            else
            {
                Action = "Idle";
                alert = false;
            }
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
        agent.isStopped = false;
        if (!alert)
        {
            AlertNerbyEnemies();
            alert = true;
        }
        else
        {
            lastKnownPlayerPosition = playerPosition;
            lastPlayerPositionDistance = Vector3.Distance(lastKnownPlayerPosition, transform.position);
            if (isRanged)
            {

                if ((distance >= rangedAttackDistance) && (playerInFieldOfView))
                {
                    FaceTarget();
                    agent.isStopped = false;
                    agent.SetDestination(playerPosition);
                    Action = "Walk";
                    lastKnownPlayerPosition = playerPosition;
                    lastPlayerPositionDistance = Vector3.Distance(lastKnownPlayerPosition, transform.position);
                }
                else if (!playerInFieldOfView)
                {
                    agent.isStopped = false;
                }
                else if (playerInFieldOfView && (distance < rangedAttackDistance))
                {
                    agent.isStopped = true;
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
                Action = "Walk";
                lastKnownPlayerPosition = playerPosition;
                lastPlayerPositionDistance = Vector3.Distance(lastKnownPlayerPosition, transform.position);
            }
        }
    }

    void AlertNerbyEnemies()
    {
        var hitColliders = Physics.OverlapSphere(transform.position, lookRadius);
        foreach (var collider in hitColliders)
        {
            if (collider != null && collider.gameObject.tag == "NPC")
            {
                collider.gameObject.GetComponent<EnemyController>().alert = true;
                collider.gameObject.GetComponent<EnemyController>().FollowPlayer();
            }
        }
    }

    void LostPlayer()
    {
        agent.SetDestination(lastKnownPlayerPosition);
    }

    void AttackPlayer()
    {
        agent.isStopped = true;
        if (player.GetComponent<health>().hitpoint == 0)
        {
            agent.isStopped = true;
            Action = "Idle";
        }
        else if ((isRanged) && (Time.time > rangedAttackTime + rangedAttackCooldown))
        {
            Action = "AttackPlayer";
            var rocket = Instantiate(currentWeapon, transform.position, lookRotation);
            rocket.AddForce(direction * shootforce);
            rangedAttackTime = Time.time;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (player.GetComponent<health>().hitpoint == 0)
        {
            agent.isStopped = true;
            Action = "Idle";
        }
        else if ((other.gameObject.tag == "Player") && (angleToPlayer < lookAngle * 0.5))
        {
            playerInFieldOfView = true;
        }
        if ((other.gameObject.tag == "Player") && (distance < meeleAttackDistance) && !isRanged)
        {
            if (Time.time > meeleAttackTime + meeleAttackCooldown)
            {
                Action = "AttackPlayer";
                player.GetComponent<health>().TakeDamage(meeleAttackPower);
                meeleAttackTime = Time.time;
            }
        }
        if ((other.gameObject.tag == "Player") && (angleToPlayer > lookAngle * 0.5))
        {
            playerInFieldOfView = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player") && (angleToPlayer < lookAngle * 0.5))
        {
            playerInFieldOfView = true;
        }

        if (other.gameObject.tag == "Playernoize")
        {
            heardPlayer = true;
            playerInFieldOfView = true;
            FollowPlayer();
        }
        if (other.gameObject.tag == "Bullet")
        {
            var hitColliders = Physics.OverlapSphere(transform.position, lookRadius);
            foreach (var collider in hitColliders)
            {
                if (collider != null && collider.gameObject.tag == "NPC")
                {
                    collider.gameObject.GetComponent<EnemyController>().alert = true;
                    collider.gameObject.GetComponent<EnemyController>().FollowPlayer();
                }
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