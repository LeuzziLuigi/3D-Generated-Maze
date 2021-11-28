using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDetector : MonoBehaviour
{
    public UIManager ui;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            ui.EndOfMaze();
        }
    }
}
