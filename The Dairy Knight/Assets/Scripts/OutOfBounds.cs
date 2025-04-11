/*
Bryan Bong
Final
Filename: OutOfBounds.cs
Description: Script to display a message if player collides with one of the borders.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OutOfBounds : MonoBehaviour
{
    public Text boundaryText;
    public Text borderText;
    public float targetTime = 3.0f;
    bool timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = false;
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
        } else if (collision.gameObject.tag == "Uncompleted")
        {
            if (collision.gameObject.name == "ChiliLevel")
            {
                Debug.Log(PlayerData.level);
                SceneManager.LoadScene("TestLevel");
            }
        }
    }
}
