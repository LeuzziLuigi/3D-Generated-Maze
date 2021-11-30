using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Audio wasn't working on a per-item basis because when the item was destroyed it destroyed the audio as well
public class AudioManager : MonoBehaviour
{
    public AudioSource gemVFX;
    public AudioSource keyVFX;
    public AudioSource lockblockVFX;
    public GameObject gemObject;
    public GameObject keyObject;
    public GameObject lockblockObject;


    // Start is called before the first frame update
    void Start()
    {
        gemVFX = gemObject.GetComponent<AudioSource>();
        keyVFX = keyObject.GetComponent<AudioSource>();
        lockblockVFX = lockblockObject.GetComponent<AudioSource>();
    }
}
