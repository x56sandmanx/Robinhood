using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyObject;
    [SerializeField] private GameManager gameManager;
    private float numOfEnemies;
    // Start is called before the first frame update
    void Start()
    {
        if(gameManager.level == "TutorialLevel")
        {
            numOfEnemies = 5;
        }

        for(int i=0;i<numOfEnemies;i++)
        {
            Instantiate(enemyObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
