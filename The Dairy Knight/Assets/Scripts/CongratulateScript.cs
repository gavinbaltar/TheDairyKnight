/*
Bryan Bong
Midterm
Filename: CongratulateScript.cs
Description: This script's only purpose is to provide exit functionality via <ESC> for the Game Over screen.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongratulateScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
