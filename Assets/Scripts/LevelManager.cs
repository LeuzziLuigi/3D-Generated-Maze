using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public PlayFabManager playFabManager;

    [SerializeField]
    private MazeGenData mazeGenData;

    public bool keyFound;
    public int gemCount;

    public GameObject endMazePanel;
    public bool levelFinished = false;
    public Text endLevelTimerText;

    public Text timerText;
    public Text pickupText;
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
        keyFound = false;
        keyIcon.gameObject.SetActive(false);
        gemCount = 0;
        pickupText.text = gemCount.ToString();
    }
    void Update()
    {
        if(levelFinished == false)
        {
            UpdateTimerUI();
        }

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
        millisecondsDisplay = millisecondsCount*100;
        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            EndOfMaze();
        }
    }

    public void EndOfMaze()
    {
        levelFinished = true;
        endMazePanel.SetActive(true);
        endLevelTimerText.text = timerText.text;

        mazeGenData.timeScore = totalSeconds;
        //mazeGenData.pickupScore = gemCount;
        //mazeGenData.totalGems = mazeGenData.totalGems + gemCount;
        mazeGenData.totalGems = 0;

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
