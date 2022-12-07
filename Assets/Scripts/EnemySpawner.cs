using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyObject;
    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform enemyTransform;
    private float numOfEnemies;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemies(string level)
    {
        //If statements will check the level name to set the numOfEnemies to certain number
        if(GameData.level == "TutorialLevel")
            numOfEnemies = 10 - GameData.enemyKills;
        else if(GameData.level == "Level01")
            numOfEnemies = 20 - GameData.enemyKills;
        else
            numOfEnemies = 30 - GameData.enemyKills;


        //For each number of enemy, we will get a random x and z coord based off the palyers pos, and make a new Vector 3 spawnPos that used thos random x and z, and default to 50 for y
        for(int i=0;i<numOfEnemies;i++)
        {
            float xPos = Random.Range(gameObject.transform.position.x-65, gameObject.transform.position.x+65);
            float zPos = Random.Range(gameObject.transform.position.z-65, gameObject.transform.position.z+65);
            Vector3 spawnPos = new Vector3(xPos,50,zPos);

            //Raycast will checkout how far the object is from the ground with the spawnPos, and on the hit.point we will spawn the enemy in order for the enemy to spawn on the ground
            if(Physics.Raycast(spawnPos, Vector3.down, out RaycastHit hit))
                Instantiate(enemyObject, hit.point, Quaternion.identity, enemyTransform);
        }
    }
}
