/*
Bryan Bong
Midterm
Filename: SpellBehavior.cs
Description: (Essentially another version of RockScript.cs from earlier assignments)
Manages spell projectile behavior regarding lifespan and behavior when interacting with a collision.
The spell projectile will destroy both itself and the target on collision.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBehavior : MonoBehaviour
{
    float lifespan = 3.0f; //how long the bullet will stay in the game scene

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        if (lifespan <= 0)
        {
            Explode();
        }
    }

    //Bullet will be directly destroyed if it collides with Zombie
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Destructible")
        {
            Destroy(collision.gameObject);
        }

        Explode();
    }

    //Explode function to destroy bullet
    void Explode()
    {
        Destroy(gameObject);
    }
}
