using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Player's body
    private Rigidbody playerBody;

    // Camera
    public GameObject cameraObject;

    // Time
    public TimeManager timeManager;

    // Settings Menu
    public GameObject settings;

    // Speed of player
    public float movementSpeed = 1.0f;
    public float jumpSpeed = 4.0f;
    public float climbingSpeed = 50.0f;

    // Activity Variables
    private bool grounded = true;
    private bool climbing = false;
    public bool AntiGravity = false;
    private Vector3 movement;

    // Gravity toggle variable
    private float toggleGravity = 1.0f;

    // Code that runs at the start of the game
    private void Start()
    {
        // Initializes Player
        playerBody = GetComponent<Rigidbody>();
        playerBody.transform.position = new Vector3(0.0f, 2.0f, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Stops player rotation and falling
        playerBody.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

        // Spawn points depending on antigravity
        if (playerBody.transform.position.y < -30)
        {
            playerBody.transform.position = new Vector3(0.0f, 1.5f, 0.0f);
        }

        if (playerBody.transform.position.y > 120)
        {
            playerBody.transform.position = new Vector3(-4.0f, 78.5f, 50.0f);
        }

        if (!(settings.activeSelf))
        {
            // Climbing and Antigravity cases
            // Climbing Normally
            if (climbing == true && AntiGravity == false)
            {
                Physics.gravity = new Vector3(0.0f, 0.0f, 0.0f);

                float moveVertical = Input.GetAxis("Vertical");

                movement = new Vector3(0.0f, moveVertical, 0.0f);

                playerBody.transform.Translate(movement * climbingSpeed);
            }

            // Climbing With AntiGravity
            if (climbing == true && AntiGravity == true)
            {
                Physics.gravity = new Vector3(0.0f, 0.0f, 0.0f);

                float moveVertical = Input.GetAxis("Vertical");

                movement = new Vector3(0.0f, moveVertical, 0.0f);

                playerBody.transform.Translate(-movement * climbingSpeed);
            }

            // No climbing and No Antigravity
            if (climbing == false && AntiGravity == false)
            {
                Physics.gravity = new Vector3(0.0f, -30.0f, 0.0f);
            }

            // No climbing and AntiGravity
            if (climbing == false && AntiGravity == true)
            {
                Physics.gravity = new Vector3(0.0f, 30.0f, 0.0f);
            }

            // If Moving:
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                // Allows for easier movement
                playerBody.drag = 0f;

                // Obtains movement from "movement" keys
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");

                // Applies the calculated force onto the player with antigravity
                if (AntiGravity)
                {
                    float horizontal = moveVertical * Mathf.Sin(-cameraObject.transform.eulerAngles.y / 57.3f) + moveHorizontal * Mathf.Cos(-cameraObject.transform.eulerAngles.y / 57.3f);
                    float vertical = moveVertical * Mathf.Cos(cameraObject.transform.eulerAngles.y / 57.3f) + moveHorizontal * Mathf.Sin(cameraObject.transform.eulerAngles.y / 57.3f);

                    movement = new Vector3(-horizontal, 0.0f, vertical);
                }

                // Applies the calculated force onto the player without antigravity
                else if (AntiGravity == false)
                {
                    float horizontal = moveVertical * Mathf.Sin(cameraObject.transform.eulerAngles.y / 57.3f) + moveHorizontal * Mathf.Cos(cameraObject.transform.eulerAngles.y / 57.3f);
                    float vertical = moveVertical * Mathf.Cos(cameraObject.transform.eulerAngles.y / 57.3f) + -moveHorizontal * Mathf.Sin(cameraObject.transform.eulerAngles.y / 57.3f);

                    movement = new Vector3(horizontal, 0.0f, vertical);
                }

                // Gets rid of strafe jumps and increased speeds in the diagonals
                if (moveHorizontal != 0 && moveVertical != 0)
                {
                    playerBody.transform.Translate(movement * movementSpeed / 1.4f);
                }

                // If not going diagonal, then normal movement
                else
                {
                    playerBody.transform.Translate(movement * movementSpeed);
                }
            }

            // If moving, then apply drag
            if (Input.GetAxis("Horizontal") == 0)
            {
                playerBody.drag = 2;
            }

            if (Input.GetAxis("Vertical") == 0)
            {
                playerBody.drag = 2;
            }

            // Jumps if spacebar is pressed and player is on the ground
            if (grounded == true && Input.GetKey("space"))
            {
                //timeManager.SlowMotion();
                // Flips jump on antigravity
                if (AntiGravity)
                {
                    playerBody.AddForce(jumpSpeed * new Vector3(0.0f, -100.0f, 0.0f));
                }

                // Normal jumping
                else
                {
                    playerBody.AddForce(jumpSpeed * new Vector3(0.0f, 100.0f, 0.0f));
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Sees if the player is grounded
        if(other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            //timeManager.NormalMotion();
        }

        // Toggles antigravity
        if (other.gameObject.CompareTag("AntiGravity"))
        {
            if (toggleGravity == 1.0f)
            {
                AntiGravity = true;
            }

            else if (toggleGravity == 0.0f)
            {
                AntiGravity = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Sees if the player isn't grounded anymore
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }

        // Sees if the player isn't climbing
        if (other.gameObject.CompareTag("Ladder"))
        {
            climbing = false;
        }

        if (other.gameObject.CompareTag("AntiGravity"))
        {
            if (AntiGravity == true)
            {
                toggleGravity = 0.0f;
            }

            else if (AntiGravity == false)
            {
                toggleGravity = 1.0f;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Sees if the player is climbing
        if (other.gameObject.CompareTag("Ladder"))
        {
            climbing = true;
        }
    }
}
