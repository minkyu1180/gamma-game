using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointHideScript : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
    }
}
