using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySquareController : MonoBehaviour {

    public GameObject player;

    public GameObject enemy;

    public GameObject heart;
    public int heartDrop;

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            heartDrop = Random.Range(1, 3);
            if(heartDrop == 3)
            {
                Instantiate(heart, enemy.transform.position, enemy.transform.rotation);
            }
            Destroy(enemy, 3.0f);
        }
    }
}
