using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Player's body
    private Rigidbody playerBody;

    // Camera
    public GameObject cameraObject;

    // Speed of player
    public float movementSpeed = 4.0f;
    public float jumpSpeed = 4.0f;
    public float climbingSpeed = 50.0f;


    private bool grounded = true;
    private bool climbing = false;
    public bool AntiGravity = false;

    private float toggleGravity = 1.0f;
    private Vector3 movement;

    //Code that runs at the start of the game
    private void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        playerBody.transform.position = new Vector3(0.0f, 1.5f, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(playerBody.transform.position.y < -30)
        {
            playerBody.transform.position = new Vector3(0.0f, 1.5f, 0.0f);
        }

        if(playerBody.transform.position.y > 120)
        {
            playerBody.transform.position = new Vector3(-4.0f, 78.5f, 50.0f);
        }

        if(climbing == true && AntiGravity == false)
        {
            Physics.gravity = new Vector3(0.0f, 0.0f, 0.0f);

            float moveVertical = Input.GetAxis("Vertical");

            movement = new Vector3(0.0f, moveVertical, 0.0f);

            playerBody.transform.Translate(movement * climbingSpeed);
        }

        if (climbing == true && AntiGravity == true)
        {
            Physics.gravity = new Vector3(0.0f, 0.0f, 0.0f);

            float moveVertical = Input.GetAxis("Vertical");

            movement = new Vector3(0.0f, moveVertical, 0.0f);

            playerBody.transform.Translate(-movement * climbingSpeed);
        }

        if (climbing == false && AntiGravity == false)
        {
            Physics.gravity = new Vector3 (0.0f, -30.0f, 0.0f);
            toggleGravity = 1.0f;
        }

        if (climbing == false && AntiGravity == true)
        {
            Physics.gravity = new Vector3(0.0f, 30.0f, 0.0f);
            toggleGravity = 0.0f;
        }

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            playerBody.drag = 0f;
            // Obtains movement from "movement" keys
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            // Applies the calculated force onto the player
            if(AntiGravity)
            {
                float horizontal = moveVertical * Mathf.Sin(-cameraObject.transform.eulerAngles.y / 57.3f) + moveHorizontal * Mathf.Cos(-cameraObject.transform.eulerAngles.y / 57.3f);
                float vertical = moveVertical * Mathf.Cos(cameraObject.transform.eulerAngles.y / 57.3f) + moveHorizontal * Mathf.Sin(cameraObject.transform.eulerAngles.y / 57.3f);

                movement = new Vector3(-horizontal, 0.0f, vertical);
            }

            else if(AntiGravity == false)
            {
                float horizontal = moveVertical * Mathf.Sin(cameraObject.transform.eulerAngles.y / 57.3f) + moveHorizontal * Mathf.Cos(cameraObject.transform.eulerAngles.y / 57.3f);
                float vertical = moveVertical * Mathf.Cos(cameraObject.transform.eulerAngles.y / 57.3f) + -moveHorizontal * Mathf.Sin(cameraObject.transform.eulerAngles.y / 57.3f);

                movement = new Vector3(horizontal, 0.0f, vertical);
            }

            if (moveHorizontal != 0 && moveVertical != 0)
            {
                playerBody.AddForce(movement * movementSpeed / 4);
            }

            else
            {
                playerBody.AddForce(movement * movementSpeed);
            }
        }

        if(Input.GetAxis("Horizontal") == 0)
        {
            playerBody.drag = 2;
        }

        if (Input.GetAxis("Vertical") == 0)
        {
            playerBody.drag = 2;
        }

        playerBody.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

        if(grounded == true && Input.GetKey("space"))
        {
            if(AntiGravity)
            {
                playerBody.AddForce(jumpSpeed * new Vector3(0.0f, -100.0f, 0.0f));
            }
            else
            {
                playerBody.AddForce(jumpSpeed * new Vector3(0.0f, 100.0f, 0.0f));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }

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
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }

        if (other.gameObject.CompareTag("Ladder"))
        {
            climbing = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            climbing = true;
        }
    }
}
