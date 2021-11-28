using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTestPlayer : MonoBehaviour
{
    [SerializeField] GameObject PlayerToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Point:" + this.transform.localPosition);
        Instantiate(PlayerToSpawn, this.transform.localPosition, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
