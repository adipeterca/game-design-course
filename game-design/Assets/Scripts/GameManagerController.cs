using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class GameManagerController : MonoBehaviour
{
    [Header("Pickup Variables")]

    // Public pickup reference
    public GameObject pickupReference;

    // Spawn points in which a pickup may spawn
    public GameObject[] pickupSpawnPoints;

    // The number of pickup objects to spawn at the start of the game
    private int pickupCount = -1;


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
        if (GlobalValues.GetInstance().volume == -1.0f)
        {
            Debug.LogWarning("GlobalValue volume set to -1.0f, dumbass!");
        }

        sounds = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < sounds.Length; i++)
            sounds[i].volume = GlobalValues.GetInstance().volume;
        soundsWasPlaying = new bool[sounds.Length];

        // Disable the end game light & collider at the start of the game
        endgameLight.SetActive(false);
        endgameCollider.SetActive(false);

        SetDifficulty();
        SpawnPrefabs(pickupReference, pickupSpawnPoints, pickupCount);

        // Bug fixing: the GUI starts before this script (I don't really know why) and, in order to make sure
        // the count number is correct, we call the update method one more time.
        GuiManagerController.Instance.UpdateCountText();
    }

    private void Update()
    {
        if (gameOver) return;

        // The user pressed the ESC key
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
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
    /// Also for the 'Back to main menu' button.
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
            GuiManagerController.Instance.DisplayEndgameText();
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
    /// Public getter for the number of pickups.
    /// </summary>
    /// <returns>the number of pickups</returns>
    public int GetPickupCount()
    {
        return pickupCount;
    }

    /// <summary>
    /// Public method which adjusts the game settings to fit a certain difficulty.<br></br>
    /// Difficulty varies from 1 to 3, 1 meaning easy and 3 meaning hard.
    /// </summary>
    public void SetDifficulty()
    {
        if (GlobalValues.GetInstance().difficulty == 1)
        {
            pickupCount = 4;
            EnemyModelController.Instance.respawnTime = 30;
            EnemyModelController.Instance.GetAgent().speed = 3;
            EnemyModelController.Instance.GetAgent().acceleration = 4;
        }
        else if (GlobalValues.GetInstance().difficulty == 2)
        {
            pickupCount = 7;
            EnemyModelController.Instance.respawnTime = 45;
            EnemyModelController.Instance.GetAgent().speed = 4;
            EnemyModelController.Instance.GetAgent().acceleration = 6;
        }
        else
        {
            pickupCount = 10;
            EnemyModelController.Instance.respawnTime = 60;
            EnemyModelController.Instance.GetAgent().speed = 5;
            EnemyModelController.Instance.GetAgent().acceleration = 8;
        }
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
