using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    //Which layer the Raycast will hit (Currently set to ground layer).
    public LayerMask groundCheckLayer = 8;

    //Declares rigidbody.
    private Rigidbody rb;
    //Delares an array of trail renderers (2).
    private TrailRenderer[] trailRend = new TrailRenderer[2];
    //Declares the players move speed.
    private float moveSpeed = 20;
    //Declares the speed which the player will rotate at when mpving in a different direction.
    private float rotationSpeed = 20;

	void Start ()
    {
        //Defines rigidbody.
        rb = GetComponent<Rigidbody>();
        //Defines both trail renderers.
        trailRend[0] = transform.Find("Trail[0]").GetComponent<TrailRenderer>();
        trailRend[1] = transform.Find("Trail[1]").GetComponent<TrailRenderer>();
        //Trail renderer null check.
        if (trailRend[0] == null || trailRend[1] == null)
        {
            Debug.Log("Trail Renderer not found");
        }
	}
	
	void Update ()
    {
        Drive();
        GroundCheck();
	}

    //Moves player.
    void Drive()
    {
        //Defines a movement vector.
        Vector3 groundMovement = new Vector3(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
        //Normalizes vector so that the player can not move faster diagonally.
        Vector3.Normalize(groundMovement);

        //moves position.
        transform.position += groundMovement;

        //Checks if there is no movement input
        if (groundMovement != Vector3.zero)
        {
            //rotates towards movement direction.
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(groundMovement), rotationSpeed * Time.deltaTime);
        }
    }

    //Checks if player is on ground.
    bool GroundCheck()
    {
        //Declares a hit.
        RaycastHit hit;

        //Draws the raycast so that I'm not blind while guessing the length.
        Debug.DrawRay(transform.position, -Vector3.up * 0.5f, Color.green, 0.2f);

        //Checks if raycast has hit anything.
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 0.5f))
        {
            Debug.Log("Floor was hit.");

            //if either of the trail renderers are not enabled.
            if (trailRend[0].enabled == false || trailRend[1].enabled == false)
            {
                //Enable both trail renderers in a for loop.
                for (int i = 0; i < trailRend.Length; i++)
                {
                    //Enable both.
                    trailRend[i].enabled = true;
                }
            }

            //returns true informing that the ray did infact hit.
            return true;
        }
        //If the raycast did not hit anything.
        else
        {
            //Disable both of the trail renderers in a for loop.
            for (int i = 0; i < trailRend.Length; i++)
            {
                //Disable current renderer.
                trailRend[i].enabled = false;
            }

            //returns false informing that the ray did not hit.
            return false;
        }
    }
}
