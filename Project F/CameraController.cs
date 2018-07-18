using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // The player
    public GameObject player;

    // Shifts when antigravity occurs
    private float cameraShift = 0.8f;

    // Speeds of Camera rotation and radius change
    public float yawSpeed = 2.0f;
    public float pitchSpeed = 2.0f;

    // Rotation, radius, and position of the camera
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        // Obtains rotation from mouse movement
        yaw -= yawSpeed * Input.GetAxis("Mouse X");
        pitch += pitchSpeed * Input.GetAxis("Mouse Y");

        if(pitch > 90)
        {
            pitch = 90;
        }

        if(pitch < -90)
        {
            pitch = -90;
        }

        transform.position = player.transform.position + new Vector3(0.0f, cameraShift, 0.0f);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
