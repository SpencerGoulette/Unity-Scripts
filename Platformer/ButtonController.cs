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

    // Button pressed animation variable
    private float originalPosition;
    private bool lowerButton = false;

    // Variable for if the player is on the platform
    private bool onPlatform = false;

    private void Start()
    {
        // Grabs the initial position of the button
        originalPosition = button.transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if(platform.transform.position.z < 40)
        {
            platform.transform.Translate(new Vector3(0.0f, 0.0f, -1.0f) * platformSpeed * moveForward * Time.deltaTime);
            if (onPlatform)
            {
                player.transform.Translate(new Vector3(0.0f, 0.0f, 1.0f) * platformSpeed * moveForward * Time.deltaTime);
            }
        }

        if (button.transform.position.y < originalPosition && lowerButton == false)
        {
            button.transform.Translate(new Vector3(0.0f, 0.01f, 0.0f) * Time.deltaTime * buttonSpeed);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            lowerButton = true;
            if (button.transform.position.y > originalPosition - 0.18)
            {
                button.transform.Translate(new Vector3(0.0f, -0.01f, 0.0f) * Time.deltaTime * buttonSpeed);
            }

            platform.transform.Translate(new Vector3(0.0f, 0.0f, -1.0f) * platformSpeed * moveForward * Time.deltaTime / 4);

            if (platform.transform.position.z < 0)
            {
                moveForward = -1.0f;
            }

            if (platform.transform.position.z > 40)
            {
                moveForward = 1.0f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            lowerButton = false;
            moveForward = -1.0f;
        }
    }
}
