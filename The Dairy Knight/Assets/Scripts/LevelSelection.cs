/*
Bryan Bong
Final
Filename: LevelSelection.cs
Description: Script to manage level selection and which levels can be interacted with. Saves progress.
*/

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public GameObject player;
    public GameObject WIP; //TODO: REMOVE FOR FINAL BUILD

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerData.level == 2)
        {
            // After First Level Completed, Remove 1st Barrier
            player.transform.position = PlayerData.playerPosition;
            GameObject chili = GameObject.Find("ChiliLevel");
            Destroy(chili);
            Destroy(GameObject.Find("FirstBound"));
            WIP.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && PlayerData.canSelect)
        {
            SceneManager.LoadScene(PlayerData.sceneName);
        }
    }
}
