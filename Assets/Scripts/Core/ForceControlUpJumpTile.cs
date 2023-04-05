using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ForceControlUpJumpTile : MonoBehaviour
{

    private Tilemap tiles;
    private Vector3Int targetPos;
    private PlatformEffector2D effector;
    private GameObject minkyu;
    private GridLayout gridLayout;
    public float force;


    void Start()
    {
        tiles = gameObject.GetComponent<Tilemap>();
        gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        minkyu = GameObject.Find("Minkyu");
    }


    void OnCollisionStay2D(Collision2D other)
    {
        targetPos = gridLayout.WorldToCell(minkyu.transform.position);
        if (tiles.HasTile(targetPos + Vector3Int.down)||
        tiles.HasTile(targetPos + Vector3Int.down + Vector3Int.left) ||
        tiles.HasTile(targetPos + Vector3Int.down + Vector3Int.right)
        )
        {   
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.rigidbody.velocity.y <= 0.1f)
                {   
                    other.rigidbody.velocity = new Vector3(other.rigidbody.velocity.x, 0, 0);
                    other.rigidbody.AddForce(new Vector2(0f,force));
                    PlayerMovement PlayerScript;
                    PlayerScript = other.gameObject.GetComponent<PlayerMovement>();
                    //PlayerScript.isDirectionDoomed = true;
                    //if (PlayerScript.horizontalInput > 0f) PlayerScript.beforeJumpInertia = 1f;
                    //else if (PlayerScript.horizontalInput < 0f) PlayerScript.beforeJumpInertia = -1f;
                    //else PlayerScript.beforeJumpInertia = 0f;
                }
            }
        }
    }    
       
}

