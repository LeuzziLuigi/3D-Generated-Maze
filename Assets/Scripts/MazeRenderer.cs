using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField]
    [Range(1,100)]
    private int width = 10;

    [SerializeField]
    [Range(1,100)]
    private int height = 10;

    [SerializeField]
    private float size = 1f;

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
    [Range(1, 999999)]
    private int seed = 0;

    // Start is called before the first frame update
    void Start()
    {
        var maze = MazeGen.Generate(width, height, seed);
        Draw(maze);
    }

    private void Draw(WallState[,] maze)
    {
        var rang = new System.Random(seed);
        bool startPlaced = false;
        bool referencePlaced = false;
        bool finishPlaced = false;


        for (int i=0; i < width; i++)
        {
            for (int j=0; j<height; j++)
            {
                var cell = maze[i, j];
                var position = new Vector3(-width / 2 + i, 0, -height / 2 + j);

                var floor = Instantiate(floorPrefab, transform);
                floor.position = position + new Vector3(0, -1, size / 2);
                floor.localScale = new Vector3(size, floor.localScale.y, size*2);

                //Place the start node exactly ONE time
                if (startPlaced == false)
                {
                    startPlaced = true;
                    var startNode = Instantiate(startNodePrefab, transform);
                    startNode.position = position + new Vector3(0, 0, 0);
                    startNode.localScale = new Vector3(startNode.localScale.x, startNode.localScale.y * 10, startNode.localScale.z);
                }

                //Place the finish node exactly ONE time
                if (finishPlaced == false && i == width-1 && j == height-1)
                {
                    finishPlaced = true;
                    var finishNode = Instantiate(finishNodePrefab, transform);
                    finishNode.position = position + new Vector3(0, 0, 0);
                    finishNode.localScale = new Vector3(finishNode.localScale.x, finishNode.localScale.y * 10, finishNode.localScale.z);
                }

                //Place the reference node exactly ONE time
                if (referencePlaced == false && i == rang.Next(0, width) && j == rang.Next(0, height))
                {
                    var referenceNode = Instantiate(referenceNodePrefab, transform);
                    referenceNode.position = position + new Vector3(0, 0, 0);
                    referenceNode.localScale = new Vector3(referenceNode.localScale.x, referenceNode.localScale.y * 10, referenceNode.localScale.z);
                    referencePlaced = true;
                }

                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, 0, size/2);
                    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                }

                if(cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-size / 2, 0, 0);
                    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0, 90, 0);
                }

                if(i == width-1)
                {
                    if(cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(+size / 2, 0, 0);
                        rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                        rightWall.eulerAngles = new Vector3(0, 90, 0);
                    }
                }

                if(j==0)
                {
                    if(cell.HasFlag(WallState.DOWN))
                    {
                        var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        bottomWall.position = position + new Vector3(0, 0, -size / 2);
                        bottomWall.localScale = new Vector3(size, bottomWall.localScale.y, bottomWall.localScale.z);
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
