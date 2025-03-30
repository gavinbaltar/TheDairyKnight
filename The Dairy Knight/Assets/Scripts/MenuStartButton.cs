/*
Bryan Bong
Midterm
Filename: MenuStartButton.cs
Description: (Logic Similar to Assignment 8): 
This script handles the changing of scenes/level when the player clicks on the start button. 
This script also handles exiting the game when the user presses the <ESC> key.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStartButton : MonoBehaviour
{

    // Loads a new scene when the START button is pressed
    public string nextScene;
    public void StartGame()
    {
        SceneManager.LoadScene(nextScene);
    }

    // Menu Escape Functionality on <ESC>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
