using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;

public class LevelIsCompletable
{
    const int MAX_TIME = 10;
    const string TEST_LEVEL = "Maze Gen";
    PlayerPathing pathing;
    GameObject exit;

    //Sets up for test.
    [SetUp]
    public void Setup()
    {
        LevelLoader loadLevel = new LevelLoader();
        loadLevel.LoadLevel(TEST_LEVEL);

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) {
            playerObject.AddComponent<NavMeshAgent>();
            playerObject.AddComponent<PlayerPathing>();

            PlayerPathing pathing = playerObject.GetComponent<PlayerPathing>();

            pathing.SetAgent(playerObject.GetComponent<NavMeshAgent>());
        }

        exit = GameObject.FindGameObjectWithTag("Exit");


    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator LevelIsCompletableWithEnumeratorPasses()
    {
        bool levelIsCompleteable = true;

        bool levelComplete = false;
        bool timeUp = false;

        pathing.SetTarget(exit.transform.position);

        int timeRan = 0;
        while (!levelComplete && !timeUp)
        {
            timeRan++;
            if (timeRan >= MAX_TIME) //Check if out of time
            {
                timeUp = true;
            }

            levelComplete = LevelIsCompleted(); //Check if level is completed
            yield return new WaitForSeconds(1f);
        }

        if (timeUp)
        {
            levelIsCompleteable = false;
        }

        // Use the Assert class to test conditions.
        Assert.IsTrue(levelIsCompleteable);

        // Use yield to skip a frame.
        yield return null;
    }

    private bool LevelIsCompleted()
    {
        bool isComplete = false;
        //Code to check for level completion

        return isComplete;
    }
}
