using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // The player
    public GameObject player;

    // Speeds of Camera rotation and radius change
    public float yawSpeed = 2.0f;
    public float pitchSpeed = 2.0f;
    public float radiusSpeed = 5.0f;

    // Rotation, radius, and position of the camera
    private float radius = 10.0f;
    private float yaw = 0.0f;
    private float pitch = 35.0f;
    private float cameraX = 0.0f;
    private float cameraY = 10.0f;
    private float cameraZ = 0.0f;

    void FixedUpdate()
    {

        // Obtains rotation from mouse movement
        yaw -= yawSpeed * Input.GetAxis("Horizontal");
        pitch += pitchSpeed * Input.GetAxis("Vertical");

        // Changes radius based off of mouse's scrollwheel
        radius += radiusSpeed * Input.GetAxis("Mouse ScrollWheel");

        // Sets limits for the radius
        if (radius < 0)
        {
            radius = 0;
        }
        else if (radius > 25)
        {
            radius = 25;
        }

        // Calculates the position of the camera based off the radius, and two angles of rotation
        cameraY = radius * Mathf.Sin(pitch / 57.3f);
        cameraX = -radius * Mathf.Sin(yaw / 57.3f) * Mathf.Cos(pitch / 57.3f);
        cameraZ = -radius * Mathf.Cos(yaw / 57.3f) * Mathf.Cos(pitch / 57.3f);

        // Limits camera rotation to top hemisphere
        if (cameraY > 0)
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
