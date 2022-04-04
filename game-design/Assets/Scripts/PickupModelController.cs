using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupModelController : MonoBehaviour
{
    // Cap for minimum value along the Y axis
    private float ymin = 0.6f;
    // Cap for maximum value along the Y axis
    private float ymax = 1.1f;
    // Is the object going up of down?
    private bool goingUp = true;

    void Update()
    {
        Animate();
    }

    /// <summary>
    /// Private function that handles the movement along the Y axis and the rotation of the model.
    /// </summary>
    void Animate()
    {
        Vector3 movementStep = new Vector3(0, 0.5f, 0);
        Vector3 rotationStep = new Vector3(0, 45, 0);

        // Modify position
        if (goingUp)
        {
            transform.position += movementStep * Time.deltaTime;
            if (transform.position.y >= ymax)
            {
                goingUp = false;
            }
        }
        else
        {
            transform.position -= movementStep * Time.deltaTime;
            if (transform.position.y <= ymin)
            {
                goingUp = true;
            }
        }

        // Rotation movement
        transform.Rotate(rotationStep * Time.deltaTime, Space.World);
    }
}
