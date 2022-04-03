using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Singleton class responsible with handling all GUI updates.<br></br>
/// </summary>
public class GuiManagerController : MonoBehaviour
{
    // Reference for the stamina slider Game Object
    public Slider staminaSlider;

    // Reference for the pickup count text Game Object
    public TextMeshProUGUI countText;

    // Reference to the options menu Game Object
    public GameObject optionsMenu;

    private GuiManagerController() { }

    public static GuiManagerController Instance
    {
        get;
        private set;
    }

    // Image used in jumpscare effects
    public GameObject jumpscareImage;

    private void Awake()
    {
        if (Instance != this && Instance != null)
            Destroy(Instance);
        else
            Instance = this;
    }

    private void Start()
    {
        UpdateCountText();
    }

    /// <summary>
    /// Public method for updating the number of picked items.
    /// </summary>
    public void UpdateCountText()
    {
        countText.text = GameManagerController.Instance.GetScore() + " / " + GameManagerController.Instance.pickupCount;
    }

    /// <summary>
    /// Public method used for simulating a jumpscare effect on the whole screen.
    /// </summary>
    public void Jumpscare()
    {
        jumpscareImage.GetComponent<Animator>().SetTrigger("JumpscareTrigger");
        jumpscareImage.GetComponent<AudioSource>().Play();
    }

    /// <summary>
    /// Public method used for setting the value of the stamina slider.<br></br>
    /// It just sets the value and DOES NOT contain any logic whatsoever.<br></br>
    /// The value should be between 0 and 100.
    /// </summary>
    /// <param name="amount">value to set</param>
    public void SetStamina(float amount)
    {
        staminaSlider.value = amount;
    }

    /// <summary>
    /// Public method used for decreasing a specified amount from the current value of the stamina slider.
    /// </summary>
    /// <param name="amount">the amount to decrease</param>
    public void DecreaseStamina(float amount)
    {
        staminaSlider.value -= amount;
    }

    /// <summary>
    /// Public method used for increasing a specified amount from the current value of the stamina slider.
    /// </summary>
    /// <param name="amount">the amount to increase</param>
    public void IncreaseStamina(float amount)
    {
        staminaSlider.value += amount;
    }

    /// <summary>
    /// Public method for getting the stamina value.
    /// </summary>
    /// <returns>the stamina value</returns>
    public float GetStamina()
    {
        return staminaSlider.value;
    }
}
