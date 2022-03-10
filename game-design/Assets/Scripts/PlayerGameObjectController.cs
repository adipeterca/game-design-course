using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameObjectController : MonoBehaviour
{
    // Reference for the model
    public GameObject model;

    // Reference for the light source
    public GameObject lightSource;

    // Original Y value for the light source position
    private float originalY;

    // Start is called before the first frame update
    void Start()
    {
        originalY = lightSource.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        lightSource.transform.position = new Vector3(model.transform.position.x, originalY, model.transform.position.z);
    }
}
