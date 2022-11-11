using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyObject;
    [SerializeField] private Transform playerPos;
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
        if(level == "TutorialLevel")
        {
            numOfEnemies = 5;
        }

        for(int i=0;i<numOfEnemies;i++)
        {
            float xPos = Random.Range(playerPos.position.x-40, playerPos.position.x+40);
            float zPos = Random.Range(playerPos.position.z-40, playerPos.position.z+40);
            Vector3 spawnPos = new Vector3(xPos,50,zPos);

            if(Physics.Raycast(spawnPos, Vector3.down, out RaycastHit hit))
                Instantiate(enemyObject, hit.point, Quaternion.identity);
        }
    }
}
