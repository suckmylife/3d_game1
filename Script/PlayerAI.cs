using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
{
    public float attackDist = 3.0f;
    public float attackDelay = 0.5f;

    private NavMeshAgent agent;
    private Animator animator;
    private GameObject selectObj;

    private int landLayer;
    private int stairLayer;
    private int layerMask;

    private GameObject target;
    private bool isAttack = false;
 
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        landLayer = LayerMask.GetMask("Land");
        stairLayer = LayerMask.GetMask("Stair");
        layerMask = landLayer | stairLayer;     
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, float.MaxValue))
            {
                if (hit.collider.tag == "Land")
                {
                    agent.destination = hit.point;
                    agent.isStopped = false;
                    animator.SetBool("IsMove", true);
                }

                else if (hit.collider.tag == "Monster")
                {
                    target = hit.collider.gameObject;
                    agent.destination = target.transform.position;
                    StartCoroutine(TraceTarget());
                }
            }
        }
        if(Input.GetMouseButtonUp(1))
        {
            animator.SetBool("IsMove", false);
        }
        
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    IEnumerator TraceTarget()
    {
        //while(dist < attackDist)
        while (target != null)
        {
            float dist = Vector3.Distance(transform.position, target.transform.position);
            //float dist = agent.remainingDistance;

            if (dist < attackDist)
            {
                agent.isStopped = true;
                Attack();
            }
            else
            {
                agent.isStopped = false;
                //agent.destination = target.transform.position;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Attack()
    {
        if (!isAttack)
        {
            animator.SetTrigger("Attack");
            StartCoroutine(AttackCheck());
        }
    }

    IEnumerator AttackCheck()
    {
        isAttack = true;

        yield return new WaitForSeconds(attackDelay);

        isAttack = false;
    }
}
