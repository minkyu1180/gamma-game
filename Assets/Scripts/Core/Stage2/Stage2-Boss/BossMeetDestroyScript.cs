using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeetDestroyScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Boss")) Destroy(gameObject);
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Boss")) Destroy(gameObject);
    }
}
