using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpJumpTile : MonoBehaviour
{
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.rigidbody.velocity.y <= 0.1f && !other.gameObject.GetComponent<Animator>().GetBool("IsFloat"))
            {
                other.rigidbody.AddForce(new Vector2(0f,1300f));
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

