using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    // Platform and player
    private Rigidbody platformBody;
    public GameObject player;

    // Platforms movement speed
    public float platformSpeed = 4.0f;

    // Limits so that platform boundaries may be customized
    public float upperLimitX = 0.0f;
    public float lowerLimitX = 0.0f;
    public float upperLimitZ = 0.0f;
    public float lowerLimitZ = 0.0f;
    public float upperLimitY = 0.0f;
    public float lowerLimitY = 0.0f;

    // Customizeable directional movement
    public bool moveInX = false;
    public bool moveInZ = true;
    public bool moveInY = false;

    // Platform movement variables
    private bool onPlatform = false;
    private float moveForward = 1.0f;

	// Gets the platform body
	void Start () {
        platformBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        // If moving in the X direction:
        if(moveInX)
        {
            // Move the platform in the X direction
            platformBody.transform.Translate(new Vector3(1.0f, 0.0f, 0.0f) * platformSpeed * moveForward * Time.deltaTime);

            // If the player is on the platform, then move the player with the platform
            if(onPlatform)
            {
                player.transform.Translate(new Vector3(1.0f, 0.0f, 0.0f) * platformSpeed * moveForward * Time.deltaTime);
            }

            // The bounds of the platform in the X direction
            if (platformBody.transform.position.x > upperLimitX)
            {
                moveForward = -1.0f;
            }

            if (platformBody.transform.position.x < lowerLimitX)
            {
                moveForward = 1.0f;
            }
        }

        // If moving in the Z direction: 
        if (moveInZ)
        {
            // Move the platform in the Z direction
            platformBody.transform.Translate(new Vector3(0.0f, 0.0f, 1.0f) * platformSpeed * moveForward * Time.deltaTime);

            // If the player is on the platform, then move the player with the platform
            if (onPlatform)
            {
                player.transform.Translate(new Vector3(0.0f, 0.0f, 1.0f) * platformSpeed * moveForward * Time.deltaTime);
            }

            // The bounds of the platform in the Z direction
            if (platformBody.transform.position.z > upperLimitZ)
            {
                moveForward = -1.0f;
            }

            if (platformBody.transform.position.z < lowerLimitZ)
            {
                moveForward = 1.0f;
            }
        }

        // If moving in the Y direction:
        if (moveInY)
        {
            // Move the platform in the Y direction
            platformBody.transform.Translate(new Vector3(0.0f, 1.0f, 0.0f) * platformSpeed * moveForward * Time.deltaTime);

            // If the player is on the platform, then move the player with the platform
            if (onPlatform)
            {
                player.transform.Translate(new Vector3(0.0f, 1.0f, 0.0f) * platformSpeed * moveForward * Time.deltaTime);
            }

            // The bounds of the platform in the Y direction
            if (platformBody.transform.position.y > upperLimitY)
            {
                moveForward = -1.0f;
            }

            if (platformBody.transform.position.y < lowerLimitY)
            {
                moveForward = 1.0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // When player on platform set onPlatform to true
        if(other.gameObject.CompareTag("Player"))
        {
            onPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When player isn't on platform set onPlatform to false
        if (other.gameObject.CompareTag("Player"))
        {
            onPlatform = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Box"))
        {
            // If box is on the platform, then move box with the platform
            if(moveInZ)
            {
                other.transform.Translate(new Vector3(0.0f, 0.0f, 1.0f) * platformSpeed * moveForward * Time.deltaTime);
            }
            
            if(moveInX)
            {
                other.transform.Translate(new Vector3(1.0f, 0.0f, 0.0f) * platformSpeed * moveForward * Time.deltaTime);
            }

            if(moveInY)
            {
                other.transform.Translate(new Vector3(1.0f, 0.0f, 0.0f) * platformSpeed * moveForward * Time.deltaTime);
            }
        }
    }
}
