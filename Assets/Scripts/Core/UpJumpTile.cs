using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UpJumpTile : MonoBehaviour
{

    private Tilemap tiles;
    private Vector3Int targetPos;
    private PlatformEffector2D effector;
    private GameObject minkyu;
    private GridLayout gridLayout;

    public float jumpForce = 1300f;


    void Start()
    {
        tiles = gameObject.GetComponent<Tilemap>();
        gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        minkyu = GameObject.Find("Minkyu");
    }


    void OnCollisionStay2D(Collision2D other)
    {
        targetPos = gridLayout.WorldToCell(minkyu.transform.position);
        if (tiles.HasTile(targetPos + Vector3Int.down))
        {   
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.rigidbody.velocity.y <= 0.1f && !other.gameObject.GetComponent<Animator>().GetBool("IsFloat"))
                {   
                    other.rigidbody.AddForce(new Vector2(0f,jumpForce));
                    PlayerMovement PlayerScript;
                    PlayerScript = other.gameObject.GetComponent<PlayerMovement>();
                    PlayerScript.isDirectionDoomed = true;
                    if (PlayerScript.horizontalInput > 0f) PlayerScript.beforeJumpInertia = 1f;
                    else if (PlayerScript.horizontalInput < 0f) PlayerScript.beforeJumpInertia = -1f;
                    else PlayerScript.beforeJumpInertia = 0f;
                }
            }
        }
    }    
       
}

