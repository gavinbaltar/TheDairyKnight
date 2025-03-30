/*
Bryan Bong
Midterm
Filename: HoveringCollectible.cs
Description: This script's creates a hover "animation" for the collectibles. Also used for the portal.
Tutorial: https://www.youtube.com/watch?v=C7sPsksH4JM
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringCollectible : MonoBehaviour
{
    [SerializeField] public float amplitude = 0.5f;
    [SerializeField] public float frequency = 1.0f;
    Vector3 posOrigin = new Vector3();
    Vector3 tempPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        posOrigin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        tempPos = posOrigin;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }
}
