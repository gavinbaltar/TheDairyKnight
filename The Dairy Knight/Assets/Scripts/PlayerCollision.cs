/*
Bryan Bong
Final
Filename: PlayerCollision.cs
Description: Script to display a message if player collides with one of the borders or boundaries.
Will also detect if the player collides with a triggers around the level selection to show the text
and works with LevelSelection.cs to see if the user can enter the E key.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    public Text boundaryText;
    public Text borderText;
    public GameObject tutorial;
    public GameObject startPrompt1;
    public GameObject startPrompt2;
    public GameObject startPrompt3;
    //public GameObject startPrompt3;
    public float targetTime = 3.0f;
    bool timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = false;
        if (PlayerData.level != 1)
        {
            tutorial.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer)
        {
            targetTime -= Time.deltaTime;
            if (targetTime <= 0)
            {
                boundaryText.gameObject.SetActive(false);
                borderText.gameObject.SetActive(false);
                timer = false;
            }
        }
    }

    // Checks if the player collides with anything
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Checks if the player is grounded for a jump
        if (collision.gameObject.tag == "Border")
        {
            borderText.gameObject.SetActive(true);
            timer = true;
        }
        else if (collision.gameObject.tag == "Boundary")
        {
            boundaryText.gameObject.SetActive(true);
            timer = true;
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject startPrompt = null;
        if (PlayerData.level == 1)
        {
            startPrompt = startPrompt1;
        } else if (PlayerData.level == 2)
        {
            startPrompt = startPrompt2;
        } else if (PlayerData.level == 3)
        {
            startPrompt = startPrompt3;
        }

        if (collision.gameObject.tag == "Uncompleted")
        {
            startPrompt.SetActive(true);

            if (collision.gameObject.name == "ChiliLevel")
            {
                PlayerData.sceneName = "TheDairyKnight_GAMMA1";
                PlayerData.canSelect = true;
            } else if (collision.gameObject.name == "JalapenoLevel")
            {
                PlayerData.sceneName = "TheDairyKnight_GAMMA2";
                PlayerData.canSelect = true;
            } else if (collision.gameObject.name == "HabaneroLevel")
            {
                PlayerData.sceneName = "TheDairyKnight_GAMMA3";
                PlayerData.canSelect = true;
            }

        } else if (collision.gameObject.tag == "Boundary")
        {
            startPrompt.SetActive(false);
            PlayerData.canSelect = false;
        } else if (collision.gameObject.name == "EndTutorial")
        {
            tutorial.SetActive(false);
        }
    }


}
