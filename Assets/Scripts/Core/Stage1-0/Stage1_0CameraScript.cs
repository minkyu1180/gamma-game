using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_0CameraScript : MonoBehaviour
{
    public GameObject Target;
    public static bool CameraGameMode;
    // Start is called before the first frame update
    void Start()
    {
        CameraGameMode = true;
        //Screen.SetResolution(1280,720,true);
    }

    // Update is called once per frame
    void Update()
    {
        //in game Camera
        if (Target.activeSelf) // if Camera enabled
        transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, -10);
    }
}
