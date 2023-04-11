using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunDongMovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Bundong;
    private GameObject BundongTop;
    private GameObject BundongStartPoint;
    private GameObject BundongEndPoint;

    public Vector3 directionVector;
    
    public float goSpeed;
    public float comeSpeed;

    public Sprite BundongDefaultSprite;
    public Sprite BundongAggrovateSprite;

    public float detectDistance = 9f;

    public bool endPointAttached = false;
    public bool startPointAttached = false;

    private bool IsAggrovated = false;

    void Start()
    {
        Bundong = transform.GetChild(3).gameObject;
        BundongEndPoint = transform.GetChild(1).gameObject;
        BundongStartPoint = transform.GetChild(0).gameObject;
        BundongTop = transform.GetChild(2).gameObject;
        

    }

    void Update()
    {
        if (!IsAggrovated){
            Debug.DrawRay(new Vector2(Bundong.transform.position.x, Bundong.transform.position.y), directionVector * detectDistance, new Color(0, 1, 0));
            RaycastHit2D rayHitUnpassable = Physics2D.Raycast(new Vector2(Bundong.transform.position.x, Bundong.transform.position.y), directionVector, detectDistance, LayerMask.GetMask("Player"));
            if (rayHitUnpassable.collider != null && rayHitUnpassable.collider.tag == "Player")
            {
                IsAggrovated = true;
                startPointAttached = false;
                endPointAttached = false;
                Bundong.GetComponent<SpriteRenderer>().sprite = BundongAggrovateSprite;
                StartCoroutine(Rush());
            }
        }
    }

    IEnumerator Rush()
    {
        Bundong.GetComponent<Rigidbody2D>().velocity = directionVector * goSpeed;
        BundongTop.GetComponent<Rigidbody2D>().velocity = directionVector * goSpeed;
        yield return new WaitWhile(() => !endPointAttached);
        Bundong.GetComponent<SpriteRenderer>().sprite = BundongDefaultSprite;

        Bundong.GetComponent<Rigidbody2D>().velocity = (directionVector * -comeSpeed);
        BundongTop.GetComponent<Rigidbody2D>().velocity = (directionVector * -comeSpeed);
        yield return new WaitWhile(() => !startPointAttached);
        Bundong.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        BundongTop.GetComponent<Rigidbody2D>().velocity = Vector3.zero;        
        IsAggrovated = false;
    }
}
