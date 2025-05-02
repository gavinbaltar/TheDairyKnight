using UnityEngine;

/*
    Assignment: Final - also used in Midterm
    Written by: Gavin Baltar created by following tutorial: https://www.youtube.com/watch?v=9dYDBomQpBQ
    Filename: PauseMenu.cs

    Description: This script takes in user input for the TAB key in order to pausethe game. This brings up a menu and freezes the game's time by setting time scaleto 0. 
        This allows the player to quit the game mid fight if they want to reset or go to main menu.
*/

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;
    void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}