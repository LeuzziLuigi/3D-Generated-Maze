using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public UIManager ui;
    public AudioManager aud;

    void Update()
    {
        transform.Rotate(0.0f, 3.5f, 0.0f, Space.Self);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            aud.gemVFX.Play();
            ui.IncreaseGemCount(1);
            Destroy(this.gameObject);
        }
    }
}
