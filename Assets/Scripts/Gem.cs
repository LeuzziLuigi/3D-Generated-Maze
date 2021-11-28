using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public UIManager ui;
    void Update()
    {
        transform.Rotate(0.0f, 3.5f, 0.0f, Space.Self);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            ui.IncreaseGemCount(1);
            Destroy(this.gameObject);
        }
    }
}
