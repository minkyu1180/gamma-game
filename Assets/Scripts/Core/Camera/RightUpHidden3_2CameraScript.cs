using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightUpHidden3_2CameraScript : MonoBehaviour
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
        transform.position = new Vector3(Target.transform.position.x + 2.0f, Target.transform.position.y + 1.5f, -10);

        if (transform.position.x >= -18f)
        transform.position = new Vector3(Target.transform.position.x + 2.0f, 0.3475184f, -10); //attach to ground
    }
}
