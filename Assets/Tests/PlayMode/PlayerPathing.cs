using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerPathing : MonoBehaviour
{
    private Vector3 MoveTarget;
    private NavMeshAgent agent;
    private bool moving = false;
    // Update is called once per frame
    void Update()
    {
        if (!moving)
        {
            if (MoveTarget != null && agent != null)
            {
                agent.SetDestination(MoveTarget);
            }
        }
    }

    public void SetTarget(Vector3 target)
    {
        MoveTarget = target;
    }

    public void SetAgent(NavMeshAgent sentAgent)
    {
        agent = sentAgent;
    }
}
