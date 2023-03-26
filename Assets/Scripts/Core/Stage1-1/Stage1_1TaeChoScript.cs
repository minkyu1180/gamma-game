using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Stage1_1TaeChoScript : MonoBehaviour
{
    public GameObject StartPoint;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.attachedRigidbody.velocity = new Vector2(0f,0f);
            other.attachedRigidbody.position = StartPoint.GetComponent<Rigidbody2D>().position;
            //Hp 감소
        }
        
    }
}
