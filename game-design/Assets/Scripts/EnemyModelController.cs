using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Script that controls the Enemey of the game.<br></br>
/// The enemy always follows the Player (meaning that it always goes straight to the Player's position),
/// but it also teleports to a random location (in which the player is not present within a given number of units).
/// </summary>
public class EnemyModelController : MonoBehaviour
{
    private EnemyModelController() { }
    public static EnemyModelController Instance
    {
        get;
        private set;
    }

    // A list of positions at which the enemy can teleport to
    public Transform[] respawnPositions;

    // At which interval to move the enemy to a different, random location (in seconds)
    [HideInInspector]
    public float respawnTime;


    // The NavMeshAgent reference which is used to move the Enemy to the Player
    private NavMeshAgent agent;

    // The search distance for the Player for each respawn point
    private int distance = 30;

    // Passed time for this script - used for respawn time
    private float passedTime = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(Instance);
        else
            Instance = this;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Teleport the enemy to a random location on the map
        transform.position = GetRespawnPoint().position;
    }

    void Update()
    {
        if (GameManagerController.Instance.gameOver)
        {
            // Slowly decrease the audio
            GetComponent<AudioSource>().volume -= Time.deltaTime;
            return;
        }

        agent.SetDestination(PlayerModelController.Instance.gameObject.transform.position);

        // Increase the time
        passedTime += Time.deltaTime;
        if (passedTime >= respawnTime)
        {
            transform.position = GetRespawnPoint().position;
            passedTime = 0;
        }
    }

    /// <summary>
    /// Private method which returns an adequate point (from the given list) at which to respawn the Enemy.<br></br>
    /// The chosen point must not be nearer that DISTANCE units from the Player.
    /// </summary>
    /// <returns>the respawn point as a Transform</returns>
    private Transform GetRespawnPoint()
    {
        Transform result;
        Transform player = PlayerModelController.Instance.gameObject.transform;

        int pos;
        while (true)
        {
            pos = Random.Range(0, respawnPositions.Length);
            result = respawnPositions[pos];

            // Is the player far enough?
            if ((result.position.x + distance <  player.position.x || result.position.x - distance > player.position.x) &&
                (result.position.z + distance < player.position.z || result.position.z - distance > player.position.z))
            {
                break;
            }
        }

        return result;
    }

    /// <summary>
    /// Returns the NavMeshAgent of the Enemy.
    /// </summary>
    /// <returns>the NavMeshAgent of the Enemy</returns>
    public NavMeshAgent GetAgent()
    {
        return agent;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Did the Enemy collide with the Player?
        if (collision.gameObject.CompareTag("Player"))
        {
            GuiManagerController.Instance.DisplayEndgameMenu(0);
            GameManagerController.Instance.gameOver = true;
        }
    }
}
