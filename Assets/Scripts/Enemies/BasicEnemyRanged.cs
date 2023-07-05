using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyRanged : MonoBehaviour
{
    //Basic Variables
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundLayer, playerLayer;

    //Patrolling Variables
    public Vector3 walkPoint;
    public bool guarding;
    public bool walkPointSet, approachingEnd;
    public float walkPointRange;
    public Transform guardPointStart, guardPointEnd;


    //Attacking Variables
    public float attackCooldown;
    bool attacked;
    public GameObject enemyBullet, newBullet;
    public Transform gunHole;
    public int bulletSpread, maxBulletNum = 50;

    //Range variables
    public float sightRange, attackRange;
    public bool playerInSight, playerInAttackRange;

    //Death variables
    public GameManager manager;
    public int deathValue;


    private void Awake()
    {
        //Grab a few components so they don't need to be added individually
        player = GameObject.Find("Player").transform;
        manager = GameObject.Find("Manager").GetComponent<GameManager>();
        //agent = GetComponentInParent<NavMeshAgent>();

        if(guarding)
        {
            walkPointSet = true;
            walkPoint = new Vector3(guardPointEnd.position.x, transform.position.y, guardPointEnd.position.z);
            approachingEnd = true;

            guardPointStart.parent = null;
            guardPointEnd.parent = null;

        }


    }

    private void Update()
    {
        //Can the enemy see or reach the player
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        //Depending on distance to player, pick general action
        if(!playerInSight && !playerInAttackRange) { Patrolling(); }
        if(playerInSight && !playerInAttackRange) { Chasing(); }
        if(playerInAttackRange) { Attacking(); }

    }

    //Move around an area
    private void Patrolling()
    {
        if (!walkPointSet) { SearchWalkPoint(); }
        else { agent.SetDestination(walkPoint); }

        //How far to reach the destination
        Vector3 distanceToWalk = transform.position - walkPoint;

        //If the enemy has arrived, pick a new destination. Could be toggled for a stable back and forth route.
        if (distanceToWalk.magnitude < 1f)
        {
            //If not on a stable route, find a new end point
            if (!guarding) { walkPointSet = false; }
            //Otherwise, set destination to other guard point
            else
            {
                if(approachingEnd) //Reached guardPointEnd
                {
                    walkPoint = new Vector3(guardPointStart.position.x, transform.position.y, guardPointStart.position.z);
                    approachingEnd = false;
                }
                else //Reached guardPointStart
                {
                    walkPoint = new Vector3(guardPointEnd.position.x, transform.position.y, guardPointEnd.position.z);
                    approachingEnd = true;

                }
            }
        }
    }
    //Find a place to move to
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
        {
            walkPointSet = true;
        }
    }

    //Try to get player into attack range
    private void Chasing()
    {
        //Can see the player, try to move to their postion. Movement will be stopped once they enter attack range
        agent.SetDestination(player.position);
    }

    //Attack player
    private void Attacking()
    {
        //Stop moving once in range
        agent.SetDestination(transform.position);
        //Face the player
        transform.LookAt(player);

        if(!attacked)
        {
            //Make sure the gun is also pointed at the player
            gunHole.LookAt(player);


            //Limit how many bullets are created
            if(GameObject.FindGameObjectsWithTag("Enemy Bullet").Length <= maxBulletNum)
            {
                //Create the bullet
                newBullet = Instantiate(enemyBullet, gunHole.position, transform.rotation * Quaternion.Euler(0f, 0f, 0f));

                //Maybe make the bullet actually be aimed at the player
                newBullet.transform.LookAt(player);

                //Generate and apply a random bullet spread
                Vector3 spread = BulletMethods.randomSpread(bulletSpread);
                newBullet.transform.Rotate(spread.x, spread.y, spread.z);

                //Mark a bullet has been fired
                attacked = true;
                //Prepare to attack again once the cooldown has passed
                Invoke(nameof(ResetAttack), attackCooldown);
            }

        }

    }
    //Take the attack off cooldown
    private void ResetAttack()
    {
        attacked = false;
    }


    //DEBUG: Vizualization of sight and attack ranges for testing
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }


    /*
    //Destroy the enemy when hit with a bullet
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            manager.updateWinCount(deathValue);
            Destroy(transform.parent.gameObject);
            Destroy(guardPointStart.gameObject);
            Destroy(guardPointEnd.gameObject);
        }
    }
    */

}
