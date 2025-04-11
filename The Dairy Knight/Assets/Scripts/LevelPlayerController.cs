/*
Bryan Bong
Midterm
Filename: LevelPlayerController.cs
Description: This script handles the player character's movement and control scheme.
Also <ESC> functionality. This is a tweaked version of the original PlayerController 
script from the Midterm. This version has no jumping, but the player can move in any direction.
Tutorials:
https://www.youtube.com/watch?v=TcranVQUQ5U&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV
https://www.youtube.com/watch?v=_UBpkdKlJzE&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=3
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPlayerController : MonoBehaviour
{
    // Player Movement Variables
    [SerializeField] private float speed;
    private Rigidbody2D body;

    // Variables for spells and instructions
    public Text instructions;

    // Start is called before the first frame update
    void Start()
    {   
        // Instantiation
        body = GetComponent<Rigidbody2D>();
    }

    // TODO: Check for collision with next level
    void OnCollisionEnter2D(Collision2D collision)
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Allows for Player Movement along a 2D Axis
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        body.velocity = new Vector2(Input.GetAxis("Vertical") * speed, body.velocity.x);

        // Flips Player Sprite if needed
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
