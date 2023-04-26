using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoTaeCho3_2AutoScrollScript : MonoBehaviour
{
    public bool hitting = false;
    public GameObject Camera;
    public GameObject StartPoint;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerHitbox") && !hitting)
        {
            StartCoroutine(WaitWithPainAndRevive(other));
            
            
            //other.attachedRigidbody.position = StartPoint.GetComponent<Rigidbody2D>().position;
            //Hp 감소
        }
        
    }

    public IEnumerator WaitWithPainAndRevive(Collider2D other)
    {
        hitting = true;
        Time.timeScale = 0.05f;
        other.gameObject.GetComponent<HealthScript>().Hit(10);
        yield return new WaitForSecondsRealtime(0.35f);
        Time.timeScale = 1.0f;
        other.gameObject.transform.parent.GetComponent<Rigidbody2D>().position = StartPoint.GetComponent<Rigidbody2D>().position;
        other.gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
        hitting = false;
        Camera.GetComponent<AutoScroll3_2Camera>().CameraInit = true;
    }
}
