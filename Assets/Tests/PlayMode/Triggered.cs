using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggered : MonoBehaviour
{
    [SerializeField] private bool triggered = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
    }

    public bool IsTriggered()
    {
        return triggered;
    }
}
