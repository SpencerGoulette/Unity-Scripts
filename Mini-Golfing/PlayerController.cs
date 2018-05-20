using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    // Player's body and camera
    private Rigidbody playerBody;
    public GameObject cameraObject;

    // Speed variables
    public float movementSpeed = 10.0f;
    public float speed = 4.0f;
    private float moveVertical = 0.0f;
    private float drag = 500;

    // Scoring variables
    private float score = 0.0f;
    private int strokes = 0;
    private string[] scoreCard = {"  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  "};
    public int par = 2;
    private int hole = 1;

    // Player spawns
    private Vector3 respawnVector;

    // Texts
    public Text scoreboard;
    public Text win;
    public Text stroke;

    // Status variables
    private bool showScore = true;
    private bool swinging = false;

    // Code that runs at the start of the game
    private void Start()
    {
        // Sets up the game
        playerBody = GetComponent<Rigidbody>();
        score = 0;
        SetPower();
        SetStroke();
        win.text = "";
        HoleSelection();
        playerBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        drag = 1 - playerBody.velocity.x + 1 - playerBody.velocity.z;
        if(drag < 0)
        {
            drag = 0.2f;
        }

        playerBody.drag = drag;

        // Reset position
        if (Input.GetKeyDown("r"))
        {
            ResetPosition();
        }

        // Checks scoreboard
        if (Input.GetKeyDown("tab"))
        {
            DisplayScore();
        }

        // Checks for completed stroke
        if (playerBody.velocity.x < 1 && playerBody.velocity.z < 1)
        {
            float moveHorizontal = 0.0f;
            if (Input.GetMouseButton(0) == true)
            {
                score -= Input.GetAxis("Mouse Y");
                if(score > 100)
                {
                    score = 100;
                }
                SetPower();
                swinging = true;
            }

            // A stroke
            else if (Input.GetMouseButton(0) == false)
            {
                if (swinging)
                {
                    playerBody.drag = 0.2f;
                    moveVertical = movementSpeed * score;
                    float horizontal = moveVertical * (transform.position.x - cameraObject.transform.position.x) + moveHorizontal * (transform.position.z - cameraObject.transform.position.z);
                    float vertical = moveVertical * (transform.position.z - cameraObject.transform.position.z) + -moveHorizontal * (transform.position.x - cameraObject.transform.position.x);

                    Vector3 movement = new Vector3(horizontal, 0.0f, vertical);

                    playerBody.AddForce(movement * speed);
                    swinging = false;
                    score = 0;
                    strokes++;
                    SetStroke();
                }
            }
        }
    }

    //Trigger Event
    void OnTriggerEnter(Collider other)
    {
        // Checks to see if ball is in the hole and gives score
        if(other.gameObject.CompareTag("Hole"))
        {
            other.gameObject.SetActive(false);
            scoreCard[hole - 1] = strokes.ToString();
            strokes = strokes - par + 5;
            if (strokes == 1)
            {
                win.text = "Hole-In-One";
            }
            else
            {
                switch (strokes)
                {
                    case 1:
                        win.text = "Condor";
                        break;
                    case 2:
                        win.text = "Albatross";
                        break;
                    case 3:
                        win.text = "Eagle";
                        break;
                    case 4:
                        win.text = "Birdie";
                        break;
                    case 5:
                        win.text = "Par";
                        break;
                    case 6:
                        win.text = "Bogey";
                        break;
                    case 7:
                        win.text = "Double Bogey";
                        break;
                }
            }

            DisplayScore();

            // Places ball at the next hole
            hole++;
            HoleSelection();
        }
    }

    // Shows power of hit
    void SetPower()
    {
        scoreboard.text = "Power: " + score.ToString();
    }

    // Shows number of strokes
    void SetStroke()
    {
        stroke.text = "Strokes: " + strokes.ToString();
    }

    // Resets player and stops velocity
    void ResetPosition()
    {
        playerBody.transform.position = respawnVector;
        playerBody.drag = 500;
    }

    // Selects hole in the course
    void HoleSelection()
    {
        switch(hole)
        {
            case 1:
                respawnVector = new Vector3(0.0f, 0.5f, 0.0f);
                HoleReset();
                par = 2;
                break;
            case 2:
                respawnVector = new Vector3(45.0f, 0.5f, 0.0f);
                HoleReset();
                par = 3;
                break;
            case 3:
                respawnVector = new Vector3(0.0f, 0.5f, 0.0f);
                HoleReset();
                break;
            case 4:
                respawnVector = new Vector3(0.0f, 0.5f, 0.0f);
                HoleReset();
                break;
            case 5:
                respawnVector = new Vector3(0.0f, 0.5f, 0.0f);
                HoleReset();
                break;
            case 6:
                respawnVector = new Vector3(0.0f, 0.5f, 0.0f);
                HoleReset();
                break;
            case 7:
                respawnVector = new Vector3(0.0f, 0.5f, 0.0f);
                HoleReset();
                break;
            case 8:
                respawnVector = new Vector3(0.0f, 0.5f, 0.0f);
                HoleReset();
                break;
            case 9:
                respawnVector = new Vector3(0.0f, 0.5f, 0.0f);
                HoleReset();
                break;
        }
    }

    // Displays the golf scoreboard
    void DisplayScore()
    {
        if (showScore)
        {
            scoreboard.text = "";
            stroke.text = "";
            win.text = "Holes [ 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 ] \n  Par   [ 2 | 3 | 3 | 4 | 3 | 5 | 3 | 4 | 5 ]  \n Score [ " + scoreCard[0] + " | " + scoreCard[1] + " | " + scoreCard[2] + " | " + scoreCard[3] + " | " + scoreCard[4] + " | " + scoreCard[5] + " | " + scoreCard[6] + " | " + scoreCard[7] + " | " + scoreCard[8] + " ] ";
            showScore = false;
        }
        else
        {
            SetPower();
            SetStroke();
            win.text = "";
            showScore = true;
        }
    }

    // Resets the hole
    void HoleReset()
    {
        ResetPosition();
        strokes = 0;
        SetStroke();
        win.text = "";
    }
}
