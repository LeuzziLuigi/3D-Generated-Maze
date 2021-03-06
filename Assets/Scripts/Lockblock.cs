using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockblock : MonoBehaviour
{
    public MazeGenData mazeGenData;
    public AudioManager aud;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player" && mazeGenData.keyCollected > 0)
        {
            aud.lockblockVFX.Play();
            mazeGenData.keyCollected -= 1;
            Destroy(this.gameObject);
        }
    }
}
