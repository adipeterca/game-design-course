using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Singleton class responsible with handling all GUI updates.<br></br>
/// </summary>
public class GuiManagerController : MonoBehaviour
{
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


    /// <summary>
    /// Public method used for simulating a jumpscare effect on the whole screen.
    /// </summary>
    public void Jumpscare()
    {
        jumpscareImage.GetComponent<Animator>().SetTrigger("JumpscareTrigger");
        jumpscareImage.GetComponent<AudioSource>().Play();
    }
}
