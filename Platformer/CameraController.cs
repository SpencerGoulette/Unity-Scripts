using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

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

    // Shifts when antigravity occurs
    private float cameraShift = 0.8f;
    private float antiGravityCamera = 1.0f;

    private bool noCutScene = false;
    private float waitTime = 0.0f;

    // SettingsMenu
    public GameObject Settings;

    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        Settings.SetActive(false);
        pitch = -90.0f;
        transform.eulerAngles = new Vector3(pitch, 0.0f, 0.0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Settings.SetActive(Settings.activeSelf ? false : true);
        }

        if (Settings.activeSelf)
        {
            Time.timeScale = 0;
        }

        if (!(Settings.activeSelf))
        {
            Time.timeScale = 1;
        }
    }

    void FixedUpdate()
    {
        if(!noCutScene)
        {
            waitTime += 0.01f;
            if (waitTime > 3.0f)
            {
                if (transform.rotation.x >= 0)
                {
                    noCutScene = true;
                }
                pitch += 0.1f;
                transform.eulerAngles = new Vector3(pitch, 0.0f, 0.0f);
                transform.position = player.transform.position + new Vector3(0.0f, cameraShift, 0.0f);
            }
        }

        if(!(Settings.activeSelf) && noCutScene)
        {

            if (pitch > 90)
            {
                pitch = 90;
            }

            if (pitch < -90)
            {
                pitch = -90;
            }

            // Obtains rotation from mouse movement
            yaw -= yawSpeed * antiGravityCamera * Input.GetAxis("Mouse X");
            pitch += pitchSpeed * antiGravityCamera * Input.GetAxis("Mouse Y");

            // If there is antigravity, then rotate camera
            if (playerController.AntiGravity == true && cameraShift > -0.8f)
            {
                cameraShift -= 0.05f;
                antiGravityCamera = -1.0f;
            }

            // If there isn't antigravity, then rotate camera
            else if (playerController.AntiGravity == false && cameraShift < 0.8f)
            {
                cameraShift += 0.05f;
                antiGravityCamera = 1.0f;
            }

            // Limits camera rotation to top hemisphere
            transform.position = player.transform.position + new Vector3(0.0f, cameraShift, 0.0f);

            // If there is antigravity, then rotate player
            if (playerController.AntiGravity == true && roll < 180.0f)
            {
                roll += 3.0f;
            }

            // If there isn't antigravity, then rotate player
            else if (playerController.AntiGravity == false && roll > 0.0f)
            {
                roll -= 3.0f;
            }

            // Applies camera rotations
            transform.eulerAngles = new Vector3(pitch, yaw, roll);
        }
    }

    public void ToggleInvertedControls(bool toggle)
    {
        if(toggle)
        {
            yawSpeed = 2.0f;
            pitchSpeed = 2.0f;
        }

        else
        {
            yawSpeed = -2.0f;
            pitchSpeed = -2.0f;
        }
    }
}
