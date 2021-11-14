using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Portal: Class that creates portals that the player can traverse through, while also manipulating reflection probes, textures, and materials to project realtime visuals of whats on the other side of the portal */
public class Portal : MonoBehaviour
{
    /* Camera attached to each portal that shows what is on this side of the portal */
    public Camera viewCam;
    /* Rendered texture for what each portal's designated camera shows */
    public RenderTexture rtex;
    /* Portal object that identifies the destination for the player on interaction */
    public Portal destination;
    /* The designated position of the player on destination arrival */
    public Transform arriveTransform;

    public CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        /* render the texture the portal will display on game runtime */
        rtex = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
        rtex.Create();
        GetComponent<Renderer>().material.mainTexture = rtex;
        /* if the destination is preset for this portal */
        if (destination != null)
        {
            /* set the destination of the portal to render the texture of the camera displaying on the other side */
            destination.SetCameraOutput(rtex);
        }
    }

    /* SetCameraOutput(): Displays what the desginated camera for the portal will display on game runtime */
    public void SetCameraOutput(RenderTexture r)
    {
        viewCam.targetTexture = r;
    }

    /* ArrivalPoint(): Returns the relative transform for the player on desination arrival */
    public Vector3 ArrivalPoint()
    {
        return arriveTransform.position;
    }

    /* ArrivalRotation(): Returns the relative rotation for the player on destination arrival */
    public Quaternion ArrivalRotation()
    {
        return arriveTransform.localRotation;
    }

    /* OnTriggerEnter(): Method that handles all interaction with the portals */
    void OnTriggerEnter(Collider col)
    {
        /* If the player interacts with a portal */
        if (col.tag == "Player")
        {
            /* and the destination said for said portal is not empty */
            if (destination != null)
            {
                /* disable the character controller so player can enter the portal */
                controller.enabled = false;
                /* set the collided game object, which is the player, postion and rotation to the preset arrival transform and rotation of the matching portal*/
                col.gameObject.transform.position = destination.ArrivalPoint();
                col.gameObject.transform.rotation = destination.ArrivalRotation();
            }
            /* re enable the controller once arrived at target destination */
            controller.enabled = true;
        }
    }
}
