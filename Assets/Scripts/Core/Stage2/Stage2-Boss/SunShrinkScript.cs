using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunShrinkScript : MonoBehaviour
{
    public bool wait = true;
    public float shrinkRate = 0.999f;

    void FixedUpdate()
    {
        if (!wait) transform.localScale = transform.localScale * shrinkRate;
        if (transform.localScale.x <= 0.01f) Destroy(gameObject);     
    }
}
