using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* EnemyAI: Class that handles and provides customization with enemy AI */
public class EnemyAI : MonoBehaviour
{
    /* the preset range for the enemy to travel on patrol */
    public float range;

    /* The speed in which the enemy will move */
    public float patrolSpeed = 1.0f;
    /* float variable for manipulating the z position of the enemy */
    float zPos;
    void Start()
    {
        /* initialize its starting position from its current z coordinate */
        zPos = transform.position.z;
    }

 
    void Update()
    {
        /* On every frame, by accessing its initial position which is based on the z axis
        * calculate how far it will move, and its speed */
        float newzPos = zPos + Mathf.Sin(Time.time * patrolSpeed) * range;
        /* set enemy's new position on the z axis, leave x and y as is */
        transform.position = new Vector3(transform.position.x, transform.position.y, newzPos);
    }
}

