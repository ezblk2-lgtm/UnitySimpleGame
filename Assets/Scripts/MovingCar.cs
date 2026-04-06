using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingCar : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject car;
    public GameObject modelHolder;

    public Controls control;

    private float maxSound = 1.8f;
    private float pitch = 0.2f;

    private bool isAlive = true;
    private bool isKilled = false;

    public List<GameObject> wheels;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isAlive)
        {
            float newSpeed = 0f;
            float sideSpeed = 0f;

            if (control != null)
            {
                newSpeed = control.speed;
                sideSpeed = control.sideSpeed;
            }

            transform.position += new Vector3(sideSpeed * Time.deltaTime, 0f, newSpeed * Time.deltaTime);
         
            float newPitch = (pitch + (newSpeed / 100));
            
            if (pitch > maxSound)
            {
                pitch = maxSound;
            }

            car.GetComponent<AudioSource>().pitch = newPitch;

            if (wheels.Count > 0)
            {
                foreach (var wheel in wheels)
                {
                    wheel.transform.Rotate(-3f, 0f, 0f);
                }
            }

            if (tag == "Car" && transform.position.y < -50f)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Road")) return;
        if (other.CompareTag("RoadLine")) return;
        if (other.CompareTag("Player")) return;

        if (other.CompareTag("Car") || other.CompareTag("Wall"))
        {
            isAlive = false;

            if (!isKilled)
            {
                isKilled = true;
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
