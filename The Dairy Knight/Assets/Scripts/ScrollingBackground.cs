/*
Bryan Bong
Final - From Midterm
Filename: ScrollingBackground.cs
Description: This script handles the behavior of the background scene. First off, it adds a parallax
effect, making it so that the background moves alongside the character in order to simulate travel.
Second, it cycles the background such that the player never travels past it. In other words, it "catches up"
to the player if they move too far, and "refreshes" itself if the background reaches its end. This ensures that
the player could theoretically travel forever without running out of background
Tutorial: https://www.youtube.com/watch?v=Wz3nbQPYwss.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    private float length, startPos;
    public GameObject cam;
    public float parallax; // Scroll Speed

    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallax));
        float distance = cam.transform.position.x * parallax;
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
        if (temp > startPos + length) { startPos += length;}
        else if (temp < startPos - length) { startPos -= length;}
    }
}
