using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockblockManager : MonoBehaviour
{
    [SerializeField]
    private LevelManager levelManager;

    public GameObject thisObject;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player" && levelManager.keyCount > 0) //
        {
            levelManager.keyCount--;
            Destroy(thisObject);
        }
    }
}
