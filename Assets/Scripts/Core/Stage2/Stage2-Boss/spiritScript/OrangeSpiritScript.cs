using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeSpiritScript : MonoBehaviour
{
    public GameObject spiritUI;
    void Start()
    {
        spiritUI = GameObject.Find("SpiritUI");
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("PlayerHitbox"))
        {
            spiritUI.GetComponent<SpiritManagerScript>().getOrange();
            Destroy(gameObject);
        }        
    }
}
