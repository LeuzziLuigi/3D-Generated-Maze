using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnvironment : MonoBehaviour
{
    public int x = 10, y = 10; //board dimension
    public int size = 10;
    public int padding = 2;

    public GameObject[] mainTree;
    public GameObject[] secondaryTree;
    public GameObject[] miscelanneous;

    public GameObject[] grass;
    public GameObject[] bigObject;

    public int spacing = 5;
    public int spacingSimilar = 2;

    public float mainTreeDensity = 0.6f;
    public float secondaryTreeDensity = 0.3f;
    public float miscelanneousDensity = 0.1f;
    

    private int mainTreeQuantity;
    private int secondaryTreeQuantity;
    private int miscelanneousQuantity;
    private int bigObjectQuantity = 1;


    private struct Sector
    {
        public int xStart, xEnd, yStart, yEnd;
        public Sector(int sxStart, int sxEnd, int syStart, int syEnd)
        {
            xStart = sxStart;
            xEnd = sxEnd;
            yStart = syStart;
            yEnd = syEnd;
        }
    }


    private Sector[] sectors = new Sector[7];

    private void Awake()
    {
        mainTreeQuantity = (int)((x + y) * mainTreeDensity);
        secondaryTreeQuantity = (int)((x + y) * secondaryTreeDensity);
        miscelanneousQuantity = (int)((x + y) * miscelanneousDensity);
     
    }



    

    void Start()
    {
        int sectorNumber = Random.Range(0, 7);
        List<Vector3> locations = new List<Vector3>(); // .z is for type (mainTree=0, secondaryTree=1, miscelanneous=2, bigObject=3)


        int infLoop = 0;
        while(mainTreeQuantity > 0 || infLoop > 1000)
        {
            float locationX = Random.Range(sectors[sectorNumber].xStart, sectors[sectorNumber].xEnd);
            float locationY = Random.Range(sectors[sectorNumber].yStart, sectors[sectorNumber].yEnd);
            Vector3 current = new Vector3(locationX, locationY, 0);
            if (validPosition(locations, current))
            {
                current.z = 0;
                Instantiate(mainTree[Random.Range(0, mainTree.Length)], current, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                mainTreeQuantity--;
                infLoop = 0;
            }
            infLoop++;
        }
        while (secondaryTreeQuantity > 0 || infLoop > 1000)
        {
            float locationX = Random.Range(sectors[sectorNumber].xStart, sectors[sectorNumber].xEnd);
            float locationY = Random.Range(sectors[sectorNumber].yStart, sectors[sectorNumber].yEnd);
            Vector3 current = new Vector3(locationX, locationY, 1);
            if (validPosition(locations, current))
            {
                current.z = 0;
                Instantiate(secondaryTree[Random.Range(0, secondaryTree.Length)], current, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                secondaryTreeQuantity--;
                infLoop = 0;
            }
            infLoop++;
        }
        while (miscelanneousQuantity > 0 || infLoop > 1000)
        {
            float locationX = Random.Range(sectors[sectorNumber].xStart, sectors[sectorNumber].xEnd);
            float locationY = Random.Range(sectors[sectorNumber].yStart, sectors[sectorNumber].yEnd);
            Vector3 current = new Vector3(locationX, locationY, 0);
            if (validPosition(locations, current))
            {
                current.z = 0;
                Instantiate(miscelanneous[Random.Range(0, miscelanneous.Length)], current, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                miscelanneousQuantity--;
                infLoop = 0;
            }
            infLoop++;
        }

        while(bigObjectQuantity > 0)
        {
            //Instantiate outside of range... do this after. One more thing, add a plane with grass
        }

    }

    private bool validPosition(List<Vector3> positions, Vector3 currentPosition)
    {
        foreach(Vector3 position in positions)
        {
            float distance = Vector2.Distance(new Vector2(currentPosition.x, currentPosition.y), new Vector2(position.x, position.y));
            if (distance < spacing)
            {
                if (!(currentPosition.z == position.z && distance > spacingSimilar))
                {
                    return false;
                }                    
            }
        }
        return true;
    }

    
    void Update()
    {
        
    }

    private void setSectors()
    {
        sectors[0] = new Sector(padding, -size - padding, padding, -size - padding);
        sectors[1] = new Sector(padding, -size - size, 0, y);
        sectors[2] = new Sector(-padding, -size - padding, y + padding, y + padding + size);
        sectors[3] = new Sector(0, x, y + padding, y + padding + size);
        sectors[4] = new Sector(x + padding, x + padding + size, y + padding, y + padding + size);
        sectors[5] = new Sector(x + padding, x + padding + size, 0, y);
        sectors[6] = new Sector(x + padding, x + padding + size, 0 - padding, 0 - padding - size);
        sectors[7] = new Sector(0, x, 0 - padding, 0 - padding + size);
    }

}
