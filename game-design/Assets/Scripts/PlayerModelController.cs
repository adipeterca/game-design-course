using System.Collections;
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
    private float movementSpeedMultiplier = 10.0f;

    // Flag which tells us if the player is sprinting or not
    private bool sprinting = false;

    // Constant multiplier for when the player sprints
    private const float sprintingMultiplier = 2.0f;

    // Flag which tells us if the player has been exhausted before
    private bool firstTimeExhausted = false;

    // Constant for the timer value
    private const float TIMER_VALUE = 5.0f;

    // Timer during which player can not sprint
    // When the player is exhausted, a period of `timer` seconds must pass until they can sprint again
    private float timer = TIMER_VALUE;

    // Constant amount to increase/decrease stamina by
    private const float staminaModifyValue = 0.5f;

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
        if (GameManagerController.Instance.gameOver) return;

        if (GuiManagerController.Instance.optionsMenu.activeSelf) return;

        if (!firstTimeExhausted)
        {
            UpdateStamina();
        }
        else
        {
            if (timer > 0.0f)
            {
                GuiManagerController.Instance.IncreaseStamina(staminaModifyValue / 8);
                timer -= Time.deltaTime;
                if (timer < 0.0f)
                    timer = 0.0f;
            }
            else
            {
                UpdateStamina();
            }
        }
    }

    void FixedUpdate()
    {
        if (GameManagerController.Instance.gameOver) return;

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        if (sprinting) {
            rb.AddForce(new Vector3(h, 0, v) * movementSpeedMultiplier * sprintingMultiplier);
        }
        else {
            rb.AddForce(new Vector3(h, 0, v) * movementSpeedMultiplier);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            // Destroy the pickup object and increase the score
            Destroy(other.gameObject.transform.parent.gameObject);
            GameManagerController.Instance.IncreaseScore();
        }
        else if (other.CompareTag("Jumpscare"))
        {
            // For the moment, each trigger will be destroyed.
            // As a future improvement, each trigger could be reseted every X seconds.
            GuiManagerController.Instance.Jumpscare();

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Endgame"))
        {
            GuiManagerController.Instance.DisplayEndgameMenu(1);

            // Destroy the enemy
            Destroy(EnemyModelController.Instance.gameObject);

        }
    }

    /// <summary>
    /// Private method for initializing game variables.
    /// </summary>
    private void InitializePlayerState()
    {
        GuiManagerController.Instance.SetStamina(100);
    }

    void UpdateStamina()
    {
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) &&
            Input.GetKey(KeyCode.LeftShift) && GuiManagerController.Instance.GetStamina() > 0.0f)
        {
            sprinting = true;
            GuiManagerController.Instance.DecreaseStamina(staminaModifyValue);

            if (GuiManagerController.Instance.GetStamina() <= 0.0f)
            {
                firstTimeExhausted = true;
                sprinting = false;
                timer = TIMER_VALUE;
            }
        }
        else
        {
            sprinting = false;
            // To take eight times as long to get back the stamina 
            GuiManagerController.Instance.IncreaseStamina(staminaModifyValue / 8);
        }
    }
}
