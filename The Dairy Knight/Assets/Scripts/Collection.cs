/*
Bryan Bong
Midterm
Filename: Collection.cs
Description: This script handles the behavior when the player collects an item. It will destroy the item and play a sound effect.
It will also increment the current score by one to track the collection. Furthermore, when all feathers have been collected,
the text will change to direct the player towards the portal, where this script then triggers the loading of the Game Over screen.
If the collected item is a key, instead of a feather, it will unlock the gate blocking the player from progressing.
*/

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Collection : MonoBehaviour
{
    public AudioClip audioClip;
    private GameObject toDelete;
    public AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Collectible")
        {
            Destroy(col.gameObject);
            audioSource.PlayOneShot(audioClip);
            ScoreTracker.gscore += 1;
        } else if (col.gameObject.tag == "Key")
        {
            // If they key is collected, unlock the gate
            Destroy(col.gameObject);
            audioSource.PlayOneShot(audioClip);
            GameObject gate = GameObject.Find("gate");
            Destroy(gate);


        } else if (col.gameObject.name == "PortalTrigger" && ScoreTracker.gscore >= 11)
        {
            // Win Condition For When All Leaves Collected and Portal Visited
            SceneManager.LoadScene("GameOverScreen");
        }
    }
}
