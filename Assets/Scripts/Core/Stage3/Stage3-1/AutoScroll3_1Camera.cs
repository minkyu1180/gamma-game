using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScroll3_1Camera : MonoBehaviour
{
    public GameObject Target;
    public static bool CameraGameMode;
    public Vector3 InitPosition;
    public bool CameraInit = false;
    Vector2 firstDirection = new Vector2(2.0f, 0f);
    bool firstMove = false;
    bool secondMove = false;
    bool thirdMove = false;
    bool hiddenMove = false;
    Vector2 secondDirection = new Vector2(1.3f, 0.4f);
    Vector2 thirdDirection = new Vector2(1.8f, 0f);
    Vector2 upDirection = new Vector2(0f, 5.0f);
    Coroutine upCoroutine = null;
    // Start is called before the first frame update
    void Start()
    {
        CameraGameMode = true;
        Target = GameObject.Find("Minkyu");
        InitPosition = new Vector3(Target.transform.position.x + 2.3f, -27.8f , transform.position.z);
        CameraInit = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = firstDirection;
        //Screen.SetResolution(1280,720,true);
    }

    // Update is called once per frame
    void Update()
    {
        //in game Camera
        //if (Target.activeSelf) // if Camera enabled

        if (CameraInit)
        {
            if (upCoroutine != null) StopCoroutine(upCoroutine);
            transform.position = InitPosition;
            gameObject.GetComponent<Rigidbody2D>().velocity = firstDirection;
            CameraInit = !CameraInit;
            firstMove = false;
            secondMove = false;
            thirdMove = false;
            hiddenMove = false;
        }

        if (transform.position.x > 7.4 && !firstMove)
        {
            firstMove = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = secondDirection;
        }

        if (transform.position.x > 29f && !secondMove)
        {
            secondMove = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = thirdDirection;
        }

        if (transform.position.x > 80f && !thirdMove)
        {
            thirdMove = true;
            hiddenMove = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (Target.transform.position.y > -13.5f && Target.transform.position.x > 64f && !hiddenMove)
        {
            thirdMove = true;
            hiddenMove = true;
            upCoroutine = StartCoroutine(GoUpFor(2.0f));
        }
    }

    IEnumerator GoUpFor(float duration)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = upDirection;
        yield return new WaitForSeconds(duration);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
