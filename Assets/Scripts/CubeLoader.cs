using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

//bool change = false; 
public class CubeLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    	if(Input.GetKeyDown(KeyCode.Space)) {
    		SceneManager.LoadScene("TutorialLevel");
    	}
    	// if (change == true) {
    	// 	SceneManager.LoadScene("TutorialLevel");
    	// }
        
    }

    // void changeBool() {
    // 	change = true;
    // }
}
