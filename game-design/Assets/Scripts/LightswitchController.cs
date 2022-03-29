using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightswitchController : MonoBehaviour
{
    // Reference to the light source object
    public GameObject lightSource;

    // Is the player in range?
    private bool inRange;

    // Start is called before the first frame update
    void Start()
    {
        // Lights start on off
        lightSource.SetActive(false);

        // At first, the player is not in range
        inRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            // Was the "E" key pressed?
            if (Input.GetKeyDown(KeyCode.E))
            {
                lightSource.SetActive(!lightSource.activeSelf);

                // Play the lightswitch sound
                GetComponent<AudioSource>().Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
