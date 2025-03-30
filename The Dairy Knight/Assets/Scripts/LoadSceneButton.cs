using UnityEngine;
using UnityEngine.SceneManagement;

/*
    Assignment: Mid
    Written by: Gavin Baltar
    Filename: LoadSceneButton.cs

    Description: This script take is used to load specific scenes on button press
        by taking in the string name of the scene to change to.
*/

public class LoadSceneButton : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f; // for pause menu.
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
