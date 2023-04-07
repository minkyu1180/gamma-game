using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustScript : MonoBehaviour, IDataPersistence
{
    public float force;
    public int type;
    public GameObject minkyu;
    public int Damage1 = 50;
    public int Damage2 = 80;
    public int Damage3 = 100;

    //public AudioSource audioSource;
    //public AudioClip rockSound;


    public bool didClearStage1;

    public void LoadData(GameData data)
    {
        this.didClearStage1 = data.didClearStage1;
    }
    public void SaveData(ref GameData data){}

    // Start is called before the first frame update
    void Start()
    {
        float xVel = Random.Range(-100f, 0f);
        transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(xVel, force));

        //audioSource = gameObject.GetComponent<AudioSource>();
        //rockSound = Resources.Load("Sound/Voice/rockSound") as AudioClip;
        //audioSource.pitch = Random.Range(0.5f, 0.6f);
        //audioSource.PlayOneShot(rockSound);
        if (didClearStage1)
        {
            Damage1 = 80;
            Damage2 = 110;
            Damage3 = 150;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerHitbox"))
        {
            if (type == 1)      other.gameObject.GetComponent<HealthScript>().Hit(Damage1);
            else if (type == 2) other.gameObject.GetComponent<HealthScript>().Hit(Damage2);
            else if (type == 3) other.gameObject.GetComponent<HealthScript>().Hit(Damage3);
        }
    }
}
