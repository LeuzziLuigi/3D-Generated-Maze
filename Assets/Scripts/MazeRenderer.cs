using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestingScripts;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField]
    public MazeGenData mazeGenData;

    [SerializeField]
    private bool debugTestingMode;

    [SerializeField]
    private Transform wallPrefab = null;
    [SerializeField]
    private Transform floorPrefab = null;
    [SerializeField]
    private Transform startNodePrefab = null;
    [SerializeField]
    private Transform startPadPrefab = null;
    [SerializeField]
    private Transform referenceNodePrefab = null;
    [SerializeField]
    private Transform finishNodePrefab = null;
    [SerializeField]
    private Transform finishPadPrefab = null;

    [SerializeField]
    private GameObject playerCharacter;
    [SerializeField]
    private GameObject finishPad;
    [SerializeField]
    private GameObject lockBlock;
    [SerializeField]
    private GameObject keyPickup;
    [SerializeField]
    private GameObject gem1;
    [SerializeField]
    private GameObject gem2;
    [SerializeField]
    private GameObject gem3;
    [SerializeField]
    private GameObject gem4;
    [SerializeField]
    private GameObject gem5;


    [SerializeField]
    GameObject NavMesh;

    // Start is called before the first frame update
    void Start()
    {
        mazeGenData.getNewSeed();
        var maze = MazeGen.Generate(mazeGenData.Width, mazeGenData.Height, mazeGenData.getRang());
        Draw(maze);
        mazeGenData.keyCollected = 0;
    }

    private void Draw(WallState[,] maze)
    {
        var rang = mazeGenData.getRang();
        bool startPlaced = false;
        bool referencePlaced = false;
        bool finishPlaced = false;

        int refPointHeight = rang.Next(0, mazeGenData.Height);
        int refPointWidth = rang.Next(0, mazeGenData.Width);

        while ((refPointHeight == mazeGenData.Height && refPointWidth == mazeGenData.Width) 
            || (refPointHeight == 0 && refPointWidth == 0))
        {
            refPointHeight = rang.Next(0, mazeGenData.Height);
            refPointWidth = rang.Next(0, mazeGenData.Width);
        }

        int gem1W = rang.Next(0, mazeGenData.Width);
        int gem2W = rang.Next(0, mazeGenData.Width);
        int gem3W = rang.Next(0, mazeGenData.Width);
        int gem4W = rang.Next(0, mazeGenData.Width);
        int gem5W = rang.Next(0, mazeGenData.Width);
        int gem1H = rang.Next(0, mazeGenData.Height);
        int gem2H = rang.Next(0, mazeGenData.Height);
        int gem3H = rang.Next(0, mazeGenData.Height);
        int gem4H = rang.Next(0, mazeGenData.Height);
        int gem5H = rang.Next(0, mazeGenData.Height);

        //var floor = Instantiate(floorPrefab, transform);
        //floor.position = new Vector3(0, -1, 0);
        //floor.localScale = new Vector3(20, floor.localScale.y, 20);

        for (int i = 0; i < mazeGenData.Width; i++)
        {
            for (int j = 0; j < mazeGenData.Height; j++)
            {
                var cell = maze[i, j];
                var position = new Vector3(-mazeGenData.Width / 2 + i, 0, -mazeGenData.Height / 2 + j);

                //var floor = Instantiate(floorPrefab, transform);
                //floor.position = position + new Vector3(0, -1, mazeGenData.Size / 2);
                //floor.localScale = new Vector3(mazeGenData.Size, floor.localScale.y, mazeGenData.Size*2);

                //Place the start node exactly ONE time
                if (startPlaced == false)
                {
                    startPlaced = true;
                    if (debugTestingMode == true)
                    {
                        var startNode = Instantiate(startNodePrefab, transform);
                        startNode.position = position + new Vector3(0, 0, 0);
                        startNode.localScale = new Vector3(startNode.localScale.x, startNode.localScale.y * 10, startNode.localScale.z);
                    }
                    else
                    {
                        var startNode = Instantiate(startPadPrefab, transform);
                        startNode.position = position + new Vector3(0, 0, 0);
                        startNode.localScale = new Vector3(startNode.localScale.x, startNode.localScale.y, startNode.localScale.z);
                        playerCharacter.transform.position = position + new Vector3(0, 0, 0);
                    }
                }

                //Place the finish node exactly ONE time
                if (finishPlaced == false && i == mazeGenData.Width - 1 && j == mazeGenData.Height - 1)
                {
                    finishPlaced = true;
                    if (debugTestingMode == true)
                    {
                        var finishNode = Instantiate(finishNodePrefab, transform);
                        finishNode.position = position + new Vector3(0, 0, 0);
                        finishNode.localScale = new Vector3(finishNode.localScale.x, finishNode.localScale.y * 10, finishNode.localScale.z);
                        lockBlock.transform.position = position + new Vector3(0, 0, 0);
                    }
                    else
                    {
                        finishPad.transform.position = position + new Vector3(0, 0, 0);
                        lockBlock.transform.position = position + new Vector3(0, 0, 0);


                        //var finishNode = Instantiate(finishPadPrefab, transform);
                        //finishNode.position = position + new Vector3(0, 0, 0);
                        //finishNode.localScale = new Vector3(finishNode.localScale.x, finishNode.localScale.y, finishNode.localScale.z);
                    }
                }

                //Place the reference node exactly ONE time
                if (debugTestingMode == true && referencePlaced == false && i == refPointWidth && j == refPointHeight)
                {
                    Debug.Log("Reference Placement Reached!");
                    var referenceNode = Instantiate(referenceNodePrefab, transform);
                    referenceNode.position = position + new Vector3(0, 0, 0);
                    referenceNode.localScale = new Vector3(referenceNode.localScale.x, referenceNode.localScale.y * 10, referenceNode.localScale.z);
                    referencePlaced = true;
                }
                else if (debugTestingMode == false && referencePlaced == false && i == refPointWidth && j == refPointHeight)
                {
                    Debug.Log("Reference Placement Reached!");
                    keyPickup.transform.position = position + new Vector3(0, 0, 0);
                    referencePlaced = true;
                }
                {//Gem Placement
                    if (i == gem1W && j == gem1H) { gem1.transform.position = position + new Vector3(0, 0, 0); }
                    if (i == gem2W && j == gem2H) { gem2.transform.position = position + new Vector3(0, 0, 0); }
                    if (i == gem3W && j == gem3H) { gem3.transform.position = position + new Vector3(0, 0, 0); }
                    if (i == gem4W && j == gem4H) { gem4.transform.position = position + new Vector3(0, 0, 0); }
                    if (i == gem5W && j == gem5H) { gem5.transform.position = position + new Vector3(0, 0, 0); }
                }

                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, 0, mazeGenData.Size / 2);
                    topWall.localScale = new Vector3(mazeGenData.Size, topWall.localScale.y, topWall.localScale.z);
                }

                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-mazeGenData.Size / 2, 0, 0);
                    leftWall.localScale = new Vector3(mazeGenData.Size, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                }

                if (i == mazeGenData.Width - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(+mazeGenData.Size / 2, 0, 0);
                        rightWall.localScale = new Vector3(mazeGenData.Size, rightWall.localScale.y, rightWall.localScale.z);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                    }
                }

                if (j == 0)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        bottomWall.position = position + new Vector3(0, 0, -mazeGenData.Size / 2);
                        bottomWall.localScale = new Vector3(mazeGenData.Size, bottomWall.localScale.y, bottomWall.localScale.z);
                    }
                }
            }
        }
    }
}
