using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunEndNotificator : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Sun")
        {
            transform.GetComponentInParent<SunMovementScript>().endPointAttached = true;
        }
    }
}
