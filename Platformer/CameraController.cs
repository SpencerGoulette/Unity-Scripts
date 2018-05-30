using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // The player
    public GameObject player;
    private PlayerController playerController;

    // Speeds of Camera rotation and radius change
    public float yawSpeed = 2.0f;
    public float pitchSpeed = 2.0f;

    // Rotation, radius, and position of the camera
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float roll = 0.0f;
    private float cameraShift = 0.8f;
    private float antiGravityCamera = 1.0f;

    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        // Obtains rotation from mouse movement
        yaw -= yawSpeed * antiGravityCamera * Input.GetAxis("Mouse X");
        pitch += pitchSpeed * antiGravityCamera * Input.GetAxis("Mouse Y");

        if(playerController.AntiGravity == true && cameraShift > -0.8f)
        {
            cameraShift -= 0.05f;
            antiGravityCamera = -1.0f;
        }

        else if(playerController.AntiGravity == false && cameraShift < 0.8f)
        {
            cameraShift += 0.05f;
            antiGravityCamera = 1.0f;
        }

        // Limits camera rotation to top hemisphere
        transform.position = player.transform.position + new Vector3(0.0f, cameraShift, 0.0f);

        if (playerController.AntiGravity == true && roll < 180.0f)
        {
            roll += 1.0f;
        }

        else if(playerController.AntiGravity == false && roll > 0.0f)
        {
            roll -= 1.0f;
        }
        
        transform.eulerAngles = new Vector3(pitch, yaw, roll);
    }
}
