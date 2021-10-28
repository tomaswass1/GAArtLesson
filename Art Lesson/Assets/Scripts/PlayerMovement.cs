using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //speed we want char to move
    public float speed = 1;

    //how powerful is character's jump
    public float jumpForce = 1;

    //distance to check from char to ground
    public float groundCheckDistance = 0.5f;

    //the sprite
    SpriteRenderer sprite = null;

    //rigidbody ref for the physics
    Rigidbody2D rb = null;

    //check if char is in the air
    bool inAir = false;

    //direction
    float scaleX = 1;


    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        scaleX = transform.localScale.x;
    }

    // Update is called once per frame
    void Update() {
        //set the speed for left and right
        rb.velocity = Vector2.right * Input.GetAxis("Horizontal") * speed + Vector2.up * rb.velocity.y;

        //flip the sprite to the set direction
        if (Input.GetAxis("Horizontal") != 0) {
            int direction = 1;
            if (Input.GetAxis("Horizontal") < 0) {
                direction = -1;
            }
            transform.localScale = new Vector3(scaleX * direction, transform.localScale.y, transform.localScale.z);
        }

        //make sure char is on the ground by checking the distance from the char downwards
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance);
        //if there is ground close enough you aren't in the air
        inAir = hit.collider == null;

        //if char is on the ground and "jump" is åushed the char jumps
        if (!inAir && Input.GetButtonDown("Jump")) {
            transform.position += Vector3.up * 0.1f;
            rb.AddForce(Vector2.up * jumpForce);
        }
    }
}
