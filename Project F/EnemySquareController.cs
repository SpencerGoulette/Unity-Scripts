using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySquareController : MonoBehaviour {

    public GameObject player;

    public GameObject enemy;

    public GameObject heart;
    public int heartDrop;
    private bool heartDropped = false;

    public float movementSpeed = 1.0f;
    private float originalHeight = 0.0f;

	// Use this for initialization
	void Start () {
        originalHeight = transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.LookAt(player.transform);
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            heartDrop = Random.Range(1, 4);
            if(heartDrop == 2 && heartDropped == false)
            {
                Instantiate(heart, enemy.transform.position, enemy.transform.rotation);
            }
            heartDropped = true;
            Destroy(enemy, 0.1f);
        }
    }
}
