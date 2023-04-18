using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    public float speed;
    public float jump;
    public float horizontalInput;
    public float beforeJumpInertia; // 1f, 0f, -1f

    private float defaultSpeed = 20f;
    private float defaultJump = 450f;
    private float defaultLinearDrag = 0.3f;

    public bool doubleJumpEnabled;
    public bool glideEnabled;
    public bool highJumpEnabled;
    public bool frozen;

    public bool isDirectionDoomed;
    public bool isDoubleJumpUsed;
    public bool isPassingObject;
    public bool onDownJumpAblePlatform;
    public bool isRight;

    private Rigidbody2D PlayerRigidbody;
    private BoxCollider2D PlayerBoxCollider;
    private SpriteRenderer spriteRenderer;
    public Animator anim;

    AudioSource audioSource;
    private AudioClip jumpSound;
    private AudioClip upJumpSound;
    private AudioClip doubleJumpSound;


    public void LoadData(GameData data)
    {
        this.doubleJumpEnabled = data.didClearStage1;
        this.glideEnabled = data.didClearStage2;
        this.highJumpEnabled = data.didClearStage3;
    }

    public void SaveData(ref GameData data){}

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
        onDownJumpAblePlatform = false;
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        PlayerBoxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        beforeJumpInertia = 0f;

        audioSource = GetComponent<AudioSource>();
        jumpSound = Resources.Load("Sound/Voice/jumpSound") as AudioClip;
        upJumpSound = Resources.Load("Sound/Voice/upJumpSound") as AudioClip;
        doubleJumpSound = Resources.Load("Sound/Voice/doubleJumpSound") as AudioClip;
    }

    void Update()
    {

        if (anim.GetBool("IsFloat") && PlayerRigidbody.velocity.y == 0.0f)
        {
            StartCoroutine(grounder());
            // in case of halt
        }

        if (!anim.GetBool("IsDown"))
        {
            PlayerBoxCollider.offset = new Vector2(0.01754826f, 0.003339767f);
            PlayerBoxCollider.size = new Vector2(0.9338287f, 1.671751f);
            
        }
        else
        {
            PlayerBoxCollider.size = new Vector2(1.508463f, 0.8153801f);
            PlayerBoxCollider.offset = new Vector2(0.001412272f, -0.4338576f);
        }

        if (!frozen)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            if (Input.GetButton("Horizontal"))
            {
                //spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
                if (Input.GetAxisRaw("Horizontal") == -1)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
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

            
            // glide Start

            if (glideEnabled && (Input.GetAxisRaw("Vertical") == 1) && anim.GetBool("IsFloat") && PlayerRigidbody.velocity.y <= 0)
            {
                PlayerRigidbody.gravityScale = 1f;
                anim.SetBool("IsGlide", true);
            }
            else
            {
                PlayerRigidbody.gravityScale = 3.0f;
                anim.SetBool("IsGlide", false);
            }

            // glide End


            // && (Mathf.Abs(PlayerRigidbody.velocity.y) <= 0.01)
            if (!Input.GetButton("Horizontal") && (Input.GetAxisRaw("Vertical") == -1) && !anim.GetBool("IsFloat"))
            {
                if (!anim.GetBool("IsDown"))
                {
                    anim.SetBool("IsDown", true);
                    jump = 0f;
                    speed = 0f;
                    //PlayerRigidbody.drag = 8.0f;
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
            
            if (Input.GetButtonDown("Jump") && !anim.GetBool("IsFloat") && anim.GetBool("IsDown") && onDownJumpAblePlatform)
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
                    audioSource.PlayOneShot(upJumpSound);
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
                audioSource.PlayOneShot(doubleJumpSound);
                int dir = 1;
                if (!isRight) dir = -1;
                PlayerRigidbody.AddForce(new Vector2(dir * 300f, jump * 0.5f));
            }
            
        }

        //Debug.DrawRay(new Vector2(PlayerRigidbody.position.x, PlayerRigidbody.position.y - 0.8f), new Vector3(0f, -0.4f, 0), new Color(0, 1, 0));
        RaycastHit2D rayHitUnpassable = Physics2D.Raycast(new Vector2(PlayerRigidbody.position.x, PlayerRigidbody.position.y -0.8f), Vector3.down, 0.4f, LayerMask.GetMask("DownJumpAblePlatform"));
        if (rayHitUnpassable.collider != null)
        {
            onDownJumpAblePlatform = true; 
        }
        else
        {
            onDownJumpAblePlatform = false;
        }


        //Debug.DrawRay(new Vector2(PlayerRigidbody.position.x, PlayerRigidbody.position.y - 0.8f), new Vector3(0.7f, 0f, 0f), new Color(0, 1, 0));
        //Debug.DrawRay(new Vector2(PlayerRigidbody.position.x, PlayerRigidbody.position.y - 0.8f), new Vector3(0f, -0.4f, 0), new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(new Vector2(PlayerRigidbody.position.x, PlayerRigidbody.position.y -0.8f), Vector3.down, 0.4f, LayerMask.GetMask("Platform", "UnPassablePlatform", "DownJumpAblePlatform"));
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

    public IEnumerator grounder()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.05f);
            if (PlayerRigidbody.velocity.y != 0f) break;
            if (i == 4)
            {
                isDirectionDoomed = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (!frozen)
        PlayerRigidbody.AddForce(new Vector2(speed * horizontalInput, 0f)); //speed 20
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //&& (PlayerRigidbody.velocity.y < 0.2)
        if (other.gameObject.CompareTag("Ground") && (PlayerRigidbody.velocity.y < 0.2))
        {
            isDirectionDoomed = false;
        }

        if (other.gameObject.CompareTag("Bundong"))
        {
            isDirectionDoomed = false;
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
        if (!isRight) transform.localScale = new Vector3(-1, 1, 1);
        else transform.localScale = new Vector3(1,1,1);
        StartCoroutine(autoWalkInumerator(duration, isRight));
    }

    public void autoDown()
    {
        anim.SetBool("IsDown", true);
    }

    public void autoUnDown()
    {
        anim.SetBool("IsDown", false);
    }

    public IEnumerator autoWalkInumerator(float duration, bool isRight)
    {
        if (!isRight) transform.localScale = new Vector3(-1, 1, 1);
        else transform.localScale = new Vector3(1,1,1);        
        
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
    public void unFreeze(){
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
