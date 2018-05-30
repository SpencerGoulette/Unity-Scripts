using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

    // Get gameobjects to allow for button and platform movements
    public GameObject platform;
    public GameObject button;
    public GameObject player;

    // Movement variables
    public float platformSpeed = 4.0f;
    public float moveForward = 1.0f;
    public float buttonSpeed = 1.0f;

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

    // Button pressed animation variable
    private float originalPosition;
    private bool lowerButton = false;

    private void Start()
    {
        // Grabs the initial position of the button
        originalPosition = button.transform.position.y;
    }

    void FixedUpdate () {
        // Button raise animation
        if (button.transform.position.y < originalPosition && lowerButton == false)
        {
            button.transform.Translate(new Vector3(0.0f, 0.01f, 0.0f) * Time.deltaTime * buttonSpeed);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // If something is on the button:
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            // Button lower animation
            lowerButton = true;
            if (button.transform.position.y > originalPosition - 0.18)
            {
                button.transform.Translate(new Vector3(0.0f, -0.01f, 0.0f) * Time.deltaTime * buttonSpeed);
            }

            if (moveInX)
            {
                // Platform moves
                platform.transform.Translate(new Vector3(-1.0f, 0.0f, 0.0f) * platformSpeed * moveForward * Time.deltaTime);

                // Platform Boundaries
                if (platform.transform.position.x < lowerLimitX)
                {
                    moveForward = -1.0f;
                }

                if (platform.transform.position.x > upperLimitX)
                {
                    moveForward = 1.0f;
                }
            }

            if (moveInY)
            {
                // Platform moves
                platform.transform.Translate(new Vector3(0.0f, -1.0f, 0.0f) * platformSpeed * moveForward * Time.deltaTime);

                // Platform Boundaries
                if (platform.transform.position.y < lowerLimitY)
                {
                    moveForward = -1.0f;
                }

                if (platform.transform.position.y > upperLimitY)
                {
                    moveForward = 1.0f;
                }
            }

            if (moveInZ)
            {
                // Platform moves
                platform.transform.Translate(new Vector3(0.0f, 0.0f, -1.0f) * platformSpeed * moveForward * Time.deltaTime);

                // Platform Boundaries
                if (platform.transform.position.z < lowerLimitZ)
                {
                    moveForward = -1.0f;
                }

                if (platform.transform.position.z > upperLimitZ)
                {
                    moveForward = 1.0f;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If nothing is on the button
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            lowerButton = false;
            moveForward = -1.0f;
        }
    }
}
