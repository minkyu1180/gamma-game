using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkSpiritScript : MonoBehaviour
{
    public GameObject spiritUI;
    void Start()
    {
        spiritUI = GameObject.Find("SpiritUI");
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerHitbox"))
        {
            spiritUI.GetComponent<SpiritManagerScript>().getPink();
            Destroy(gameObject);
        }        
    }
}
