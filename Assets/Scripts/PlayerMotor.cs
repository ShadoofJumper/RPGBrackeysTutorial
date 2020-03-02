using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    private NavMeshAgent agent;

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
}
