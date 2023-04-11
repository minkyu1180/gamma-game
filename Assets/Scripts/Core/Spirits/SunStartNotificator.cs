using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunStartNotificator : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sun")
        {
            transform.GetComponentInParent<SunMovementScript>().startPointAttached = true;
        }
    }
}
