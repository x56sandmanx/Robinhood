using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private bool wanderMode = true;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private Transform playerPos;
    [SerializeField] private int health;
    [SerializeField] private Slider healthSlider;

    private float timer;
    private float moveTime;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        //Start of random timer for moving and a moveTime that will calculate if the enemy can move or not
        timer = Random.Range(5f,10f);
        moveTime = 0;
        health = 5;
        healthSlider.value = health;
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
                Destroy(gameObject);
            }
        }
    }

    IEnumerator DecreaseHealth(){
        while(healthSlider.value >= health)
        {
            Debug.Log("lowering health");
            healthSlider.value -= 10 * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
