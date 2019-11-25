using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    // raio de visao do inimigo
    public float lookRadius = 10f;
    
    public AIPath aipath;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;

    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody rb;
    public Animator animator;

    public Transform target;

    NavMeshAgent agent;


    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        


        Idle();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    
    void FixedUpdate()
    {
        Idle();

        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (distanceToTarget <= lookRadius)
        {
            //Run();
            FaceTarget();
            if (path == null)
                return;

            if (distanceToTarget <= agent.stoppingDistance)
            {

                //attack the target
                //face the target
                
                FaceTarget();
                Attack();

            }

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector3 direction = ((Vector3)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector3 force = direction * speed * Time.deltaTime;

            rb.AddForce(force);
            float distance = Vector3.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }
        /*if (distanceToTarget <= lookRadius)
        {

            Debug.Log("Distance to target " + distanceToTarget + " stopping " + agent.stoppingDistance);

            if (distanceToTarget <= agent.stoppingDistance)
            {
                
                //attack the target
                //face the target
                FaceTarget();
                Attack();

            }
            else
            {
                if (path == null)
                    return;
                Run();
            }
        }
        */else
        {
            Idle();
        }
        
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0 ,direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void Run()
    {
        animator.SetBool("Running", true);
        animator.SetBool("Idle", false);
        animator.SetBool("Attacking", false);
    }

    public void Attack()
    {
        animator.SetBool("Running", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Attacking", true);
    }

    public void Idle()
    {
        animator.SetBool("Running", false);
        animator.SetBool("Idle", true);
        animator.SetBool("Attacking", false);
        animator.SetBool("Hit", false);
    }

    public void getHit()
    {
        animator.SetBool("Hit", true);
        Invoke("Idle", 0.3f);
    }


}
