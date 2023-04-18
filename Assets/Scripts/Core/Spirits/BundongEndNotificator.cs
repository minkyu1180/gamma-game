using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundongEndNotificator : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bundong")
        {
            if (transform.IsChildOf(other.transform.parent))
            {
                other.transform.parent.GetComponent<BunDongMovementScript>().endPointAttached = true;
            }
        }
    }
    
}
