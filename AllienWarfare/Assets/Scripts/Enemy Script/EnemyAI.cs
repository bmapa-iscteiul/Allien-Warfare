using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWayPointDistance = 3f;
    public float attackingDistance = 2f;

    public Animator animator;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody rb;
    private float lookRadius = 10f;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody>();

        InvokeRepeating("UpdatePath", 0f, .5f);
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
    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else {
            reachedEndOfPath = false;
        
        }


        
        Vector3 direction = ((Vector3)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector3 force = direction * speed * Time.deltaTime;


        if (distanceToTarget <= lookRadius && distanceToTarget >= attackingDistance)
        {
            FaceTarget(); 
            rb.AddForce(force);
            Run();

        }
        if (distanceToTarget < attackingDistance)
        {
            Attack();
        }

        float distance = Vector3.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWayPointDistance)
        {
            currentWaypoint++;
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

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
        speed = 200f;
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
        Debug.Log("Got hit");
        animator.SetBool("Hit", true);
        Invoke("Idle", 0.3f);
        lookRadius = 200f;
    }

  
}
