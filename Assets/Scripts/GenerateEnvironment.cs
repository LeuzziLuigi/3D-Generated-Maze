using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnvironment : MonoBehaviour
{
    public int x = 100, z = 100; //board dimension
    public int size = 100;
    public int padding = 10;

    public GameObject[] mainTree;
    public GameObject[] secondaryTree;
    public GameObject[] miscelanneous;

    public GameObject[] grass;
    public GameObject[] bigObject;

    public int spacing = 10;

    public float mainTreeDensity = 0.6f;
    public float secondaryTreeDensity = 0.4f;
    public float miscelanneousDensity = 0.1f;

    public bool random;

    public int bigObjectQuantity = 5;

    public float minScale = 0.4f;
    public float maxScale = 3f;


    private int mainTreeQuantity;
    private int secondaryTreeQuantity;
    private int miscelanneousQuantity;
    


    private void Awake()
    {
        SetQuantities();
    }

    void Start()
    {
        InstantiateRandomly();        
    }

    //pick a location
    //check for nearby objects
    //if valid, place; otherwise, re-do
    private void InstantiateRandomly()
    {
        List<Vector3> locations = new List<Vector3>(); // .z is for type (mainTree=0, secondaryTree=1, miscelanneous=2, bigObject=3)
        int i = 0;
        bool valid = true;
        while (valid && i < 100)
        {
            int sectorNumber = Random.Range(0, 8);
            valid = false;
            Vector3 current = Vector3.zero;

            while (!valid && i < 100)
            {
                current.x = Random.Range(-size -padding, x + padding + size);
                current.z = Random.Range(-size - padding, z + padding + size);
                valid = validPosition(locations, current);
                i++;
            }

            if (valid)
            {
                if (mainTreeQuantity > 0)
                {
                    GameObject newObj = Instantiate(mainTree[Random.Range(0, mainTree.Length)], current + transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0), transform);
                    newObj.transform.localScale *= Random.Range(minScale, maxScale);
                    mainTreeQuantity--;
                }
                else if (secondaryTreeQuantity > 0)
                {
                    GameObject newObj = Instantiate(secondaryTree[Random.Range(0, secondaryTree.Length)], current + transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0), transform);
                    newObj.transform.localScale *= Random.Range(minScale, maxScale);
                    secondaryTreeQuantity--;
                }
                else if (miscelanneousQuantity > 0)
                {
                    GameObject newObj = Instantiate(miscelanneous[Random.Range(0, miscelanneous.Length)], current + transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0), transform);
                    newObj.transform.localScale *= Random.Range(minScale, maxScale);
                    miscelanneousQuantity--;
                }
            }
        }

        //instantiate bigObjects even if there is no more space (i > 100)
        while(bigObjectQuantity > 0)
        {
            Vector3 current1 = Vector3.zero;
            current1.x = Random.Range(-size - padding, x + padding + size);
            current1.z = Random.Range(-size - padding, z + padding + size);
            //pushing them farther away so we dont have to worry about spacing
            current1.x = current1.x > 0 ? current1.x += size : current1.x -= size;
            current1.z = current1.z > 0 ? current1.z += size : current1.z -= size;
            GameObject newObj = Instantiate(bigObject[Random.Range(0, bigObject.Length)], current1 + transform.position, Quaternion.Euler(0, Random.Range(0, 360), 0), transform);
            newObj.transform.localScale *= Random.Range(size / 100, size / 100 + maxScale);
            bigObjectQuantity--;
        }

    }

    //return true if it is not in the maze and there are no nearby (spacing) objects
    private bool validPosition(List<Vector3> locations, Vector3 currentLocation)
    {
        if ((currentLocation.x > -padding && currentLocation.x < x + padding) && (currentLocation.z > -padding && currentLocation.z < z + padding))
            return false;
        foreach(Vector3 location in locations)
        {
            if (Vector3.Distance(location, currentLocation) < spacing)
                return false;    
        }
        locations.Add(currentLocation);
        return true;
    }

    
    private void SetQuantities()
    {
        mainTreeQuantity = (int)((x + z) * mainTreeDensity);
        secondaryTreeQuantity = (int)((x + z) * secondaryTreeDensity);
        miscelanneousQuantity = (int)((x + z) * miscelanneousDensity);

        if (random)
        {
            for (int i = 0; i < 10; i++)
            {
                int r = Random.Range(0, 3);
                Debug.Log(r);
                if (r == 0)
                {
                    r = Random.Range(1, 3);
                    if (r == 1)
                        mainTreeQuantity *= Random.Range(1, 5);
                    else
                        mainTreeQuantity = (int)mainTreeQuantity / Random.Range(1, 5);
                }
                else if (r == 1)
                {
                    r = Random.Range(1, 3);
                    if (r == 1)
                        secondaryTreeQuantity *= Random.Range(1, 5);
                    else
                        secondaryTreeQuantity = (int)mainTreeQuantity / Random.Range(1, 5);
                }
                else
                {
                    r = Random.Range(1, 3);
                    if (r == 1)
                        miscelanneousQuantity *= Random.Range(1, 5);
                    else
                        miscelanneousQuantity = (int)mainTreeQuantity / Random.Range(1, 5);
                }
            }
        }
    }

}
