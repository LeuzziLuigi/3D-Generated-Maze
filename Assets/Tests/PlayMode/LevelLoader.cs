using System.Collections;
using UnityEngine.SceneManagement;

public class LevelLoader
{
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}