/*
Bryan Bong
Final
Filename: LevelSelection.cs
Description: Script to manage level selection and which levels can be interacted with.
Saves progress using static variables from PlayerData.cs. This script will update the level
according to the player's level, such as by removing barriers or the previous level's interactable.
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
    public new GameObject camera;
    public GameObject WIP; //TODO: REMOVE FOR FINAL BUILD

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerData.level > 1)
        {
            // After First Level Completed, Remove 1st Barrier
            player.transform.position = PlayerData.playerPosition;
            camera.transform.position = PlayerData.cameraPosition;
            GameObject chili = GameObject.Find("FirstLevel");
            Destroy(chili);
            Destroy(GameObject.Find("FirstBound"));
            //WIP.gameObject.SetActive(true);
        }

        if (PlayerData.level > 2)
        {
            player.transform.position = PlayerData.playerPosition;
            camera.transform.position = PlayerData.cameraPosition;
            GameObject jalapeno = GameObject.Find("SecondLevel");
            Destroy(jalapeno);
            Destroy(GameObject.Find("SecondBound"));
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
