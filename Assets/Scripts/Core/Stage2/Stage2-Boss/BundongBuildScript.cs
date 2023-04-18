using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundongBuildScript : MonoBehaviour
{
    private GameObject minkyuHitbox;
    public PlusBundongControllerScript plusBundongControllerScript;
    bool isCountDownActivated = false;
    public float existDuration = 10f;
    public float distance = 3f;
    public Vector3 movedirection = new Vector3(1f, 0f, 0f);
    public Vector3 offset = new Vector3(0f, -0.4f, 0f);
    public int damage = 30;

    void Start()
    {
        minkyuHitbox = GameObject.Find("hitbox");
        plusBundongControllerScript = transform.parent.GetComponent<PlusBundongControllerScript>();
        transform.position = minkyuHitbox.transform.position + (-distance * movedirection) + offset;

    }

    void Update()
    {
        if (plusBundongControllerScript.bundongMove)
        {
            transform.position = minkyuHitbox.transform.position + (-distance * movedirection) + offset;
        }
        else
        {
            if (!isCountDownActivated){
                StartCoroutine(destroyItselfAfter(existDuration));
                isCountDownActivated = true;
            } 
        }
    }

    IEnumerator destroyItselfAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

}
