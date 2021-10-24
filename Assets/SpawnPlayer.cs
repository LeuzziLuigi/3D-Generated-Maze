using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] GameObject PlayerToSpawn;
    // Start is called before the first frame update
    void Awake()
    {
        Instantiate(PlayerToSpawn, new Vector3(-7, 0, -7), transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
