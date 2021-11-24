using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NumDoorsAndKeysMatch
{
    const int MAX_TIME = 30;
    const string TEST_LEVEL = "Maze Gen";
    const string KEY = "Key";
    const string DOOR = "Door";

    MazeGenData mazeData;
    LevelLoader loadLevel;

    //Sets up for test.
    [SetUp]
    public void SetUp()
    {
        loadLevel = new LevelLoader();
        MazeGenData[] mazeDataSearch = Resources.FindObjectsOfTypeAll<MazeGenData>();
        mazeData = mazeDataSearch[0];
        mazeData.resetSeed();
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    [TestCase(1, ExpectedResult = null)]
    [TestCase(333333, ExpectedResult = null)]
    [TestCase(999999, ExpectedResult = null)]
    public IEnumerator NumOfKeysAndDoorsMatch(int seed)
    {
        //Set seed
        mazeData.Seed = seed;

        // Start loading the scene and wait for completion
        loadLevel.LoadLevel(TEST_LEVEL);
        yield return new WaitForSeconds(1f);

        GameObject[] Keys = getObjectList(KEY);
        Debug.Log("# of Keys: " + Keys.Length);

        GameObject[] Doors = getObjectList(DOOR);
        Debug.Log("# of Doors: " + Doors.Length);

        //Get result bool/
        bool numOfKeysAndDoorsMatch = (Keys.Length == Doors.Length);

        Assert.IsTrue(numOfKeysAndDoorsMatch);
    }

    private GameObject[] getObjectList(string tag)
    {
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag(tag);
        return gameObjectArray;
    }
}