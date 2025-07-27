using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class WanderingEnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject followTarget;
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    Animator animator;
    [SerializeField]
    float wanderDistance = 6f;
    [SerializeField]
    float wanderFrequency = 6f;

    float wanderTimer = 0f;

    void Update()
    {

        animator.SetFloat("Speed", agent.velocity.magnitude);
        if(followTarget != null)
        {
            agent.SetDestination(followTarget.transform.position);
            return;
        }
        wanderTimer += Time.deltaTime;
     
        if(wanderTimer > wanderFrequency)
        {
            WalktoRandomPoint();
            wanderTimer = 0f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            followTarget = other.transform.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            followTarget = null;
        }
    }
    void WalktoRandomPoint()
    {
        Vector3 targetPosition = transform.position;
        targetPosition += Random.insideUnitSphere * wanderDistance;

        NavMesh.SamplePosition(targetPosition, out NavMeshHit hit, wanderDistance, ~0);

        targetPosition = hit.position;
        agent.SetDestination(targetPosition);

    }

}
