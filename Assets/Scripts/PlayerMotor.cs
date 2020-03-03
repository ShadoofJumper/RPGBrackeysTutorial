using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform targetFollow;

    // Start is called before the first frame update
    void Start()
    {
        // get component navmesh agent
        agent = GetComponent<NavMeshAgent>();
    }

    // method for move agend topoint, use navmesh mechanics
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    private void FixedUpdate()
    {
        // if have target then look on target
        if (targetFollow != null)
        {
            FaceTarget();
        }
    }

    private void FaceTarget()
    {
        // calculate angel for looking on target
        // get vector to taget
        Vector3 direction = (targetFollow.position - transform.position).normalized;
        // get angle to taget
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // 0 on y so player not look up and down
        // look on taget
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); 
    }

    public void FollowTarget(Interactable target)
    {
        // add target
        targetFollow = target.interacteTransform;
        // start move to target
        StartCoroutine("MoveToTarget");
        // change afent stop move parametr
        agent.stoppingDistance = target.rangeAcces * 0.8f;

        // for rotate to target even if we stay near target
        // disable auto rotate to target
        agent.updateRotation = false;
    }

    public void UnfollowTarget()
    {
        targetFollow = null;
        agent.stoppingDistance = 0;
        agent.updateRotation = true;
    }

    IEnumerator MoveToTarget()
    {
        //if have target then every 0.25 second move to target
        while (targetFollow)
        {
            agent.SetDestination(targetFollow.position);
            yield return new WaitForSeconds(0.25f);
        }
        yield return null;
    }
}
