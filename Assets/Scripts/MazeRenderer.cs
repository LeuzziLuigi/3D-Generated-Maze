using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestingScripts;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField]
    private MazeGenData mazeGenData;

    [SerializeField]
    private Transform wallPrefab = null;
    [SerializeField]
    private Transform floorPrefab = null;
    [SerializeField]
    private Transform startNodePrefab = null;
    [SerializeField]
    private Transform referenceNodePrefab = null;
    [SerializeField]
    private Transform finishNodePrefab = null;

    [SerializeField]
    GameObject NavMesh;

    // Start is called before the first frame update
    void Start()
    {
        var maze = MazeGen.Generate(mazeGenData.Width, mazeGenData.Height, mazeGenData.Seed);
        Draw(maze);
    }

    private void Draw(WallState[,] maze)
    {
        var rang = new System.Random(mazeGenData.Seed);
        bool startPlaced = false;
        bool referencePlaced = false;
        bool finishPlaced = false;

        var floor = Instantiate(floorPrefab, transform);
        floor.position = new Vector3(0, -1, 0);
        floor.localScale = new Vector3(20, floor.localScale.y, 20);
        for (int i=0; i < mazeGenData.Width; i++)
        {
            for (int j=0; j<mazeGenData.Height; j++)
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
                    var startNode = Instantiate(startNodePrefab, transform);
                    startNode.position = position + new Vector3(0, 0, 0);
                    startNode.localScale = new Vector3(startNode.localScale.x, startNode.localScale.y * 10, startNode.localScale.z);
                }

                //Place the finish node exactly ONE time
                if (finishPlaced == false && i == mazeGenData.Width-1 && j == mazeGenData.Height-1)
                {
                    finishPlaced = true;
                    var finishNode = Instantiate(finishNodePrefab, transform);
                    finishNode.position = position + new Vector3(0, 0, 0);
                    finishNode.localScale = new Vector3(finishNode.localScale.x, finishNode.localScale.y * 10, finishNode.localScale.z);
                }

                //Place the reference node exactly ONE time
                if (referencePlaced == false && i == rang.Next(0, mazeGenData.Width) && j == rang.Next(0, mazeGenData.Height))
                {
                    var referenceNode = Instantiate(referenceNodePrefab, transform);
                    referenceNode.position = position + new Vector3(0, 0, 0);
                    referenceNode.localScale = new Vector3(referenceNode.localScale.x, referenceNode.localScale.y * 10, referenceNode.localScale.z);
                    referencePlaced = true;
                }

                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, 0, mazeGenData.Size/2);
                    topWall.localScale = new Vector3(mazeGenData.Size, topWall.localScale.y, topWall.localScale.z);
                }

                if(cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-mazeGenData.Size / 2, 0, 0);
                    leftWall.localScale = new Vector3(mazeGenData.Size, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                }

                if(i == mazeGenData.Width-1)
                {
                    if(cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(+mazeGenData.Size / 2, 0, 0);
                        rightWall.localScale = new Vector3(mazeGenData.Size, rightWall.localScale.y, rightWall.localScale.z);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                    }
                }

                if(j==0)
                {
                    if(cell.HasFlag(WallState.DOWN))
                    {
                        var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        bottomWall.position = position + new Vector3(0, 0, -mazeGenData.Size / 2);
                        bottomWall.localScale = new Vector3(mazeGenData.Size, bottomWall.localScale.y, bottomWall.localScale.z);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
