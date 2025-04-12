/*
Bryan Bong
Midterm
Filename: PlayerController.cs
Description: This script handles the player character's movement and control scheme. It also
determines if it is grounded in order to allow for jumping. Also <ESC> functionality.
Tutorials:
https://www.youtube.com/watch?v=TcranVQUQ5U&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV
https://www.youtube.com/watch?v=_UBpkdKlJzE&list=PLgOEwFbvGm5o8hayFB6skAfa8Z-mw4dPV&index=3
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Player Movement Variables
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    private bool onGround;
    private Rigidbody2D body;

    // Variables for spells and instructions
    public Text instructions;

    // Start is called before the first frame update
    void Start()
    {   
        // Instantiation
        body = GetComponent<Rigidbody2D>();
        onGround = false;
    }

    // Checks if the player is currently on a surface
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Checks if the player is grounded for a jump
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Border" || collision.gameObject.tag == "Boundary"
            || collision.gameObject.tag == "Uncompleted" || collision.gameObject.tag == "Completed")
        {
            onGround = true;
        } else
        {
            onGround = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Tracks player position
        PlayerData.playerPosition = transform.position;

        // Allows for Player Movement along a 2D Axis
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        // Flips Player Sprite if needed
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && onGround)
        {
            // Allows for Jumping if the character is not in the air
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            onGround = false;
        } else if (Input.GetKeyDown(KeyCode.C)) {
            // Shows/Hides Instructions 
            //instructions.gameObject.SetActive(!instructions.gameObject.active);
        }
    }
}
