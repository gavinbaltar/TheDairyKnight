/*
Bryan Bong
Final
Filename: PlayerData.cs
Description: Script that stores static variables in order to keep track of
the player's progress in the LevelSelect scene and the overall game.
*/

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static int level = 1;
    public static bool wonPrevious = false;
    public static Vector2 playerPosition = Vector2.zero;
    public static bool canSelect = false;
    public static string sceneName = "";
}
