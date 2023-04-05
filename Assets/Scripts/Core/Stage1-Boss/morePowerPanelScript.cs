using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class morePowerPanelScript : MonoBehaviour
{
    public Sprite feverSprite;
    public AudioSource audioSource;
    public AudioClip feverSound;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        feverSound = Resources.Load("Sound/Voice/bikeFeverSound") as AudioClip;
    }    
    
    public void feverNotification()
    {
        //audioSource.PlayOneShot(feverSound);
        gameObject.GetComponent<SpriteRenderer>().sprite = feverSprite;
        StartCoroutine(DestroyItSelf());
    }

    IEnumerator DestroyItSelf()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }

}
