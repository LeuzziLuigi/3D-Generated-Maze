using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GemsFunction
{
    const int MAX_TIME = 30;
    const string TEST_LEVEL = "TestScene";

    MazeGenData mazeData;
    LevelLoader loadLevel;

    //Sets up for test.
    [SetUp]
    public void SetUp()
    {
        loadLevel = new LevelLoader();
        //MazeGenData[] mazeDataSearch = Resources.FindObjectsOfTypeAll<MazeGenData>();
        mazeData = Resources.Load<MazeGenData>("ScriptableObjects/MazeData");
        mazeData.resetSeed();
        mazeData.totalGems = 0;
    }

    [UnityTest]
    [TestCase(1, ExpectedResult = null)]
    public IEnumerator CollectablesCanBePickedUp(int seed)
    {
        //Set seed
        mazeData.Seed = seed;

        // Start loading the scene and wait for completion
        loadLevel.LoadLevel(TEST_LEVEL);
        yield return new WaitForSeconds(3f);

        //Set Player Pathing to collectable mode
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<TestingScripts.PlayerPathing>().collectableMode = true;

        bool collectablePickedUp = false;
        bool timeUp = false;

        int timeRan = 0;
        while (!collectablePickedUp && !timeUp)
        {
            timeRan++;
            if (timeRan >= MAX_TIME) //Check if out of time
            {
                timeUp = true;
            }

            collectablePickedUp = CollectablePickedUp(); //Check if level is completed
            yield return new WaitForSeconds(1f);

            // Use yield to skip a frame.
            yield return null;

            // Use the Assert class to test conditions.
        }
        Assert.IsTrue(collectablePickedUp);
    }

    private bool CollectablePickedUp()
    {
        bool collectablePickedUp = false;

        var collectableCount = mazeData.totalGems;

        if (collectableCount > 0)
        {
            collectablePickedUp = true;
        }

        return collectablePickedUp;
    }
}
