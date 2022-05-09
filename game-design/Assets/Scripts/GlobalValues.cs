using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton holder for global values.<br></br>
/// Usually useful when passing information from one scene to another.
/// </summary>
public class GlobalValues
{
    private GlobalValues() { }

    private static GlobalValues instance;

    // Volume set from the Options menu (defaults to -1.0f)
    public float volume = -1.0f;

    // Difficulty (defaults to 2 - Medium)
    public int difficulty = 2;

    /// <summary>
    /// Public method of getting the singleton instance.
    /// </summary>
    /// <returns>the singleton instance (lazy initialization)</returns>
    public static GlobalValues GetInstance()
    {
        if (instance == null)
            instance = new GlobalValues();
        return instance;
    }

}
