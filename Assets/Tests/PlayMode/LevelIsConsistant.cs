using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LevelIsConsistant
{
    const int NUMBER_OF_COORDINATES = 3;
    const int TIMES_TO_REPEAT_TEST = 100;
    const string TEST_LEVEL = "TestScene";
    const string START = "Start Node";
    const string EXIT = "Finish Node";
    const string REFERENCE = "Reference Node";
    private LevelLoader loadLevel = new LevelLoader();

    MazeGenData mazeData;

    [SetUp]
    public void SetUp()
    {
        LoadLevelNoReset();
        //MazeGenData[] mazeDataSearch = Resources.FindObjectsOfTypeAll<MazeGenData>();
        mazeData = Resources.Load<MazeGenData>("ScriptableObjects/MazeData");

        mazeData.resetSeed();
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    [TestCase(1, ExpectedResult = null)]
    [TestCase(333333, ExpectedResult = null)]
    [TestCase(999999, ExpectedResult = null)]
    public IEnumerator LevelIsConsistantSingleLevel(int seed)
    {
        mazeData.Seed = seed;
        mazeData.resetSeed();
        LoadLevelNoReset();
        yield return new WaitForSeconds(1f);
        bool levelsAreConsistant = true;

        List<Vector3> referenceFromMapOne = GetReferencePositions();

        while (referenceFromMapOne.Count < 1)
        {
            referenceFromMapOne = GetReferencePositions();
            yield return new WaitForSeconds(1f);
        }
        LoadLevelWithReset();

        List<Vector3> referenceFromMapTwo = GetReferencePositions();
        while (referenceFromMapTwo.Count < 1)
        {
            referenceFromMapTwo = GetReferencePositions();
            yield return new WaitForSeconds(1f);
        }

        levelsAreConsistant = LevelConsistencyTest(referenceFromMapOne, referenceFromMapTwo);
        yield return new WaitForSeconds(1f);

        // Use the Assert class to test conditions.
        Assert.IsTrue(levelsAreConsistant);
        
        // Use yield to skip a frame.
        yield return null;
    }

    [UnityTest]
    [TestCase(1, ExpectedResult = null)]
    [TestCase(333333, ExpectedResult = null)]
    [TestCase(999999, ExpectedResult = null)]
    public IEnumerator LevelTwoIsDifferent(int seed)
    {
        mazeData.Seed = seed;
        LoadLevelNoReset();
        yield return new WaitForSeconds(1f);
        bool levelsAreConsistant = true;

        List<Vector3> referenceFromMapOne = GetReferencePositions();

        while (referenceFromMapOne.Count < 1)
        {
            referenceFromMapOne = GetReferencePositions();
            yield return new WaitForSeconds(1f);
        }
        LoadLevelNoReset();
        yield return new WaitForSeconds(2f);

        List<Vector3> referenceFromMapTwo = GetReferencePositions();
        while (referenceFromMapTwo.Count < 1)
        {
            referenceFromMapTwo = GetReferencePositions();
            yield return new WaitForSeconds(1f);
        }

        levelsAreConsistant = LevelConsistencyTest(referenceFromMapOne, referenceFromMapTwo);
        yield return new WaitForSeconds(1f);

        // Use the Assert class to test conditions.
        Assert.IsTrue(!levelsAreConsistant);

        // Use yield to skip a frame.
        yield return null;
    }

    private bool LevelConsistencyTest(List<Vector3> referenceFromMapOne, List<Vector3> referenceFromMapTwo)
    {
        bool allReferencesMatch = true;

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
        {
            Debug.Log("Start Reference" + startReference);
            referencePointList.Add(startReference);
        }
        if (randomPointReference != null)
        {
            Debug.Log("Random Reference" + randomPointReference);
            referencePointList.Add(randomPointReference);
        }
        if (exitReference != null)
        {
            Debug.Log("Exit Reference" + exitReference);
            referencePointList.Add(exitReference);
        }

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

    private void LoadLevelWithReset()
    {
        //Reset Seed randomizer
        mazeData.resetSeed();

        //Reset Level
        loadLevel.LoadLevel(TEST_LEVEL);
    }

    private void LoadLevelNoReset()
    {
        //Reset Level
        loadLevel.LoadLevel(TEST_LEVEL);
    }

}
