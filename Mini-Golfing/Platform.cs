using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    // Platform variables
    private Rigidbody platformBody;
    public float platformSpeed = 4;
    private bool platformMoving = false;

    // Gets the platform's body
    void Start () {
        platformBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        // Makes sure that the platform is bounded
        if(platformBody.transform.position.y < 51 && platformBody.transform.position.y > -0.12 && (platformMoving == true))
        {
            platformBody.transform.Translate(Vector3.up * platformSpeed * Time.deltaTime);
        }
        if(platformBody.transform.position.y < -0.1 || platformBody.transform.position.y > 50)
        {
            platformMoving = false;
        }
    }

    // When ball enters, lifts the platform
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ball"))
        {
            platformSpeed = 4;
            platformMoving = true;
        }
    }

    // When ball exits, lowers the platform
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            platformSpeed = -4;
            platformMoving = true;
        }
    }
}
