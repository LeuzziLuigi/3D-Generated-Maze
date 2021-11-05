using UnityEngine;


[CreateAssetMenu(fileName = "MazeData", menuName ="ScriptableObjects/MazeGenData")]
public class MazeGenData : ScriptableObject
{
    [SerializeField]
    [Range(1, 100)]
    private int width = 10;

    [SerializeField]
    [Range(1, 100)]
    private int height = 10;

    [SerializeField]
    private float size = 1f;

    [SerializeField]
    [Range(1, 999999)]
    private int seed = 0;

    private System.Random rang;

    // Start is called before the first frame update
    void Start()
    {
        resetSeed();
    }

    public int Width
    {
        get
        {
            return width;
        }

        set
        {
            width = value;
        }
    }

    public int Height
    {
        get
        {
            return height;
        }

        set
        {
            height = value;
        }
    }

    public float Size
    {
        get
        {
            return size;
        }

        set
        {
            size = value;
        }
    }

    public int Seed
    {
        get
        {
            return seed;
        }

        set
        {
            seed = value;
        }
    }

    public void getNewSeed()
    {
        seed = Random.Range(1, 999999);
    }

    public void resetSeed()
    {
        rang = new System.Random(seed);
    }
}
