/*
Bryan Bong
Midterm
Filename: CastSpell.cs
Description: (Similar logic to LaunchButtonPressed.cs from Challenge 2 and the Throw Rock script provided by assignments)
This script handles the creation of a spell projectile that is launched from the player character upon clicking the left mouse button (LMB).
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpell : MonoBehaviour
{
    public GameObject spell_prefab;
    public GameObject spell_origin;
    [SerializeField] float startForce = 10f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            // Changes direction of the spell if player is turned around
            if (transform.localScale.x == -1)
            {
                startForce = -10f;
            } else
            {
                startForce = 10f;
            }

            GameObject spell = (GameObject)Instantiate(spell_prefab, spell_origin.transform.position,
            spell_origin.transform.rotation); //cast spell to the forward direction
            spell.GetComponent<Rigidbody2D>().AddForce(new Vector2(startForce, 0), ForceMode2D.Impulse); //adding force to our spell
        }
    }
}
