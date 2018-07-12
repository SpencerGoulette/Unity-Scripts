using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour {

    // Get the player
    public GameObject player;

    // Coordinates to teleport the player
    public float coordinateX = 0.0f;
    public float coordinateY = 0.0f;
    public float coordinateZ = 0.0f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            // Teleport the player
            player.transform.position = new Vector3(coordinateX, coordinateY, coordinateZ);
        }
    }
}
