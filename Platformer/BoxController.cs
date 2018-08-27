using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour {

    // Grabs proper items to use
    private Rigidbody boxBody;
    public GameObject player;
    private PlayerController playerController;

    public Vector3 respawnPoint;

    // Grabs script and box's body
    void Start () {
        boxBody = GetComponent<Rigidbody>();
        playerController = player.GetComponent<PlayerController>();
    }
	
	// Spawn points for box depending on antigravity
	void FixedUpdate () {
		if(boxBody.transform.position.y < -80 || boxBody.transform.position.y > 120)
        {
            if (playerController.AntiGravity == false)
            {
                boxBody.transform.position = respawnPoint;
                boxBody.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            }
            else if(playerController.AntiGravity == true)
            {
                boxBody.transform.position = new Vector3(17.0f, 42.0f, -20.0f);
                boxBody.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.E))
            {

            }
        }
    }
}
