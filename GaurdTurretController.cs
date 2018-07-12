using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaurdTurretController : MonoBehaviour {

    private float originalPositionY;
    private bool inZone;
    public float fireRate = 0.5f;
    private float time;
    public GameObject turret;
    public GameObject player;
    public GameObject laser;
    public ParticleSystem readyShot;

	// Use this for initialization
	void Start () {
        originalPositionY = turret.transform.position.y;
        time = 0.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        turret.transform.LookAt(player.transform);

        if (inZone)
        {
            if(turret.transform.position.y < (originalPositionY + 1.5f))
            {
                turret.transform.Translate(new Vector3(0.0f, 1.0f, 0.0f) * Time.deltaTime);
            }

            if (turret.transform.position.y > (originalPositionY + 1.0f))
            {
                time += Time.deltaTime;
                if (time >= fireRate)
                {
                    time = 0.0f;
                    readyShot.Play();
                    Instantiate(laser, turret.transform.position + new Vector3(0.0f, 0.5f, 0.0f), turret.transform.rotation);
                }
            }
        }

        else
        {
            if(turret.transform.position.y > originalPositionY)
            {
                turret.transform.Translate(new Vector3(0.0f, 1.0f, 0.0f) * Time.deltaTime * -1.0f);
                readyShot.Stop();
            }
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            inZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            inZone = false;
        }
    }
}
