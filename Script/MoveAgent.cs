using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveAgent : MonoBehaviour
{
    private List<Transform> wayPoints = new List<Transform>();
    public int nextIndex;

    private NavMeshAgent agent;

    private readonly float patrolSpeed = 1.5f;
    private readonly float traceSpeed = 4.5f;

    private float damping = 1.0f;

    private bool _patrolling;
    public bool patrolling
    {
        get
        {
            return _patrolling;
        }
        set
        {
            _patrolling = value;
            if (_patrolling)
            {
                agent.speed = patrolSpeed;
                damping = 1.0f;
                MoveWayPoint();
            }
        }
    }

    private Vector3 _traceTarget;
    public Vector3 traceTarget
    {
        get { return _traceTarget; }
        set
        {
            _traceTarget = value;
            agent.speed = traceSpeed;
            damping = 7.0f;
            TraceTarget(_traceTarget);
        }
    }

    public float speed
    {
        get { return agent.velocity.magnitude; }
    }


    private void Awake()
    {
        GameObject group = GameObject.Find("WayPoint");

        if (group != null)
        {
            group.GetComponentsInChildren<Transform>(wayPoints);
            wayPoints.RemoveAt(0);
        }

        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
       // agent.speed = patrolSpeed;
       // agent.updateRotation = false;

       // nextIndex = Random.Range(0, wayPoints.Count);

        MoveWayPoint();
    }

    private void MoveWayPoint()
    {
        if (agent.isPathStale)
            return;

        agent.destination = wayPoints[nextIndex].position;
        //agent.SetDestination(wayPoints[nextIndex].position);
        //agent.isStopped = false;
    }

    private void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale)
            return;

        agent.destination = pos;
        agent.isStopped = false;
    }

    public void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        _patrolling = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (agent.isStopped == false)
        //{
        //    Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity);
        //    //Quaternion rot = Quaternion.LookRotation(agent.destination);

        //    transform.rotation = Quaternion.Slerp(transform.rotation,
        //        rot, Time.deltaTime * damping);
        //}

        //if (!_patrolling)
        //    return;

        if (agent.velocity.sqrMagnitude <= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
        {
           // nextIndex = ++nextIndex % wayPoints.Count;
           nextIndex = Random.Range(0, wayPoints.Count);
            MoveWayPoint();
        }
    }
}
