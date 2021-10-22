using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;

public class LevelIsCompletable
{
    const int MAX_TIME = 10;

    //Sets up for test.
    [SetUp]
    public void Setup()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
            playerObject.AddComponent<NavMeshAgent>();
    }

    // A Test behaves as an ordinary method
    [Test]
    public void LevelIsCompletableSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator LevelIsCompletableWithEnumeratorPasses()
    {
        bool levelIsCompleteable = true;

        bool levelComplete = false;
        bool timeUp = false;

        // Use the Assert class to test conditions.
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

        Assert.IsTrue(levelIsCompleteable);

        // Use yield to skip a frame.
        yield return null;
    }

    private bool LevelIsCompleted()
    {
        bool isComplete = true;
        //Code to check for level completion

        return isComplete;
    }
}
