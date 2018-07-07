using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour {

    public float movementSpeed = 1.0f;
    public GameObject laser;

	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        Destroy(laser, 3.0f);
    }
}
