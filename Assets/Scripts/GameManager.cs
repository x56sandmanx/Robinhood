using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject dialogueText;
    [SerializeField] private TextMeshProUGUI objectCounter;
    [SerializeField] private EnemySpawner enemySpawner;
    public GameObject coinObject;
    private string level;
    // Start is called before the first frame update
    void Start()
    {
        //Sets the time scale of the level and gets the name of the scene for enemyspawner to know what level it is.
        Time.timeScale = 1;
        level = SceneManager.GetActiveScene().name;

        //Call Spawn Enemies method from Enemy Spawner, passing in the level name to know how many enemies to spawn.
        enemySpawner.SpawnEnemies(level);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void PauseScreen()
    {
        //Checks for if we are in the paused state, to either freeze time and set cursor active, otherwise resume time and hide cursor.
        if(pauseScreen.activeSelf == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }

    }

    public void ShowDialogue(bool showDialog)
    {
        dialogueText.SetActive(showDialog);
    }

    public void UpdateCounter()
    {
        int counter = int.Parse(objectCounter.text);
        counter++;
        objectCounter.text = counter.ToString();
    }
}
