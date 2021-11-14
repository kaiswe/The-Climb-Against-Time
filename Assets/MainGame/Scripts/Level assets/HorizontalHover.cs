using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* HorizontalHover class: Class that dictates the objects movement pattern into an up-and-down movement */
public class HorizontalHover : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    /*set initial platform speed*/
    public float platformSpeed = 2.0f;

    [Range(0.0f, 10.0f)]
    /*set initial distance the platform will travel*/
    public float platformDistance = 0.1f;

    /*platforms starting position*/
    private Vector3 initialPos;
    
    
    private float _timer = 0.0f;

    void Start()
    {
        /* set its initial position to its default location */
        initialPos = transform.position;
    }

    void Update()
    {
        /*every frame, transform this objects position*/
        _timer += Time.deltaTime;
        /* Consistently set the new position of the platform based on the preset distance to travel and speed
         * Since vector3.forward is being added to the initial position of the platform, that would dictate that the platform will be moving horizontally */
        transform.position = initialPos + Vector3.forward * Mathf.Sin(_timer * platformSpeed) * platformDistance;
    }
}
