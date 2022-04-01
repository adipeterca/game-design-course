using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script used for testing
/// </summary>
public class testscript : MonoBehaviour
{
    float x = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            x = 180 - x;

            transform.position = new Vector3(transform.position.x, transform.position.y, -transform.position.z);
            transform.eulerAngles = new Vector3(35, x, 0);


        }
    }
}
