using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Transform orientation;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject touchingObject;
    [SerializeField] private Slider playerHealth;

    float horizontal;
    float vertical;
    float groundDistance = 0.4f;
    float walkSpeed = 4f;
    float sprintSpeed = 6f;
    float moveSpeed;
    bool isGrounded;

    public AudioSource[] sounds;
    private AudioSource jump;
	private AudioSource treasure;
	private AudioSource damage;

    Vector3 moveDir;
    Vector3 velocity;
    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveSpeed = walkSpeed;

        sounds = GetComponents<AudioSource>();
        jump = sounds[1];
        treasure = sounds[2];
        damage = sounds[3];
        playerHealth.maxValue = 100;

        if(PlayerPrefs.HasKey("health"))
        {
            playerHealth.value = PlayerPrefs.GetInt("health");
            GameData.health = PlayerPrefs.GetInt("health");
        }
        else
        {
            playerHealth.value = 100;
            GameData.health = 100;
        }
    }


    // Update is called once per frame
    void Update()
    {
        //Checking that the player is grounded on the ground for jumping
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);

        //If the player is grounded, and they aren't moving, setting velocity to -2 so offset the velocity going up forever
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        //Getting Unity Inputs
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        //The Direction of which the player is moving
        moveDir = orientation.forward * vertical + orientation.right * horizontal;

        //If player holds shift, they can sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }



        //Moving player based on movement and sprint or not
        characterController.Move(moveDir * moveSpeed * Time.deltaTime);

        //If player is grounded, can jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jump.Play();
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        //UI For Pause Screen
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.PauseScreen();
        }

        //Checking if the player is in range of an item that they can pick up
        //if (touchingObject != null && Input.GetKeyDown(KeyCode.E))
        //{
        //    Destroy(touchingObject);
        //    touchingObject = null;
        //    gameManager.ShowDialogue(false);
        //
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            //gameManager.ShowDialogue(true);
            Destroy(other.gameObject);
            gameManager.UpdateCounter();
            treasure.Play();
            //touchingObject = other.gameObject;
        }
        if(other.gameObject.CompareTag("Treasure"))
        {
            //gameManager.ShowDialogue(true);
            Destroy(other.gameObject);
            gameManager.UpdateCounter();
            gameManager.UpdateCounter();
            gameManager.UpdateCounter();
            gameManager.UpdateCounter();
            gameManager.UpdateCounter();
            treasure.Play();
            //touchingObject = other.gameObject;
        }
        if(other.gameObject.name == "Damage")
        {
            GameData.health -= 10;
            if(GameData.health == 0)
                gameManager.ChangeScene("LoseScene");
            else
                StartCoroutine(DecreaseHealth());
            damage.Play();
        }
        if(other.gameObject.name == "DamageBoss")
        {
            GameData.health -= 20;
            if(GameData.health == 0)
                gameManager.ChangeScene("LoseScene");
            else
                StartCoroutine(DecreaseHealth());
            damage.Play();
        }
    }

  //  private void OnTriggerExit(Collider other)
  //  {
  //      if (other.gameObject.CompareTag("Coin"))
  //      {
  //          gameManager.ShowDialogue(false);
  //          touchingObject = null;
  //      }
  //  }

    IEnumerator DecreaseHealth(){
        while(playerHealth.value >= GameData.health)
        {
            playerHealth.value -= 10 * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
