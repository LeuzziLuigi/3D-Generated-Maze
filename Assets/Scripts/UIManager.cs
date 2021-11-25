using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayStandardMode()
    {
        SceneManager.LoadScene(sceneName: "Maze Level");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(sceneName: "Menu");
    }

    public void PublishHiScore()
    {
        //SceneManager.LoadScene(sceneName: "Hi Score Publishing");
    }
}
