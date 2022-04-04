using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManagerController : MonoBehaviour
{
    [Header("Pickup Variables")]

    // Public pickup reference
    public GameObject pickupReference;

    // The number of pickup objects to spawn at the start of the game
    [Range(1, 10)]
    public int pickupCount;

    // Spawn points in which a pickup may spawn
    public GameObject[] pickupSpawnPoints;

    [Header("End Game")]
    // The light which will be displayed once all pickup objects are collected
    public GameObject endgameLight;

    // The collider which will register the Player as exiting the level
    public GameObject endgameCollider;

    [HideInInspector]
    public bool gameOver = false;

    // Score for the number of pickups collected
    private int playerScore = 0;

    // List of all audio sources available
    AudioSource[] sounds;

    // List of all audio sources that were playing a sound before the game was paused
    bool[] soundsWasPlaying;


    /// <summary>
    /// Public static method for retrieving the only instance of the GameManagerController-er.
    /// </summary>
    public static GameManagerController Instance
    {
        get;
        private set;
    }

    // Don't bother, taken from here
    // https://gamedev.stackexchange.com/questions/116009/in-unity-how-do-i-correctly-implement-the-singleton-pattern
    private void Awake()
    {
        // Make sure that there is always only one reference for the singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SpawnPrefabs(pickupReference, pickupSpawnPoints, pickupCount);

        sounds = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < sounds.Length; i++)
            sounds[i].volume = GlobalValues.GetInstance().volume / 100.0f;
        soundsWasPlaying = new bool[sounds.Length];

        // Disable the end game light & collider at the start of the game
        endgameLight.SetActive(false);
        endgameCollider.SetActive(false);
    }

    private void Update()
    {
        if (gameOver) return;

        // The user pressed the ESC key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GuiManagerController.Instance.optionsMenu.activeSelf)
            {
                // Stop the game
                Time.timeScale = 0;

                GuiManagerController.Instance.optionsMenu.SetActive(true);

                // Pause all the sounds
                for (int i = 0; i < sounds.Length; i++)
                {
                    if (sounds[i].isPlaying)
                    {
                        sounds[i].Pause();
                        soundsWasPlaying[i] = true;
                    }
                }
            }
            else
            {
                Continue();
            }
        }
    }

    /// <summary>
    /// Callback for the 'Continue' button.
    /// It resumes the game in its actual state.
    /// </summary>
    public void Continue()
    {
        // Start the game
        Time.timeScale = 1;

        GuiManagerController.Instance.optionsMenu.SetActive(false);

        // Resume all sounds
        AudioSource[] sounds = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < sounds.Length; i++)
            if (soundsWasPlaying[i])
                sounds[i].Play();
    }

    /// <summary>
    /// Callback for the 'Quit' button.
    /// It exists the current scene (1) and returns the user to the MainMenu scene (0).
    /// </summary>
    public void Quit()
    {
        // Revert to the old time scale before reloading a new scene
        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Public method called on destruction of a pickup object.
    /// </summary>
    public void IncreaseScore()
    {
        playerScore++;
        
        // The player collected all pickups
        if (playerScore == pickupCount)
        {
            endgameLight.SetActive(true);
            endgameCollider.SetActive(true);
            GuiManagerController.Instance.DisplayEndGameText();
        }
        GuiManagerController.Instance.UpdateCountText();
    }

    /// <summary>
    /// Public getter for player score.
    /// </summary>
    /// <returns>the player's score</returns>
    public int GetScore()
    {
        return playerScore;
    }

    /// <summary>
    /// Private method used to spawn a given prefab over a given array of spawn points.<br></br>
    /// By default, all spawn points will be filled.
    /// </summary>
    /// <param name="objectToSpawn">reference to the object to spawn</param>
    /// <param name="spawnPoints">array of spawn points (usually empty GameObjects)</param>
    /// <param name="maxCount">maximum number of elements to spawn (-1 by default)</param>
    private void SpawnPrefabs(GameObject objectToSpawn, GameObject[] spawnPoints, int maxCount = -1)
    {
        List<int> elements = new List<int>();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            elements.Add(i);
        }

        int pos;
        for (int i = 0; i < maxCount && i < spawnPoints.Length; i++)
        {
            // Choose a random element from the list
            pos = Random.Range(0, elements.Count);

            // Spawn a pickup at the picked location
            Vector3 position = spawnPoints[elements[pos]].transform.position;
            Instantiate(objectToSpawn).transform.position = position;

            // Update the list (in order to not spawn two pickups at the same location)
            elements.Remove(elements[pos]);
        }
    }
}
