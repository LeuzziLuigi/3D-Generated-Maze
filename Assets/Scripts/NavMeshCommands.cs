using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

namespace TestingScripts
{
    public class NavMeshCommands : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface surface;
        public void RebuildMesh()
        {
            surface.BuildNavMesh();
        }
    }
}

