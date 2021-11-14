using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* FallingPlatform: Class that manipulates platforms into falling */
public class FallingPlatform : MonoBehaviour
{

    public float platformFallDelay = 1.0f;
    public Rigidbody rb;
    float fallTimer;

    bool playerTouched;

    void Start()
    {
        fallTimer = 0;
        playerTouched = false;
        /* On initialization, grab platforms rigidbody component */
        rb = GetComponent<Rigidbody>();
        /* if there is a rigidbody component */
        if(rb != null)
        {
            /* set its kinematic setting to true, making it set in place */
            rb.isKinematic = true;
        }
    }

    void Update()
    {
            /* If the player has touched a falling platform */
            if(playerTouched)
            {
                /* increase fall timer by time passed since last loop */
                fallTimer += Time.deltaTime;
            }
            /* if the preset falltimer is greater or equal to the platforms fall delay */
            if(fallTimer >= platformFallDelay)
            {
                /* the player has not touched the platform */
                playerTouched = false;
                EnablePlatformPhysics();
            }
    }

    /* OnTriggerEnter(): Method handling player interactions with designated falling platforms */
    void OnTriggerEnter(Collider col)
    {
        /* if the player collides with the platform */
        if(col.tag == "Player")
        {
            /* set bool to true */
            playerTouched = true;
        }

    }

    /* EnablePlatformPhysics(): Method that sets the collided platform to fall */
    void EnablePlatformPhysics()
    {
        /* still check if the rigidbody component is not empty */
        if(rb != null)
        {
            /*if so set kinematic to false, causing platform to fall */
            rb.isKinematic = false;
        }
    }
}
