using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    // Player's body
    private Rigidbody playerBody;
    
    // Camera
    public GameObject cameraObject;
    
    // Speed of player
    public float speed = 4.0f;

    //Code that runs at the start of the game
    private void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Obtains movement with mouse
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        // Calculates how to move in the X and Z plane based off of the position of the player and the camera
        float horizontal = moveVertical * (transform.position.x - cameraObject.transform.position.x) + moveHorizontal * (transform.position.z - cameraObject.transform.position.z);
        float vertical = moveVertical * (transform.position.z - cameraObject.transform.position.z) + -moveHorizontal * (transform.position.x - cameraObject.transform.position.x);
        
        // Applies the calculated force onto the player
        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);

        playerBody.AddForce(movement * speed);
    }
}
