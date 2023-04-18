using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneRoomCameraScript : MonoBehaviour
{
    public GameObject Target;
    public static bool CameraGameMode;
    // Start is called before the first frame update
    void Start()
    {
        CameraGameMode = true;
        if (Target.activeSelf)
        transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y + 4.5f, -10);
        //Screen.SetResolution(1280,720,true);
    }
}
