using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Camera and main menu controller.
/// </summary>
public class MainMenu : MonoBehaviour
{
    // Public reference to the volume slider
    public Slider volumeSlider;

    // Public reference for the underline of each button when it is selected
    public GameObject[] selectedButton;

    // Private reference to the camera Animator component
    private Animator cameraAnim;

    // Array containing all audio sources from the Main menu scene
    private AudioSource[] audios;

    private void Start()
    {
        cameraAnim = GetComponent<Animator>();
        audios = FindObjectsOfType<AudioSource>();

        // Set default value for volume
        if (GlobalValues.GetInstance().volume == GlobalValues.defaultVolume)
            UpdateVolume();
        else
        { 
            volumeSlider.value = GlobalValues.GetInstance().volume;
            SetAudio();
        }

        // Set default value for difficulty
        if (GlobalValues.GetInstance().difficulty == GlobalValues.defaultDifficulty)
        {
            ChangeDifficulty(2);
        }
        else
        {
            ChangeDifficulty(GlobalValues.GetInstance().difficulty);
        }
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
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
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
        SetAudio();
    }

    /// <summary>
    /// Private method which saves which difficulty was selected. Defaults to "Medium".
    /// </summary>
    public void ChangeDifficulty(int type)
    {
        GlobalValues.GetInstance().difficulty = type;
        for (int i = 0; i < selectedButton.Length; i++)
        {
            if (i + 1 == type)
                selectedButton[i].SetActive(true);
            else
                selectedButton[i].SetActive(false);
        }
    }

    /// <summary>
    /// Private method for updating the volume on all audios from the main menu scene.
    /// </summary>
    private void SetAudio()
    {
        for (int i = 0; i < audios.Length; i++)
            audios[i].volume = GlobalValues.GetInstance().volume;
    }
}