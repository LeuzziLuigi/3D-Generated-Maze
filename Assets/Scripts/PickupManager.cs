using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField]
    private LevelManager levelManager;

    public GameObject thisObject;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player" && this.tag == "Key")
        {
            levelManager.keyFound = true;
            levelManager.keyIcon.gameObject.SetActive(true);
            Destroy(thisObject);
        }
        if (collision.tag == "Player" && this.tag == "Gem")
        {
            levelManager.gemCount++;
            levelManager.pickupText.text = "Gems: " + levelManager.gemCount;
            Destroy(thisObject);
        }
    }
}

