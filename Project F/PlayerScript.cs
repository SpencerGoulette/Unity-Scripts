using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour {

    // Player's body
    private Rigidbody playerBody;

    // To check for respawn/reset
    public bool playerAlive = true;

    // Player's coins
    public float playerCoins = 0.0f;
    public TextMeshProUGUI coinText;

    // Player's inventory
    private bool hasCaboodle = false;
    public GameObject Inventory;

    // Player Message
    public TextMeshProUGUI playerMessage;

    // playerHealthMax, playerAttackMax, playerDefenseMax, playerSpeedMax, playerLuckMax, playerIntelligenceMax, playerSanityMax
    private int[] playerStatsMax = new int[] { 1, 1, 1, 1, 1, 1, 1 };
    public int[] playerStats = new int[] { 0, 1, 1, 1, 1, 1, 1 };

    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI playerStatsText;

    // Player's EXP
    public int playerLevel = 1;
    public int playerEXP = 0;
    private int addOn = 0;
    private int[] playerEXPLevelUp = new int[] { 2, 3, 5, 7, 9, 16, 25, 42, 66, 119, 173, 277, 453, 632, 871, 1186, 1659, 2311, 3105, 4468, 6224, 8129, 12410, 16562, 21282};
    
    // Camera
    public GameObject cameraObject;

    // Speed of player
    public float movementSpeed = 1.0f;
    public float jumpSpeed = 1.0f;

    // Player on Ground
    private bool grounded;

    // Movement vector
    private Vector3 movement;

    // Respawn vector
    private Vector3 respawnPoint = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start () {
        playerBody = GetComponent<Rigidbody>();
        Inventory.SetActive(false);
        PlayerRespawn();
        Physics.gravity = new Vector3(0.0f, -80.0f, 0.0f);
        playerHealthText.text = playerStats[0].ToString("000");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        playerBody.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

        for(int i = playerLevel - 1; i < playerLevel; i++)
        {
            if (playerEXP > playerEXPLevelUp[i])
            {
                playerLevel++;
                LevelUp();
                DisplayLevel();
            }
        }

        if(hasCaboodle && Input.GetKeyDown(KeyCode.E))
        {
            Inventory.SetActive(Inventory.activeSelf ? false : true);
            playerMessage.text = "";
        }

        if (!playerAlive)
        {
            PlayerRespawn();
        }

        if (playerAlive)
        {
            PlayerMovement();
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            playerCoins++;
            coinText.text = "$" + playerCoins.ToString("000");
        }

        if(other.gameObject.CompareTag("Caboodle"))
        {
            other.gameObject.SetActive(false);
            hasCaboodle = true;
            playerMessage.text = "PRESS [E] TO OPEN CABOODLE";
        }

        if(other.gameObject.CompareTag("Heart"))
        {
            for (int i = 0; i < 1; i++)
            {
                if (playerStats[i] < playerStatsMax[i])
                {
                    other.gameObject.SetActive(false);
                    playerStats[0]++;
                    playerHealthText.text = playerStats[0].ToString("000");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    public void Craft(string itemToCraft)
    {

    }

    public void PlayerRespawn()
    {

    }

    public void PlayerMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        float horizontal = moveVertical * Mathf.Sin(cameraObject.transform.eulerAngles.y / 57.3f) + moveHorizontal * Mathf.Cos(cameraObject.transform.eulerAngles.y / 57.3f);
        float vertical = moveVertical * Mathf.Cos(cameraObject.transform.eulerAngles.y / 57.3f) + -moveHorizontal * Mathf.Sin(cameraObject.transform.eulerAngles.y / 57.3f);

        movement = new Vector3(horizontal, 0.0f, vertical);

        // Gets rid of strafe jumps and increased speeds in the diagonals
        if (moveHorizontal != 0 && moveVertical != 0)
        {
            playerBody.transform.Translate(movement* movementSpeed / 1.4f);
        }

        // If not going diagonal, then normal movement
        else
        {
            playerBody.transform.Translate(movement* movementSpeed);
        }

        // Jumps if spacebar is pressed and player is on the ground
        if (grounded == true && Input.GetKey("space"))
        {
            playerBody.AddForce(jumpSpeed * new Vector3(0.0f, 100.0f, 0.0f));
        }
    }

    public void LevelUp()
    {
        for(int i = 0; i < 7; i++)
        {
            addOn = Random.Range(1, 3);
            playerStatsMax[i] += addOn;
            playerStats[i] += addOn;
        }
    }

    public void DisplayLevel()
    {
        playerStatsText.text = playerStats[0].ToString("000") + "\n" + playerStats[1].ToString("000") + "\n" + playerStats[2].ToString("000") + "\n" + playerStats[3].ToString("000") + "\n" + playerStats[4].ToString("000") + "\n" + playerStats[5].ToString("000") + "\n" + playerStats[6].ToString("000");
    }
}
