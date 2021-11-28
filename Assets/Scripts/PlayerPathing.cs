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
        [SerializeField] private GameObject targetObject;

        public bool collectableMode;


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
            if (targetObject == null && !collectableMode)
            {
                targetObject = GameObject.FindGameObjectWithTag("Key");
                if (targetObject == null)
                {
                    targetObject = GameObject.FindGameObjectWithTag("Finish Node");
                }
                if (targetObject != null)
                {
                    SetTarget(targetObject.transform.position);
                }
            } else if (collectableMode && (targetObject == null || targetObject.tag != "Gem"))
            {
                targetObject = GameObject.FindGameObjectWithTag("Gem");
                if (targetObject != null)
                {
                    SetTarget(targetObject.transform.position);
                }
            }
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
