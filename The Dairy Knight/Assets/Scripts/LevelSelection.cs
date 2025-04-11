/*
Bryan Bong
Final
Filename: LevelSelection.cs
Description: Script to manage level selection and which levels can be interacted with.
Tutorial (Based on): https://www.youtube.com/watch?v=vpbPd6jNEBs
*/

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    public GameObject player;
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
