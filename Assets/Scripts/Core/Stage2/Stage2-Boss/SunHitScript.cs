using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunHitScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject minkyuHitbox;
    private PlusSunControllerScript plusSunControllerScript;
    public Vector3 movedirection;
    bool isCountDownActivated = false;
    public float existDuration = 10f;
    public float sunSpeed = 1f;
    public float distance = 3f;
    public Vector3 offset = new Vector3(0f, -0.4f, 0f);
    public int damage = 30;

    void Start()
    {
        minkyuHitbox = GameObject.Find("hitbox");
        plusSunControllerScript = transform.parent.gameObject.GetComponent<PlusSunControllerScript>();
        transform.position = minkyuHitbox.transform.position + (-distance * movedirection) + offset;

    }

    void Update()
    {
        if (!plusSunControllerScript.sunGo)
        {
            transform.position = minkyuHitbox.transform.position + (-distance * movedirection) + offset;
        }
        else
        {
            if (!isCountDownActivated){
                StartCoroutine(destroyItselfAfter(existDuration));
                gameObject.GetComponent<Rigidbody2D>().velocity = (new Vector2(movedirection.x, movedirection.y) * sunSpeed);
                isCountDownActivated = true;
            } 
        }
    }

    IEnumerator destroyItselfAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "PlayerHitbox")
        {
            minkyuHitbox.GetComponent<HealthScript>().Hit(damage);
            Destroy(gameObject);
        }
    }
}
