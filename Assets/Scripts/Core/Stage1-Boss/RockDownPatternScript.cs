using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDownPatternScript : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> dustList = new List<GameObject>();
    public float xCorrection;
    public float yCorrection;
    public float bikeRunTime;
    public float bikeVelocity;
    public GameObject minkyu;
    public AudioSource audioSource;
    public AudioClip bikeSound;

    public bool IsAggressive = false;

    public int BikeDamage = 100;


    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        bikeSound = Resources.Load("Sound/Voice/bikeSound") as AudioClip;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsAggressive && other.gameObject.CompareTag("PlayerHitbox"))
        {
            other.gameObject.GetComponent<HealthScript>().Hit(BikeDamage);
        }
    }

    public void RockDown(float termMin, float termMax)
    {
        StartCoroutine(BikeRunAct(bikeRunTime, bikeVelocity, termMin, termMax));
    }

    IEnumerator BikeRunAct(float bikeRunTime, float bikeVelocity, float termMin, float termMax)
    {   
        transform.position = new Vector3(minkyu.transform.position.x - 10f, transform.position.y, transform.position.z);
        transform.GetComponent<SpriteRenderer>().color = Color.white;
        transform.GetComponent<Rigidbody2D>().velocity = new Vector2(bikeVelocity, 0f);
        IsAggressive = true;

        audioSource.PlayOneShot(bikeSound);

        IEnumerator RockDownInstance = RockDownAct(termMin, termMax);
        StartCoroutine(RockDownInstance);

        yield return new WaitForSeconds(bikeRunTime);

        StopCoroutine(RockDownInstance);
        IsAggressive = false;
        transform.GetComponent<SpriteRenderer>().color = Color.clear;
        transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
    }

    IEnumerator RockDownAct(float termMin, float termMax)
    {
        while (true)
        {
            float waitTime = Random.Range(termMin, termMax);
            yield return new WaitForSeconds(waitTime);
            
            int index = Random.Range(0, dustList.Count);
            var RealDust = Instantiate(dustList[index], new Vector3(transform.position.x + xCorrection, transform.position.y + yCorrection, transform.position.z), Quaternion.identity);
            RealDust.transform.parent = gameObject.transform.parent.transform;
        }
    }
}
