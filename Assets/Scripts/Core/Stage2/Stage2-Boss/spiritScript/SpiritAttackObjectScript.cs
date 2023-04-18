using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritAttackObjectScript : MonoBehaviour
{
    public GameObject minkyu;
    public GameObject boss;
    void Start()
    {
        minkyu = GameObject.Find("Minkyu");
        boss = GameObject.Find("Boss");
        StartCoroutine(DestroyItselfAfterWhile(0.5f));
    }

    IEnumerator DestroyItselfAfterWhile(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = minkyu.transform.position;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Boss")
        {
            boss.GetComponent<SusangPatternScript>().sufferingByDamage = true;
            //gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        }        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Boss")
        {
            boss.GetComponent<SusangPatternScript>().sufferingByDamage = false;
        }
    }


    void OnDestroy()
    {
        boss.GetComponent<SusangPatternScript>().sufferingByDamage = false;
    }
}
