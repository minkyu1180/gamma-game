using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusBundongControllerScript : MonoBehaviour
{
    public bool bundongMove = true;
    public float waitDuration = 2.0f;
    void Start()
    {
        StartCoroutine(sunGoTrueAfterWhile(waitDuration));
    }

    IEnumerator sunGoTrueAfterWhile(float duration)
    {
        yield return new WaitForSeconds(duration);
        bundongMove = false;
    }

    void Update()
    {
        if (transform.childCount == 0) Destroy(gameObject);
    }
}
