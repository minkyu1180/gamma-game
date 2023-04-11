using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundongStartNotificator : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bundong")
        {
            if (transform.IsChildOf(other.transform.parent))
            {
                other.transform.parent.GetComponent<BunDongMovementScript>().startPointAttached = true;
            }
        }
    }
    
}
