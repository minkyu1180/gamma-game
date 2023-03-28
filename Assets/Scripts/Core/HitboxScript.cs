using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.parent.GetComponent<PlayerMovement>().anim.GetBool("IsDown"))
        {
            gameObject.GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Horizontal;
            gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0.02036301f, -0.6735954f);
            gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(1.346406f, 0.3002559f);
        }
        else
        {
            gameObject.GetComponent<CapsuleCollider2D>().direction = CapsuleDirection2D.Vertical;
            gameObject.GetComponent<CapsuleCollider2D>().offset = new Vector2(0.05163574f, 0.01489794f);
            gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(0.3967285f, 1.498405f);
        }
    }
}
