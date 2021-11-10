using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader
{
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public IEnumerator WaitForLevelLoad(string levelName)
    {
        // Start loading the scene
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
        // Wait until the level finish loading
        while (!asyncLoadLevel.isDone)
            yield return null;
    }
}