using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private MazeGenData mazeGenData;

    public void NewSeedFromUI()
    {
        mazeGenData.getNewSeed();
    }

    public void PlayStandardMode()
    {
        SceneManager.LoadScene(sceneName: "Maze Level");
    }

    public void Exit()
    {
        Application.Quit();
    }

}
