using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{

    public Animator anim;
    [SerializeField] public bool wanderMode = true;
    public bool isMoving = false;
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private Transform playerPos;
    [SerializeField] private int health;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject coinObject;
    [SerializeField] private GameManager gameManager;

    private float timer;
    private float moveTime;

    public AudioSource[] sounds;
    private AudioSource enemyDeath;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        //Start of random timer for moving and a moveTime that will calculate if the enemy can move or not
        timer = Random.Range(5f,10f);
        moveTime = 0;
        if(gameObject.tag == "Boss")
            health = 20;
        else
            health = 5;
        healthSlider.maxValue = health;
        healthSlider.value = healthSlider.maxValue;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        coinObject = gameManager.coinObject;

        sounds = GetComponents<AudioSource>();
        enemyDeath = sounds[0];
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.gameObject.transform.LookAt(playerPos);
        //Checking that if the enemies velocity is something other that 0, that it is moving or not
        if(navAgent.velocity != Vector3.zero)
            isMoving = true;
        else
            isMoving = false;

        if(isMoving) {
          anim.Play("Walk");
        } else {
          anim.Play("Idle");
        }


        //If enemy is in wander mode and not moving, will start a timer to get a pos to move
        if(wanderMode)
        {
            if(!isMoving)
            {
                //Just adding up the move time while it is less than timer, so it doesn't move every frame
                if(moveTime < timer)
                {
                    moveTime += Time.deltaTime;
                }
                else
                    //Get a Random Point to move.
                    navAgent.SetDestination(RandomNavMeshLocation(10f));
            }
        }
        else
        {
            navAgent.SetDestination(playerPos.position);
        }
    }

    private Vector3 RandomNavMeshLocation(float radius)
    {
        //Get a random point in a sphere multiplied by the radius, so picking a random point in a sphere
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;

        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;

        //Finding the nearest position in the randomDirection, and radius
        if(NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
            finalPosition = hit.position;

        //Resetting the timer and moveTime and finally returning the finalPos the enemy will move to
        timer = Random.Range(5f,10f);
        moveTime = 0;
        return finalPosition;
    }

    //Triggers to tell if enemy is in wander mode or not
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            wanderMode = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            wanderMode = true;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Arrow"))
        {
            health--;
            Destroy(other.gameObject);
            StartCoroutine(DecreaseHealth());
            if(health == 0)
            {
                if(gameObject.tag == "Boss")
                {
                    gameManager.ChangeScene("WinScene");
                }
				enemyDeath.Play();
                int numOfCoins = Random.Range(1,5);
                for(int i = 0;i<numOfCoins;i++)
                    Instantiate(coinObject, gameObject.transform.position, gameObject.transform.rotation);
                GameData.enemyKills += 1;
                CheckEnemyKills();
                Destroy(gameObject, .3f);
            }
        }
    }

    IEnumerator DecreaseHealth(){
        while(healthSlider.value >= health)
        {
            healthSlider.value -= 10 * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private void CheckEnemyKills(){
        gameManager.UpdateEnemyCounter();
        if(GameData.level == "TutorialLevel")
        {
            if(GameData.enemyKills == 10)
            {
                GameData.level = "Level01";
                GameData.enemyKills = 0;
                PlayerPrefs.SetString("level",GameData.level);
                PlayerPrefs.SetInt("enemyKills",GameData.enemyKills);
                PlayerPrefs.SetInt("health",GameData.health);
                PlayerPrefs.SetInt("coinCount",GameData.coinCount);
                gameManager.ChangeScene(GameData.level);
            }
        }
        else
        {
            if(GameData.enemyKills == 20)
            {
                Debug.Log("Boss Time!");
                gameManager.SpawnBoss();
            }
        }
    }
}
