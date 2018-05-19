using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    // Player's body
    private Rigidbody playerBody;
    public GameObject cameraObject;
    private int score = 0;
    public Text scoreboard;
    public Text win;
    public float speed = 4.0f;

    //Code that runs at the start of the game
    private void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        score = 0;
        SetScoreboard();
        win.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        float horizontal = moveVertical * (transform.position.x - cameraObject.transform.position.x) + moveHorizontal * (transform.position.z - cameraObject.transform.position.z);
        float vertical = moveVertical * (transform.position.z - cameraObject.transform.position.z) + -moveHorizontal * (transform.position.x - cameraObject.transform.position.x);

        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);

        playerBody.AddForce(movement * speed);
    }

    //Trigger Event
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            score++;
            SetScoreboard();

            //Checks to see if all 8 boxes have been collected
            if(score >= 8)
            {
                win.text = "You Win!";
            }
        }
    }

    void SetScoreboard()
    {
        scoreboard.text = "Score: " + score.ToString();
    }
}
