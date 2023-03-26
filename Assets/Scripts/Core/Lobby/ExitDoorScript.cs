using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitDoorScript : MonoBehaviour
{
    public GameObject MapChooseUI;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetAxisRaw("Vertical") == 1)
        {
            if (other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1f)
            {
                MapChooseUI.SetActive(true);
            }
        }
        if (other.gameObject.CompareTag("Player") && Input.GetAxisRaw("Vertical") == -1)
        {
            MapChooseUI.SetActive(false);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MapChooseUI.SetActive(false);
        }
    }
}
