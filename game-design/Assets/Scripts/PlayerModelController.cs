using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelController : MonoBehaviour
{
    // Multiplier for movement speed
    public float movementSpeedMultiplier = 1;

    // Rigidbody of the Player object
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        rb.AddForce(new Vector3(h, 0, v) * movementSpeedMultiplier);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            // Destroy the pickup object and increase the score
            Destroy(other.gameObject.transform.parent.gameObject);
            GameManagerController.Instance.IncreaseScore();

            Debug.Log("Got one point!");
        }
    }
}
