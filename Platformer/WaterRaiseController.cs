using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRaiseController : MonoBehaviour {

    public float raiseHeight = 0.0f;
    public float raiseSpeed = 5.0f;

    private bool isTriggered = false;

    public GameObject hidden;
    public GameObject raise;

	// Use this for initialization
	void Start () {
        hidden.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(isTriggered)
        {
            if (raise.transform.position.y < raiseHeight)
            {
                raise.transform.Translate(new Vector3(0.0f, 1.0f, 0.0f) * raiseSpeed);
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            hidden.gameObject.SetActive(true);
            isTriggered = true;
        }
    }
}
