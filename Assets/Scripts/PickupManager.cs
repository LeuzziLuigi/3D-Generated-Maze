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
            levelManager.keyCount++;
            levelManager.keyText.text = levelManager.keyText.text + "X";
            levelManager.keyText.color = Color.green;
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

