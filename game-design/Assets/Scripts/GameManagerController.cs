using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManagerController : MonoBehaviour
{

    // Static reference for the Singleton pattern
    private static GameManagerController instance = null;

    [Header("Pickup Variables")]

    // Public pickup reference
    public GameObject pickupReference;

    // The number of pickup objects to spawn at the start of the game
    [Range(1, 100)]
    public int pickupCount;

    // Spawn point in which a pickup may spawn
    public GameObject[] spawnPoints;


    // Score for the number of pickups collected
    private int playerScore = 0;


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
        SpawnPickups();
    }

    private void Update()
    {
        
    }

    /// <summary>
    /// Public method called on destruction of a pickup object.
    /// </summary>
    public void IncreaseScore()
    {
        playerScore++;
    }

    /// <summary>
    /// Private method which spawn pickups according to the given array of spawn points. <br></br>
    /// Spawns elements as long as there are spaces left. 
    /// If no more space is available (though this should be avoided), it does not raise an error. 
    /// </summary>
    private void SpawnPickups()
    {
        List<int> elements = new List<int>();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            elements.Add(i);
        }

        int pos;
        for (int i = 0; i < pickupCount && i < spawnPoints.Length; i++)
        {
            // Choose a random element from the list
            pos = Random.Range(0, elements.Count);

            // Spawn a pickup at the picked location
            Vector3 position = spawnPoints[elements[pos]].transform.position;
            Instantiate(pickupReference).transform.position = position;

            // Update the list (in order to not spawn two pickups at the same location)
            elements.Remove(pos);
        }

    }
}
