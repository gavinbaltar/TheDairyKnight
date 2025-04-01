/* 
    
    Assignment: Final
    Written by: David Hall
    Filename: CheesedToSwitchWeapons

    Description: Upon clicking the swap button, the player will be able to click another button from a list of buttons. 
A new button will appear for each weapon the player unlocks. Plugging this script --or putting it on a game manager before plugging that-- into each button
would allow you to run the following functions on click.
 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    //Weapon booleans. Add a new one for every weapon to control whether or not they're active.
    public bool Sword;
    //public bool Lance;
    //public bool Bow;
    //public bool Hammer;
    //etc.

    void start() //optional if you want the player to have the sword equipped at the start of each battle
    {
        Sword = true;
        //Set all other weapon bools to false.
        //i.e: Bow = false;
        //etc.
    }

    void SharpCheddarSword()
    {
        Sword = true;
        //Set all other weapon bools to false.
        //i.e: Bow = false;
        //etc.
    }

    void MozzarellaStickLance()
    {
        //Lance = true;
        Sword = false;
        //like before, set all other weapon bools to false
    }

    void StringCheeseBow()
    {
        //Bow = true;
        Sword = false;
        //you know what to do
    }

    //You should also include your functions for controlling the player attacks, animations, and hitboxes here (or above) should we have to combine scripts.
}
