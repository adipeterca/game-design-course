﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Camera and main menu controller
/// </summary>
public class MainMenu : MonoBehaviour
{
    // Public reference to the volume slider
    public Slider volumeSlider;

    // Private reference to the camera Animator component
    private Animator cameraAnim;

    private void Start()
    {
        cameraAnim = GetComponent<Animator>();
    }

    /// <summary>
    /// Public callback method for the Play button.<br></br>
    /// It activates the Camera animation and hides the buttons.
    /// </summary>
    public void PlayGame()
    {
        cameraAnim.SetTrigger("Play");
    }

    /// <summary>
    /// Public callback method for the Quit button.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Load the next scene in the hierarchy.<br></br>
    /// Used as callback from the Camera animation event.
    /// </summary>
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Callback method for updating the volume.
    /// </summary>
    public void UpdateVolume()
    {
        GlobalValues.GetInstance().volume = volumeSlider.value;
    }
}