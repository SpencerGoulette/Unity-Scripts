using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour {

    float prevAngle = 0.0f;

    public ParticleSystem landed;

    // Player's body
    private Rigidbody playerBody;

    //Player Animation
    Animator anim;

    // To check for respawn/reset
    public bool playerAlive = true;

    // Player's coins
    public float playerCoins = 0.0f;
    public TextMeshProUGUI coinText;
    public AudioClip coinGrab;

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
    public int playerEXP = 8;
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

    //Audio Source
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        playerBody = GetComponent<Rigidbody>();
        Inventory.SetActive(false);
        PlayerRespawn();
        Physics.gravity = new Vector3(0.0f, -80.0f, 0.0f);
        playerHealthText.text = playerStats[0].ToString("000");
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        coinText.fontSize = 64;

        if(Input.GetKeyDown(KeyCode.N))
        {
            anim.SetBool("twist", true);
        }

        else
        {
            anim.SetBool("twist", false);
        }

        for (int i = playerLevel - 1; i < playerLevel; i++)
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
        if (other.gameObject.CompareTag("Ground"))
        {
            landed.gameObject.transform.position = playerBody.transform.position + new Vector3(0.0f, 1.0f, 0.0f);
            landed.Play();
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            playerCoins++;
            coinText.fontSize = 74;
            coinText.text = "$" + playerCoins.ToString("000");
            audioSource.PlayOneShot(coinGrab, 0.7F);
        }

        if (other.gameObject.CompareTag("Caboodle"))
        {
            other.gameObject.SetActive(false);
            hasCaboodle = true;
            playerMessage.text = "PRESS [E] TO OPEN CABOODLE";
        }

        if (other.gameObject.CompareTag("Heart"))
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

        if (other.gameObject.CompareTag("Enemy"))
        {
            playerStats[0]--;
            playerHealthText.text = playerStats[0].ToString("000");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            landed.Stop();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            anim.SetBool("jump", false);
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

        playerBody.transform.eulerAngles = new Vector3(0.0f, prevAngle, 0.0f);

        // Gets rid of strafe jumps and increased speeds in the diagonals
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            float newAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(newAngle, Vector3.up);
            playerBody.transform.Translate(Vector3.forward * movementSpeed);
            anim.SetBool("running", true);
            prevAngle = newAngle;
        }

        else
        {
            anim.SetBool("running", false);
        }

        // Jumps if spacebar is pressed and player is on the ground
        if (grounded == true && Input.GetKey("space"))
        {
            playerBody.AddForce(jumpSpeed * new Vector3(0.0f, 100.0f, 0.0f));
            anim.SetBool("jump", true);
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
