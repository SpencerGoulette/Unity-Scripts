using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    private Rigidbody platformBody;
    public GameObject player;

    public float platformSpeed = 4.0f;

    public float upperLimitY = 0.0f;
    public float lowerLimitY = 0.0f;

    public bool moveInX = false;
    public bool moveInZ = true;
    public bool moveInY = false;
    private bool onPlatform = false;
    private float moveForward = 1.0f;

	// Use this for initialization
	void Start () {
        platformBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(moveInX)
        {
            platformBody.transform.Translate(new Vector3(1.0f, 0.0f, 0.0f) * platformSpeed * moveForward * Time.deltaTime);

            if(onPlatform)
            {
                player.transform.Translate(new Vector3(1.0f, 0.0f, 0.0f) * platformSpeed * moveForward * Time.deltaTime);
            }

            if (platformBody.transform.position.x > 10)
            {
                moveForward = -1.0f;
            }

            if (platformBody.transform.position.x < -5)
            {
                moveForward = 1.0f;
            }
        }

        if(moveInZ)
        {
            platformBody.transform.Translate(new Vector3(0.0f, 0.0f, 1.0f) * platformSpeed * moveForward * Time.deltaTime);

            if(onPlatform)
            {
                player.transform.Translate(new Vector3(0.0f, 0.0f, 1.0f) * platformSpeed * moveForward * Time.deltaTime);
            }

            if (platformBody.transform.position.z > 15)
            {
                moveForward = -1.0f;
            }

            if (platformBody.transform.position.z < 0)
            {
                moveForward = 1.0f;
            }
        }

        if (moveInY)
        {
            platformBody.transform.Translate(new Vector3(0.0f, 1.0f, 0.0f) * platformSpeed * moveForward * Time.deltaTime);

            if (onPlatform)
            {
                player.transform.Translate(new Vector3(0.0f, 1.0f, 0.0f) * platformSpeed * moveForward * Time.deltaTime);
            }

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
        if(other.gameObject.CompareTag("Player"))
        {
            onPlatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            onPlatform = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            if(moveInZ)
            {
                other.transform.Translate(new Vector3(0.0f, 0.0f, 1.0f) * platformSpeed * moveForward * Time.deltaTime);
            }
            
            if(moveInX)
            {
                other.transform.Translate(new Vector3(1.0f, 0.0f, 0.0f) * platformSpeed * moveForward * Time.deltaTime);
            }
        }
    }
}
