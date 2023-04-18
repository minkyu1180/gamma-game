using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplySunControllerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public bool sunGo = false;
    public float waitDuration = 2.0f;
    void Start()
    {
        StartCoroutine(sunGoTrueAfterWhile(waitDuration));
    }

    IEnumerator sunGoTrueAfterWhile(float duration)
    {
        yield return new WaitForSeconds(duration);
        sunGo = true;
    }
}
