﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton class which handles the control and behaviour of the Player object.
/// </summary>
public class PlayerModelController : MonoBehaviour
{
    private PlayerModelController() { }

    public static PlayerModelController Instance
    {
        get;
        private set;
    }

    // Multiplier for movement speed
    private float movementSpeedMultiplier = 10;

    // Rigidbody of the Player object
    private Rigidbody rb;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(Instance);
        else
            Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InitializePlayerState();
    }

    void Update()
    {
        // The amount to modify (increase/decrease) stamina 
        float staminaModifyValue = 0.5f;

        // Just for testing the implementation
        if (Input.GetKey(KeyCode.Space) && GuiManagerController.Instance.GetStamina() >= 0.1f)
        {
            GuiManagerController.Instance.DecreaseStamina(staminaModifyValue);
        }
        else
        {
            // To take eight times as long to get back the stamina
            GuiManagerController.Instance.IncreaseStamina(staminaModifyValue / 8);
        }
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
        else if (other.CompareTag("Jumpscare"))
        {
            // For the moment, each trigger will be destroyed.
            // As a future improvement, each trigger could be reseted every X seconds.
            GuiManagerController.Instance.Jumpscare();

            Destroy(other.gameObject);
        }
    }

    private void InitializePlayerState()
    {
        // Set the stamina to 100 at first
        GuiManagerController.Instance.SetStamina(100);
    }
}
