using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartRotator : MonoBehaviour {
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.eulerAngles += new Vector3(0.0f, 1.0f, 0.0f);
	}
}
