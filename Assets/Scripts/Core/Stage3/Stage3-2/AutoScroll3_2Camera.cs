using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScroll3_2Camera : MonoBehaviour
{
    public GameObject Target;
    public bool CameraGameMode;
    public Vector3 InitPosition;
    public bool CameraInit = false;
    Vector2 firstDirection = new Vector2(0f, 1f);
    bool firstMove = false;
    bool scrollFinished = false;
    Coroutine upCoroutine = null;
    public bool portalTouched = false;
    // Start is called before the first frame update
    void Start()
    {
        //CameraGameMode = true;
        Target = GameObject.Find("Minkyu");
        InitPosition = new Vector3(-33.5f, -27.8f , transform.position.z);
        transform.position = InitPosition;
        gameObject.GetComponent<Rigidbody2D>().velocity = firstDirection;
        CameraInit = true;
        //Screen.SetResolution(1280,720,true);
    }

    // Update is called once per frame
    void Update()
    {
        //in game Camera
        //if (Target.activeSelf) // if Camera enabled

        if (CameraGameMode && CameraInit)
        {
            if (upCoroutine != null) StopCoroutine(upCoroutine);
            transform.position = InitPosition;
            gameObject.GetComponent<Rigidbody2D>().velocity = firstDirection;
            CameraInit = !CameraInit;
            firstMove = false;
            scrollFinished = false;
            portalTouched = false;
        }

        if (!scrollFinished && portalTouched)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = firstDirection * 5; 
        }



        if (transform.position.y > 32f && !firstMove)
        {
            firstMove = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            scrollFinished = true;
        }

        if (!CameraGameMode)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
