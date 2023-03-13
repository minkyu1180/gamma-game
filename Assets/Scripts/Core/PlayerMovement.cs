using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;
    private float horizontalInput;
    private float beforeJumpInertia; // 1f, 0f, -1f

    private float defaultSpeed = 20f;
    private float defaultJump = 450f;
    private float defaultLinearDrag = 0.3f;

    public bool doubleJumpEnabled;
    public bool highJumpEnabled;
    public bool frozen;

    public bool isDirectionDoomed;
    public bool isDoubleJumpUsed;
    public bool isPassingObject;
    public bool onPassablePlatform;
    public bool isRight;

    private Rigidbody2D PlayerRigidbody;
    private BoxCollider2D PlayerBoxCollider;
    private SpriteRenderer spriteRenderer;
    public Animator anim;

    AudioSource audioSource;
    private AudioClip jumpSound;


    // Start is called before the first frame update
    void Start()
    {
        jump = defaultJump;
        speed = defaultSpeed;
        isRight = true;
        isDirectionDoomed = true;
        frozen = false;
        isDoubleJumpUsed = false;
        isPassingObject = false;
        onPassablePlatform = false;
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        PlayerBoxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        beforeJumpInertia = 0f;

        audioSource = GetComponent<AudioSource>();
        jumpSound = Resources.Load("Sound/Voice/jumpSound") as AudioClip;
    }

    void Update()
    {
        if (!frozen)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            if (Input.GetButton("Horizontal"))
            {
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
                isRight = Input.GetAxisRaw("Horizontal") == 1;
                if (!isDirectionDoomed)
                {
                    anim.SetBool("IsWalking", true);
                }
            }
            if (Input.GetButtonUp("Horizontal"))
            {
                if (anim.GetBool("IsWalking"))
                {
                    anim.SetBool("IsWalking", false);
                }
            }

            if (Mathf.Abs(PlayerRigidbody.velocity.x) >= 5)
            {
                horizontalInput = 0f;
            }
            if (isDirectionDoomed) //isJump     
            {
                if (beforeJumpInertia * horizontalInput <= 0f)
                    horizontalInput *= 0.07f;
                else
                    horizontalInput *= 0.9f;
            }

            if (!Input.GetButton("Horizontal") && (Input.GetAxisRaw("Vertical") == -1) && !anim.GetBool("IsFloat") && (Mathf.Abs(PlayerRigidbody.velocity.y) <= 0.01))
            {
                if (!anim.GetBool("IsDown"))
                {
                    anim.SetBool("IsDown", true);
                    jump = 0f;
                    speed = 0f;
                    PlayerRigidbody.drag = 8.0f;
                    // disable jump, walk and increase linear drag when down
                }
            }
            if (Input.GetButtonUp("Vertical") && anim.GetBool("IsDown") && !isPassingObject)
            {
                anim.SetBool("IsDown", false);
                jump = defaultJump;
                speed = defaultSpeed;
                PlayerRigidbody.drag = defaultLinearDrag;
            }
            
            if (Input.GetButtonDown("Jump") && !anim.GetBool("IsFloat") && anim.GetBool("IsDown") && onPassablePlatform)
            {
                StartCoroutine(passObjectInumerator(0.4f));
            }

            //Player.transform.Translate (speed * Time.deltaTime * horizontalInput, 0, 0);  //speed 
            //Player.AddForce(new Vector2(speed * horizontalInput, 0f)); //speed 2
            //Debug.Log(horizontalInput);
            if (Input.GetButtonDown("Jump") && !anim.GetBool("IsFloat") && !anim.GetBool("IsDown")) // isJumping
            {
                isDirectionDoomed = true;
                if (horizontalInput > 0f) beforeJumpInertia = 1f;
                else if (horizontalInput < 0f) beforeJumpInertia = -1f;
                else beforeJumpInertia = 0f;
                if (!Input.GetButton("Horizontal") && (Input.GetAxisRaw("Vertical") == 1) && highJumpEnabled)
                {
                    PlayerRigidbody.AddForce(new Vector2(0f, jump * 2f));
                    anim.SetTrigger("UpJumpTrigger");    
                }
                else
                {
                    audioSource.PlayOneShot(jumpSound);
                    PlayerRigidbody.AddForce(new Vector2(0f, jump));
                }
            }
            else if (Input.GetButtonDown("Jump") && !isDoubleJumpUsed && anim.GetBool("IsFloat") && doubleJumpEnabled)
            {
                isDoubleJumpUsed = true;
                anim.SetBool("IsDoubleJumpUsed", true);
                int dir = 1;
                if (!isRight) dir = -1;
                PlayerRigidbody.AddForce(new Vector2(dir * 300f, jump * 0.5f));
            }
            
        }


        //Debug.DrawRay(new Vector2(PlayerRigidbody.position.x, PlayerRigidbody.position.y), Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHitUnpassable = Physics2D.Raycast(PlayerRigidbody.position, Vector3.down, 0.9f, LayerMask.GetMask("UnPassablePlatform"));
        if (rayHitUnpassable.collider != null)
        {
            onPassablePlatform = false; 
        }
        else
        {
            onPassablePlatform = true;
        }

        RaycastHit2D rayHit = Physics2D.Raycast(PlayerRigidbody.position, Vector3.down, 0.9f, LayerMask.GetMask("Platform", "UnPassablePlatform"));
        if (rayHit.collider!=null && !isDirectionDoomed)
        {
            anim.SetBool("IsFloat", false);
            isDoubleJumpUsed = false;
            anim.SetBool("IsDoubleJumpUsed", false);
        }
        else
        {
            anim.SetBool("IsFloat", true);
        }
    }

    void FixedUpdate()
    {
        if (!frozen)
        PlayerRigidbody.AddForce(new Vector2(speed * horizontalInput, 0f)); //speed 20
    }

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && (PlayerRigidbody.velocity.y < 0.2))
        {
            isDirectionDoomed = false;
            Debug.Log("wwww");
        }
    }

    public void autoJump()
    {
        audioSource.PlayOneShot(jumpSound);
        PlayerRigidbody.AddForce(new Vector2(0f, jump));
    }

    public void autoUpJump()
    {
        PlayerRigidbody.AddForce(new Vector2(0f, jump * 2f));
        anim.SetTrigger("UpJumpTrigger");   
    }

    public void autoDoubleJump(float period)
    {
        StartCoroutine(autoDoubleJumpInumerator(period));
    }

    public IEnumerator autoDoubleJumpInumerator(float period)
    {
        audioSource.PlayOneShot(jumpSound);
        PlayerRigidbody.AddForce(new Vector2(0f, jump));

        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(period);
            isDoubleJumpUsed = true;
            anim.SetBool("IsDoubleJumpUsed", true);
            int dir = 1;
            if (!isRight) dir = -1;
            PlayerRigidbody.AddForce(new Vector2(dir * 300f, jump * 0.5f));
        }
    }

    public void autoWalk(float duration, bool isRight)
    {
        spriteRenderer.flipX = !isRight;
        StartCoroutine(autoWalkInumerator(duration, isRight));
    }


    public IEnumerator autoWalkInumerator(float duration, bool isRight)
    {
        spriteRenderer.flipX = !isRight;
        float dir = -1;
        anim.SetBool("IsWalking", true);
        for (float i = 0; i < duration; i = i + 0.02f)
        {
            if (isRight) dir = 1;
            
            if (Mathf.Abs(PlayerRigidbody.velocity.x) >= 5)
            {
                dir = 0f;
            }
            PlayerRigidbody.AddForce(new Vector2(speed * dir, 0f));
            yield return new WaitForSeconds(0.02f);
        }
        anim.SetBool("IsWalking", false);
    }

    public IEnumerator passObjectInumerator(float duration)
    {
        PlayerBoxCollider.enabled = false;
        isPassingObject = true;
        PlayerRigidbody.AddForce(new Vector2(0f, -500f));
        yield return new WaitForSeconds(duration);

        PlayerBoxCollider.enabled = true;
        isPassingObject = false;
        anim.SetBool("IsDown", false);
        jump = defaultJump;
        speed = defaultSpeed;
        PlayerRigidbody.drag = defaultLinearDrag;
    }

    public void freeze(){
        frozen = true;
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsFloat", false);
        anim.SetBool("IsDoubleJumpUsed", false);
        anim.SetBool("IsDown", false);

    }
    public void unfreeze(){
        frozen = false;
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
