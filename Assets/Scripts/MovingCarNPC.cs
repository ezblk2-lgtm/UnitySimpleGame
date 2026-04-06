using System.Collections.Generic;
using UnityEngine;

public class MovingCarNPC : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject car;
    public GameObject modelHolder;




    private bool isAlive = true;

    public List<GameObject> wheels;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isAlive)
        {
            float speed = 2f;

            transform.position += new Vector3(0, 0f, speed * Time.deltaTime);


            if (wheels.Count > 0)
            {
                foreach (var wheel in wheels)
                {
                    wheel.transform.Rotate(-3f, 0f, 0f);
                }
            }
        }
    }
    public bool fetch(float z)
    {
        bool result = false;

        if (z > transform.position.z + 100f)
        {
            result = true;
        }

        return result;
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}
