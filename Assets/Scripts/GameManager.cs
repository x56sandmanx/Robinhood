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
    [SerializeField] private GameObject deleteSaveObject;
    public GameObject coinObject;
    private string level;
    // Start is called before the first frame update
    void Start()
    {
        //Sets the time scale of the level and gets the name of the scene for enemyspawner to know what level it is.
        Time.timeScale = 1;
        level = SceneManager.GetActiveScene().name;

        if(level == "TitleScreen")
        {
            if(PlayerPrefs.HasKey("coinCount"))
                deleteSaveObject.SetActive(true);
            else
                deleteSaveObject.SetActive(false);
        }

        if(PlayerPrefs.HasKey("coinCount"))
            GameData.coinCount = PlayerPrefs.GetInt("coinCount");
        else
            GameData.coinCount = 0;

        objectCounter.text = GameData.coinCount.ToString();

        //Call Spawn Enemies method from Enemy Spawner, passing in the level name to know how many enemies to spawn.
        enemySpawner.SpawnEnemies(level);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene(string sceneName)
    {
        if(SceneManager.GetActiveScene().name == "TitleScreen")
        {
            if(PlayerPrefs.HasKey("coinCount"))
                SceneManager.LoadScene("TutorialLevel");
            else
                SceneManager.LoadScene("InitialCutscene");
        }
        else
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
        GameData.coinCount = counter;
        objectCounter.text = counter.ToString();
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("coinCount", GameData.coinCount);
        ChangeScene("TitleScreen");
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
        deleteSaveObject.SetActive(false);
    }
}
