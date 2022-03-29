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
