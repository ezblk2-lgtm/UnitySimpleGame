using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public List<GameObject> blocks;
    public List<GameObject> carsNPC;
    public GameObject player;
    public GameObject roadPrefab;
    public GameObject carNPCPrefab;

    private System.Random rand = new System.Random();
    private List<GameObject> toRemoveBlock = new List<GameObject>();
    private List<GameObject> toRemoveCar = new List<GameObject>();
    private int countNPC = 0;
    void Update()
    {
        float z = player.GetComponent<MovingCar>().rb.position.z;

        var last = blocks[blocks.Count - 1];

        if (z > last.transform.position.z - 15f * 20f)
        {
            var block = Instantiate(roadPrefab, new Vector3(last.transform.position.x, last.transform.position.y, last.transform.position.z + 15f), Quaternion.identity);
            block.transform.SetParent(gameObject.transform);
            blocks.Add(block);

            countNPC++;

            if (countNPC == 2)
            {
                float side = rand.Next(1, 3) == 1 ? 0.2f : 4.75f;

                var car = Instantiate(carNPCPrefab, new Vector3(last.transform.position.x + side, last.transform.position.y + 0.2f, last.transform.position.z), Quaternion.identity);
                car.transform.SetParent(gameObject.transform);
                carsNPC.Add(car);

                countNPC = 0;
            }
        }

        toRemoveBlock.Clear();
        foreach (GameObject block in blocks) 
        {
            bool fetched = block.GetComponent<RoadBlock>().fetch(z);

            if (fetched)
            {
                toRemoveBlock.Add(block);
            }
        }

        foreach(GameObject block in toRemoveBlock)
        {
            blocks.Remove(block);
            block.GetComponent<RoadBlock>().Delete();
        }

        toRemoveCar.Clear();
        foreach(GameObject carNPC in carsNPC)
        {
            bool fetched = carNPC.GetComponent<MovingCarNPC>().fetch(z);

            if (fetched)
            {
                toRemoveCar.Add(carNPC);
            }
        }

        foreach(GameObject carNPC in toRemoveCar)
        {
            carsNPC.Remove(carNPC);
            carNPC.GetComponent<MovingCarNPC>().Delete();
        }
    }
}
