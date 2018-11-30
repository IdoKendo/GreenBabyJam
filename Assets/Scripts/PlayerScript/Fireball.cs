using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float Speed; // Floating point variable to store the player's movement speed.
    public int Direction;
    public float UpSpeed;

    private Rigidbody2D rb2d; // Store a reference to the Rigidbody2D component required to use 2D Physics.

    // Use this for initialization
    private void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    private void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Direction * 1; //Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.

        // TODO: elipsys
        //float moveVertical = 0.1;//Input.GetAxis("Vertical");
        UpSpeed = (float)(UpSpeed - 0.08);
        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, UpSpeed);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * Speed);
    }
}
