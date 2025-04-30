/*
Bryan Bong
Midterm
Filename: CameraFollowPlayer.cs
Description: This script makes it so that the camera follows the player character at all times.
Tutorial: https://www.youtube.com/watch?v=FXqwunFQuao
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public float FollowSpeed = 2f;
    [SerializeField] public float yOffset = 1f;
    public Transform player;

    private void Start()
    {
        transform.position = PlayerData.cameraPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector4 newPos = new Vector4(player.position.x, player.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed*Time.deltaTime);
        PlayerData.cameraPosition = newPos;
    }
}
