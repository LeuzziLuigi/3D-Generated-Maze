using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayFabManager playFabManager;

    [SerializeField]
    private MazeGenData mazeGenData;

    public GameObject endMazePanel;
    public GameObject gamePanel;
    public bool levelFinished = false;
    public Text endLevelTimerText;

    public Text timerText;
    public Text gemText;
    public Image keyIcon;
    private float totalSeconds;
    private float millisecondsCount;
    private float millisecondsDisplay;
    private float secondsCount;
    private int minuteCount;

    private string millisecondsText;
    private string secondsText;
    private string minutesText;
    void Start()
    {
        keyIcon.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (levelFinished == false)
        {
            UpdateTimerUI();
        }
    }

    public void PlayStandardMode()
    {
        SceneManager.LoadScene(sceneName: "Maze Level");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(sceneName: "Menu");
    }

    public void UpdateTimerUI()
    {
        //set timer UI
        totalSeconds += Time.deltaTime;
        millisecondsCount += Time.deltaTime;

        minutesText = minuteCount.ToString();
        secondsText = secondsCount.ToString();
        millisecondsText = millisecondsDisplay.ToString();

        if (minuteCount < 10) { minutesText = "0" + minutesText; }
        if (secondsCount < 10) { secondsText = "0" + secondsText; }

        timerText.text = minutesText + ":" + secondsText + ":";
        if (millisecondsDisplay < 10) { timerText.text = timerText.text + "0" + (int)millisecondsDisplay; }
        else { timerText.text = timerText.text + (int)millisecondsDisplay; }


        if (millisecondsCount >= 1)
        {
            millisecondsCount = 0;
            secondsCount++;
        }
        millisecondsDisplay = millisecondsCount * 100;
        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }
    }

    public void IncreaseGemCount(int value)
    {
        mazeGenData.totalGems += value;
        gemText.text = mazeGenData.totalGems.ToString();
    }

    public void KeyFound()
    {
        mazeGenData.keyCollected += 1;
        keyIcon.gameObject.SetActive(true);
    }

    public void EndOfMaze()
    {
        mazeGenData.levelFinished = true;
        endMazePanel.SetActive(true);
        playFabManager.GetLeaderboard();
        gamePanel.SetActive(false);
        endLevelTimerText.text = timerText.text;

        mazeGenData.timeScore = totalSeconds;

        SendScore();
    }

    private void SendScore()
    {
        string score = timerText.text;
        score = score.Remove(5, 1);
        score = score.Remove(2, 1);

        while (score[0] == '0')
        {
            score = score.Remove(0, 1);
        }

        playFabManager.SendLeaderbord(int.Parse(score));
    }
}
