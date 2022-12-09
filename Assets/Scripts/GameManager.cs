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
    [SerializeField] private GameObject boss;
    [SerializeField] private Transform bossSpawn;
    [SerializeField] private TextMeshProUGUI objectCounter;
    [SerializeField] private TextMeshProUGUI enemyCounter;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject deleteSaveObject;
    [SerializeField] private GameObject HTPScreen;
    [SerializeField] private GameObject AScreen;
    public GameObject coinObject;
    private string level;
    // Start is called before the first frame update
    void Start()
    {
        //Sets the time scale of the level and gets the name of the scene for enemyspawner to know what level it is.
        Time.timeScale = 1;
        level = SceneManager.GetActiveScene().name;

        if(level == "WinScene" || level == "LoseScene" || level == "TitleScreen")
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;

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
        if(PlayerPrefs.HasKey("enemyKills"))
            GameData.enemyKills = PlayerPrefs.GetInt("enemyKills");
        else
            GameData.enemyKills = 0;
        if(PlayerPrefs.HasKey("level"))
            GameData.level = PlayerPrefs.GetString("level");
        else
            GameData.level = "TutorialLevel";

        if(level != "TitleScreen" || level != "WinScene")
        {
            objectCounter.text = GameData.coinCount.ToString();
            if(GameData.level == "TutorialLevel")
            {
                int counter = 10 - GameData.enemyKills;
                enemyCounter.text = counter.ToString();
            }
            else
            {
                int counter = 20 - GameData.enemyKills;
                enemyCounter.text = counter.ToString();
            }

            //Call Spawn Enemies method from Enemy Spawner, passing in the level name to know how many enemies to spawn.
            //enemySpawner.SpawnEnemies(GameData.level);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene(string sceneName)
    {
        if(SceneManager.GetActiveScene().name == "TitleScreen")
        {
            if(PlayerPrefs.HasKey("level"))
                SceneManager.LoadScene(PlayerPrefs.GetString("level"));
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

    public void UpdateEnemyCounter()
    {
        if(GameData.level == "TutorialLevel")
        {
            int counter = 10 - GameData.enemyKills;
            enemyCounter.text = counter.ToString();
        }
        else
        {
            int counter = 20 - GameData.enemyKills;
            enemyCounter.text = counter.ToString();
        }
    }

    public void SaveGame()
    {
        Debug.Log("Clicked");
        PlayerPrefs.SetInt("coinCount", GameData.coinCount);
        PlayerPrefs.SetInt("enemyKills", GameData.enemyKills);
        PlayerPrefs.SetString("level", GameData.level);
        PlayerPrefs.SetInt("health", GameData.health);
        ChangeScene("TitleScreen");
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
        deleteSaveObject.SetActive(false);
    }

    public void Restart()
    {
        GameData.health = 100;
        PlayerPrefs.SetInt("health", GameData.health);
        if(PlayerPrefs.HasKey("level"))
            ChangeScene(PlayerPrefs.GetString("level"));
        else
            ChangeScene("TutorialLevel");
    }
    
    public void HowToPlayScreen()
    {
        if(HTPScreen.activeSelf)
            HTPScreen.SetActive(false);
        else
            HTPScreen.SetActive(true);
    }
    
    public void AboutScreen()
    {
        if(AScreen.activeSelf)
            AScreen.SetActive(false);
        else
            AScreen.SetActive(true);
    }

    public void ToTitle()
    {
        PlayerPrefs.DeleteAll();
        ChangeScene("TitleScreen");
    }

    public void SpawnBoss()
    {
        Vector3 pos = new Vector3(bossSpawn.position.x, bossSpawn.position.y, bossSpawn.position.z);
        Instantiate(boss, pos, Quaternion.identity);
    }
}
