using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1280,720,true);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Target.transform.position.x, transform.position.y, -10);
    }
}
