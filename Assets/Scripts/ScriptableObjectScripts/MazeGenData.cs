using UnityEngine;


[CreateAssetMenu(fileName = "MazeData", menuName = "ScriptableObjects/MazeGenData")]
public class MazeGenData : ScriptableObject
{
    [SerializeField]
    [Range(1, 100)]
    private int width = 15;

    [SerializeField]
    [Range(1, 100)]
    private int height = 15;

    [SerializeField]
    private float size = 1f;

    [SerializeField]
    [Range(1, 999999)]
    private int seed = 0;

    private System.Random rang;

    public float timeScore;
    public int totalGems = 0;

    public bool levelFinished = false;
    public int keyCollected = 0;

    // Start is called before the first frame update
    void Awake()
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

    public System.Random getRang()
    {
        if(rang != null)
        {
            return rang;
        }
        else
        {
            resetSeed();
            return rang;
        }
    }

    public int getNextRandInt(int min, int max)
    {
        return rang.Next(min, max);
    }
}