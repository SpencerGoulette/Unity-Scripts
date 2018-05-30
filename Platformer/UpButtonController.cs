using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpButtonController : MonoBehaviour {

    public GameObject platform;
    public GameObject platform2;
    public GameObject button;
    public GameObject player;

    public float platformSpeed = 4.0f;
    public float moveForward = 1.0f;
    public float buttonSpeed = 1.0f;

    private bool onPlatform = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (platform.transform.position.z < 40)
        {
            if (onPlatform)
            {
                player.transform.Translate(new Vector3(0.0f, 0.0f, 1.0f) * platformSpeed * moveForward * Time.deltaTime);
            }
        }

        if (button.transform.position.y < 0.5)
        {
            button.transform.Translate(new Vector3(0.0f, 0.01f, 0.0f) * Time.deltaTime * buttonSpeed);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            if (button.transform.position.y > 0.32)
            {
                button.transform.Translate(new Vector3(0.0f, -0.01f, 0.0f) * Time.deltaTime * buttonSpeed);
            }

            platform.transform.Translate(new Vector3(0.0f, -1.0f, 0.0f) * platformSpeed * moveForward * Time.deltaTime);
            platform2.transform.Translate(new Vector3(0.0f, 1.0f, 0.0f) * platformSpeed * moveForward * Time.deltaTime);

            if (platform.transform.position.y < 17)
            {
                moveForward = -1.0f;
            }

            if (platform.transform.position.y > 45)
            {
                moveForward = 1.0f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
        {
            moveForward = -1.0f;
        }
    }
}
