using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTouchedScript : MonoBehaviour
{
    GameObject Camera;
    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("MainCamera");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Camera.GetComponent<AutoScroll3_2Camera>().portalTouched = true;
        }        
    }
}
