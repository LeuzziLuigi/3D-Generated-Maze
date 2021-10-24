using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;

public class LevelIsCompletable
{
    const int MAX_TIME = 30;
    const string TEST_LEVEL = "Maze Gen";
    const string START = "Start Node";
    const string EXIT = "Finish Node";

    GameObject start;
    GameObject exit;

    //Sets up for test.
    [SetUp]
    public void SetUp()
    {
        LevelLoader loadLevel = new LevelLoader();
        loadLevel.LoadLevel(TEST_LEVEL);

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    [TestCase(1, ExpectedResult = null)]
    public IEnumerator LevelNotCompletedWhenNotAtFinish(int placeholder)
    {
        bool levelComplete = false;
        bool timeUp = false;

        int timeRan = 0;
        while (!levelComplete && !timeUp)
        {
            timeRan++;
            if (timeRan >= 1) //Check if out of time
            {
                timeUp = true;
            }

            levelComplete = LevelIsCompleted(); //Check if level is completed
            yield return new WaitForSeconds(1f);

            // Use yield to skip a frame.
            yield return null;

            // Use the Assert class to test conditions.
            Assert.IsTrue(!levelComplete);
        }
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    [TestCase(1, ExpectedResult = null)]
    public IEnumerator LevelIsCompletableTest(int placeholder)
    {
        bool levelIsCompleteable = true;

        bool levelComplete = false;
        bool timeUp = false;

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

        // Use yield to skip a frame.
        yield return null;

        // Use the Assert class to test conditions.
        Assert.IsTrue(levelIsCompleteable);

    }

    private bool LevelIsCompleted()
    {
        bool isComplete = false;
        if (exit == null)
        {
            if (GameObject.FindGameObjectWithTag(EXIT) != null)
            {
                exit = GameObject.FindGameObjectWithTag(EXIT);
            }
        }
        //Code to check for level completion
        if (exit != null)
        {
            isComplete = exit.GetComponent<Triggered>().IsTriggered();
        }


        return isComplete;
    }
}
