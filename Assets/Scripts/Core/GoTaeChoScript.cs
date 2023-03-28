using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoTaeChoScript : MonoBehaviour
{
    public GameObject StartPoint;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerHitbox"))
        {
            StartCoroutine(WaitWithPainAndRevive(other));
            
            
            //other.attachedRigidbody.position = StartPoint.GetComponent<Rigidbody2D>().position;
            //Hp 감소
        }
        
    }

    IEnumerator WaitWithPainAndRevive(Collider2D other)
    {
        Time.timeScale = 0.05f;
        yield return new WaitForSecondsRealtime(0.35f);
        Time.timeScale = 1.0f;
        other.gameObject.GetComponent<HealthScript>().Hit(100);
        other.gameObject.transform.parent.GetComponent<Rigidbody2D>().position = StartPoint.GetComponent<Rigidbody2D>().position;
        other.gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
        
    }
}
