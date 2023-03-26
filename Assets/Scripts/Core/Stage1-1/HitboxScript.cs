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
        if (!gameObject.GetComponentInParent<PlayerMovement>().isRight)
        {
            //transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
