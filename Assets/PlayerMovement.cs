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
    public bool isDoubleJumpUsed;
    public bool isRight;

    private Rigidbody2D Player;
    private SpriteRenderer spriteRenderer;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        isRight = true;
        isJumping = true;
        isDoubleJumpUsed = false;
        Player = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        beforeJumpInertia = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            isRight = Input.GetAxisRaw("Horizontal") == 1;
            if (!isJumping)
            {
                anim.SetBool("isWalking", true);
            }
        }
        if (Input.GetButtonUp("Horizontal"))
        {
            if (anim.GetBool("isWalking"))
            {
                anim.SetBool("isWalking", false);
            }
        }

        if (Mathf.Abs(Player.velocity.x) >= 5)
        {
            horizontalInput = 0f;
        }
        if (isJumping)
        {
            if (beforeJumpInertia * horizontalInput <= 0f)
                horizontalInput *= 0.07f;
            else
                horizontalInput *= 0.9f;
        }

        //Player.velocity = new Vector2(speed * move, Player.velocity.y);  //speed 2
        Player.AddForce(new Vector2(speed * horizontalInput, 0f)); //speed 0.4
    
        if (Input.GetButton("Jump") && !isJumping)
        {
            isJumping = true;
            if (horizontalInput > 0f) beforeJumpInertia = 1f;
            else if (horizontalInput < 0f) beforeJumpInertia = -1f;
            else beforeJumpInertia = 0f;
            if (!Input.GetButton("Horizontal") && (Input.GetAxisRaw("Vertical") == 1))
            {
                Player.AddForce(new Vector2(0f, jump * 2f));
                anim.SetTrigger("UpJumpTrigger");    
            }
            else
            {
                Player.AddForce(new Vector2(0f, jump));
            }
        }

        if (Input.GetButtonDown("Jump") && !isDoubleJumpUsed && anim.GetBool("IsFloat"))
        {
            isDoubleJumpUsed = true;
            anim.SetBool("IsDoubleJumpUsed", true);
            int dir = 1;
            if (!isRight) dir = -1;
            Player.AddForce(new Vector2(dir * 300f, jump * 0.5f));
        }

        Debug.DrawRay(Player.position, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(Player.position, Vector3.down, 0.9f, LayerMask.GetMask("Platform"));
        if (rayHit.collider!=null)
        {
            //Debug.Log (rayHit.distance);
            anim.SetBool("IsFloat", false);
            isDoubleJumpUsed = false;
            anim.SetBool("IsDoubleJumpUsed", false);
        }
        else
        {
            anim.SetBool("IsFloat", true);
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
