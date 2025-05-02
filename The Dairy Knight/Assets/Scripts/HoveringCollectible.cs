/*
Bryan Bong
Final - Updated from Midterm
Filename: HoveringCollectible.cs
Description: This script as a hovering motion to sprites using sinusoidal functions by oscillating it
from a point of origin. This script was originally used in the final for collectibles, but was repurposed for
the animation it added, minus the collectible functionality.
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
