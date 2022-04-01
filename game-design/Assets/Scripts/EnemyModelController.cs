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
    // A list of positions at which the enemy can teleport to
    public Transform[] respawnPositions;


    // The NavMeshAgent reference which is used to move the Enemy to the Player
    private NavMeshAgent agent;

    // At which interval to move the enemy to a different, random location (in seconds)
    private float respawnTime = 60;

    // The search distance for the Player for each respawn point
    private int distance = 30;

    private float passedTime = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Teleport the enemy to a random location on the map
        transform.position = GetRespawnPoint().position;
    }

    void Update()
    {
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
}
