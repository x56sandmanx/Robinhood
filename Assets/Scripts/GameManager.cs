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
    public string level;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        level = SceneManager.GetActiveScene().name;
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
