/*
Bryan Bong
Midterm
Filename: ScoreTracker.cs
Description: Tracks the score using text. Similar to ScoreCount.cs from past assignments. Will activate the portal object (for win condition).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    public static int gscore = 0;
    public Text scoreText;
    public GameObject portal;
    void Update()
    {
        scoreText.text = "Collected: " + gscore + "/11";

        // Opens Portal for win condition
        if (gscore >= 11)
        {
            scoreText.text = "Go to the portal!";
            portal.SetActive(true);
        } 

    }
}
