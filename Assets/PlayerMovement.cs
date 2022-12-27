using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private float horizontalInput;
    private float beforeJumpInertia; // 1f, 0f, -1f

    public float jump;

    public bool isJumping;

    private Rigidbody2D Player;

    // Start is called before the first frame update
    void Start()
    {
        isJumping = true;
        Player = GetComponent<Rigidbody2D>();
        beforeJumpInertia = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(Player.velocity.x) >= 3)
        {
            horizontalInput = 0f;
        }
        if (isJumping)
        {
            if (beforeJumpInertia * horizontalInput <= 0f)
            horizontalInput *= 0.07f;
            
        }

        //Player.velocity = new Vector2(speed * move, Player.velocity.y);  //speed 2
        Player.AddForce(new Vector2(speed * horizontalInput, 0f)); //speed 0.4
    
        if (Input.GetButton("Jump") && isJumping == false)
        {
            isJumping = true;
            if (horizontalInput > 0f) beforeJumpInertia = 1f;
            else if (horizontalInput < 0f) beforeJumpInertia = -1f;
            else beforeJumpInertia = 0f;
            Player.AddForce(new Vector2(0f, jump));
            Debug.Log("jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    /*
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;   
        }
    }
    */
}
