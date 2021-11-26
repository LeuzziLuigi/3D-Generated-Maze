using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TestingScripts
{
    public class PlayerPathing : MonoBehaviour
    {
        private Vector3 MoveTarget;
        private NavMeshAgent agent;
        private bool targetFound = false;

        private void Start()
        {
            GameObject surface = GameObject.Find("NavMesh");
            Debug.Log(surface.name + " was found.");
            surface.GetComponent<NavMeshCommands>().RebuildMesh();
            agent = this.gameObject.AddComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Debug.Log(MoveTarget);
            if (!targetFound)
            {
                var targetObject = GameObject.FindGameObjectWithTag("Finish Node");
                if (targetObject != null)
                {
                    SetTarget(targetObject.transform.position);
                    targetFound = true;
                }
            } else
            {
                agent.SetDestination(MoveTarget);
            }
        }

        public void SetTarget(Vector3 target)
        {
            MoveTarget = target;
            Debug.Log(target);
        }
    }
}
