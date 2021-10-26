using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LevelIsConsistant
{
    const int NUMBER_OF_COORDINATES = 3;
    const int TIMES_TO_REPEAT_TEST = 100;
    const string TEST_LEVEL = "Maze Gen";
    const string START = "Start Node";
    const string EXIT = "Finish Node";
    const string REFERENCE = "Reference Node";
    private LevelLoader loadLevel = new LevelLoader();

    [SetUp]
    public void SetUp()
    {
        loadLevel.LoadLevel(TEST_LEVEL);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest, Retry(50)]
    public IEnumerator LevelIsConsistantPlayTest()
    {
        bool levelsAreConsistant = true;

        levelsAreConsistant = LevelConsistencyTest();

        // Use the Assert class to test conditions.
        Assert.IsTrue(levelsAreConsistant);
        
        // Use yield to skip a frame.
        yield return null;
    }

    private bool LevelConsistencyTest()
    {
        bool allReferencesMatch = true;
        List<Vector3> referenceFromMapOne = GetReferencePositions();

        ReloadLevel();

        List<Vector3> referenceFromMapTwo = GetReferencePositions();

        if (referenceFromMapOne.Count != NUMBER_OF_COORDINATES || referenceFromMapTwo.Count != NUMBER_OF_COORDINATES)
        {
            allReferencesMatch = false;
        }
        int x = 0;
        while (x < referenceFromMapOne.Count && allReferencesMatch)
        {
            if (!CompareReferencePoints(referenceFromMapOne[x], referenceFromMapTwo[x]))
            {
                allReferencesMatch = false;
            }
            x++;
        }

        return allReferencesMatch;
    }

    private List<Vector3> GetReferencePositions()
    {
        List<Vector3> referencePointList = new List<Vector3>();

        Vector3 startReference = GameObject.FindGameObjectWithTag(START).transform.position;
        Vector3 randomPointReference = GameObject.FindGameObjectWithTag(REFERENCE).transform.position;
        Vector3 exitReference = GameObject.FindGameObjectWithTag(EXIT).transform.position;

        if (startReference != null)
            referencePointList.Add(startReference);
        if (randomPointReference != null)
            referencePointList.Add(randomPointReference);
        if (exitReference != null)
            referencePointList.Add(exitReference);

        return referencePointList;
    }

    private bool CompareReferencePoints(Vector3 referenceOne, Vector3 referenceTwo)
    {
        bool referenceMatch = true;

        if (referenceOne.x != referenceTwo.x) //Check for x coordinate mismatch
        {
            referenceMatch = false;
        } else if (referenceOne.y != referenceTwo.y) //Check for y coordinate mismatch
        {
            referenceMatch = false;
        }
        else if (referenceOne.z != referenceTwo.z) //Check for z coordinate mismatch
        {
            referenceMatch = false;
        }

        return referenceMatch;
    }

    private void ReloadLevel()
    {
        //Reset Seed randomizer
        //Reload from same seed
        //Reset Level
        loadLevel.LoadLevel(TEST_LEVEL);
    }
}
