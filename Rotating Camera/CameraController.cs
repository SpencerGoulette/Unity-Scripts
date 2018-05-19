using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;

    public float yawSpeed = 2.0f;
    public float pitchSpeed = 2.0f;
    public float radiusSpeed = 5.0f;

    private float radius = 10.0f;
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float cameraX = 0.0f;
    private float cameraY = 10.0f;
    private float cameraZ = 0.0f;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void FixedUpdate()
    {
        yaw += yawSpeed * Input.GetAxis("Mouse X");
        pitch -= pitchSpeed * Input.GetAxis("Mouse Y");

        radius += radiusSpeed * Input.GetAxis("Mouse ScrollWheel");
        if(radius < 0)
        {
            radius = 0;
        }
        else if(radius > 25)
        {
            radius = 25;
        }

        cameraY = radius * Mathf.Sin(pitch / 57.3f);
        cameraX = -radius * Mathf.Sin(yaw / 57.3f) * Mathf.Cos(pitch / 57.3f);
        cameraZ = -radius * Mathf.Cos(yaw / 57.3f) * Mathf.Cos(pitch / 57.3f);
        
        // Dampen towards the target rotation
        if(cameraY > 0)
        {
            transform.position = player.transform.position + new Vector3(cameraX, cameraY, cameraZ);
        }
        else
        {
            transform.position = player.transform.position + new Vector3(cameraX, 0, cameraZ);
        }
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
