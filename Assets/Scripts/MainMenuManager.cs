using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private MazeGenData mazeGenData;

    public GameObject menuPanel;
    public GameObject loginPanel;

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

    public void LoginButton()
    {
        menuPanel.SetActive(false);
        loginPanel.SetActive(true);
    }

    public void LoginCancel()
    {
        menuPanel.SetActive(true);
        loginPanel.SetActive(false);
    }

    public void LoginSubmit()
    {
        //Yolo
    }
}
