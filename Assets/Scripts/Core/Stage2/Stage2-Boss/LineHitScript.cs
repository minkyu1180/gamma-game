using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineHitScript : MonoBehaviour
{
    // Start is called before the first frame update
    private EdgeCollider2D edgeCollider2D;
    private HealthScript healthScript;
    public bool active = false;
    public int damage = 30;

    void Start()
    {
        edgeCollider2D = gameObject.GetComponent<EdgeCollider2D>();
        healthScript = GameObject.Find("hitbox").GetComponent<HealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            StartCoroutine(destroyItselfAfter(0.1f));
        }
    }
    IEnumerator destroyItselfAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "PlayerHitbox" && active)
        {
            healthScript.Hit(damage);
            //Hit
            edgeCollider2D.enabled = false;
        }
    }
}
