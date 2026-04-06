using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    private float maxSpeed = 30f;
    private float minSpeed = 3f;
    public float speed = 0f;
    public float sideSpeed = 0f;

    void Update()
    {
        float moveForward = 0f;
        if (Keyboard.current.wKey.isPressed) moveForward = 1f;
        if (Keyboard.current.sKey.isPressed) moveForward = -1f;

        float moveSide = 0f;
        if (Keyboard.current.dKey.isPressed) moveSide = 3f;
        if (Keyboard.current.aKey.isPressed) moveSide = -3f;

        if (moveSide != 0)
        {
            sideSpeed = moveSide;
        }
        else
        {
            sideSpeed = 0;
        }

        if (moveForward != 0)
        {
            speed += 0.1f * moveForward;
        }
        else
        {
            if (speed > 0)
            {
                speed -= 0.1f;
            }
            else
            {
                speed += 0.1f;
            }
        }

        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }

        if (speed < minSpeed)
        {
            speed = minSpeed;
        }
    }
}
