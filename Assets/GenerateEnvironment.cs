using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnvironment : MonoBehaviour
{
    public int x = 100, y = 10; //board dimension
    public int size = 100;
    public int padding = 2;

    public GameObject[] mainTree;
    public GameObject[] secondaryTree;
    public GameObject[] miscelanneous;

    public GameObject[] grass;
    public GameObject[] bigObject;

    public int spacing = 10;

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


    private Sector[] sectors = new Sector[8];

    private void Awake()
    {
        mainTreeQuantity = (int)((x + y) * mainTreeDensity);
        secondaryTreeQuantity = (int)((x + y) * secondaryTreeDensity);
        miscelanneousQuantity = (int)((x + y) * miscelanneousDensity);
        setSectors();
    }



    

    void Start()
    {
        instantiateRandomly();        
    }

    //pick a location
    //check for nearby objects
    //if valid, place; otherwise, redo
    private void instantiateRandomly()
    {
        List<Vector3> locations = new List<Vector3>(); // .z is for type (mainTree=0, secondaryTree=1, miscelanneous=2, bigObject=3)
        bool done = false;
        int i = 0;

        while (!done && i < 100)
        {
            done = true;
            int sectorNumber = Random.Range(0, 8);
            bool valid = false;
            Vector3 current = Vector3.zero;

            while (!valid && i < 100)
            {
                current.x = Random.Range(sectors[sectorNumber].xStart, sectors[sectorNumber].xEnd);
                current.z = Random.Range(sectors[sectorNumber].yStart, sectors[sectorNumber].yEnd);
                valid = validPosition(locations, current);
                i++;
            }

            if (valid)
            {
                if (mainTreeQuantity > 0)
                {
                    Instantiate(mainTree[Random.Range(0, mainTree.Length)], current, Quaternion.Euler(90, 0, Random.Range(0, 360)), transform);
                    mainTreeQuantity--;
                    done = false;
                }
            }
            
        }
        
        
        /*
        while (secondaryTreeQuantity > 0 && infLoop < 10)
        {
            float locationX = Random.Range(sectors[sectorNumber].xStart, sectors[sectorNumber].xEnd);
            float locationY = Random.Range(sectors[sectorNumber].yStart, sectors[sectorNumber].yEnd);
            Vector3 current = new Vector3(locationX, locationY, 1);
            if (validPosition(locations, current))
            {
                current.z = 0;
                Instantiate(secondaryTree[Random.Range(0, secondaryTree.Length - 1)], current, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                secondaryTreeQuantity--;
                infLoop = 0;
            }
            infLoop++;
        }
        while (miscelanneousQuantity > 0 && infLoop < 10)
        {
            float locationX = Random.Range(sectors[sectorNumber].xStart, sectors[sectorNumber].xEnd);
            float locationY = Random.Range(sectors[sectorNumber].yStart, sectors[sectorNumber].yEnd);
            Vector3 current = new Vector3(locationX, locationY, 0);
            if (validPosition(locations, current))
            {
                current.z = 0;
                Instantiate(miscelanneous[Random.Range(0, miscelanneous.Length - 1)], current, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                miscelanneousQuantity--;
                infLoop = 0;
            }
            infLoop++;
        }

        while (bigObjectQuantity > 0)
        {
            //Instantiate outside of range... do this after. One more thing, add a plane with grass
        }
        */
    }


    private bool validPosition(List<Vector3> locations, Vector3 currentLocation)
    {

        foreach(Vector3 location in locations)
        {
            if (Vector3.Distance(location, currentLocation) < spacing)
                return false;    
        }
        locations.Add(currentLocation);
        return true;
    }

    
    void Update()
    {
        
    }

    private void setSectors()
    {
        sectors[0] = new Sector(-padding, -size - padding, -padding, -size - padding);
        sectors[1] = new Sector(-padding, -padding - size, 0, y);
        sectors[2] = new Sector(-padding, -size - padding, y + padding, y + padding + size);
        sectors[3] = new Sector(0, x, y + padding, y + padding + size);
        sectors[4] = new Sector(x + padding, x + padding + size, y + padding, y + padding + size);
        sectors[5] = new Sector(x + padding, x + padding + size, 0, y);
        sectors[6] = new Sector(x + padding, x + padding + size, 0 - padding, 0 - padding - size);
        sectors[7] = new Sector(0, x, 0 - padding, 0 - padding - size);
    }

    private void printSectors()
    {
        int i = 0;
        foreach (Sector sector in sectors)
        {
            Debug.Log("Sector " + i);
            Debug.Log(sector.xStart + " " + sector.xEnd + " " + sector.yStart + " " + sector.yEnd);
            i++;
        }
    }

}
