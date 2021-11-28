using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemMotion : MonoBehaviour
{
    public GameObject gemObject;

    // Update is called once per frame
    void Update()
    {
        gemObject.transform.Rotate(0.0f, 3.5f, 0.0f, Space.Self);
    }
}
