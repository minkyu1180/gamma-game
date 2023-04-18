using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritManagerScript : MonoBehaviour
{
    private bool HasGreen = false;
    private bool HasPink = false;
    private bool HasOrange = false;
    public bool IsBossStage = true;

    public GameObject pinkSpirit;
    public GameObject orangeSpirit;
    public GameObject greenSpirit;
    public GameObject attackEffect;

    public GameObject RealPinkSpirit = null;
    public GameObject RealOrangeSpirit = null;
    public GameObject RealGreenSpirit = null;


    private AudioClip spiritAttackAudioClip;
    private AudioSource audioSource;
    public GameObject minkyu;

    void Start()
    {
        //transform.GetChild(0).gameObject.SetActive(false);
        //transform.GetChild(1).gameObject.SetActive(false);
        //transform.GetChild(2).gameObject.SetActive(false);
        minkyu = GameObject.Find("Minkyu");

        audioSource = gameObject.GetComponent<AudioSource>();
        spiritAttackAudioClip = Resources.Load("Sound/Voice/spiritAttackSound") as AudioClip;

        if (IsBossStage) spiritSpread();
    }

    // Update is called once per frame
    void Update()
    {
        if (HasGreen && HasPink && HasOrange && IsBossStage)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                spiritAttack();
            }
        }
    }

    public bool checkHasAllSpirits()
    {
        return HasGreen && HasPink && HasOrange;
    }

    public void getGreen()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        HasGreen = true;
    }
    public void getPink()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        HasPink = true;
    }
    public void getOrange()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        HasOrange = true;
    }

    public void getRidOfAll() //boss also use this func
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        HasGreen = false;
        HasPink = false;
        HasOrange = false;
    }

    private void spiritAttack()
    {
        Instantiate(attackEffect, Vector3.zero, Quaternion.identity);
        audioSource.PlayOneShot(spiritAttackAudioClip);
        getRidOfAll();
        spiritSpread();
    }



/*
Pink (-81.81, 1.23) (-80.31, 4.02) (-81, 6.49)

Green(-73.84,4.56) (-73.84, 1.28)

Orange(-64.55, 1.4) (-67.03, 3.84) (-66.05, 6.43)
*/
    public void spiritSpread() //After get rid of All Existing spirits
    {
        int choose = 0;
        choose = Random.Range(0,3);
        if (RealPinkSpirit != null) Destroy(RealPinkSpirit);
        if (choose == 0) RealPinkSpirit = Instantiate(pinkSpirit, new Vector3(-81.81f, 1.23f, 0f), Quaternion.identity);
        else if (choose == 1) RealPinkSpirit = Instantiate(pinkSpirit, new Vector3(-80.31f, 4.02f, 0f), Quaternion.identity);
        else if (choose == 2) RealPinkSpirit = Instantiate(pinkSpirit, new Vector3(-81f, 6.49f, 0f), Quaternion.identity);
        

        choose = Random.Range(0,2);
        if (RealGreenSpirit != null) Destroy(RealGreenSpirit);
        if (choose == 0) RealGreenSpirit = Instantiate(greenSpirit, new Vector3(-73.84f, 4.56f, 0f), Quaternion.identity);
        else if (choose == 1) RealGreenSpirit = Instantiate(greenSpirit, new Vector3(-73.84f, 1.28f, 0f), Quaternion.identity);

        choose = Random.Range(0,3);
        if (RealOrangeSpirit != null) Destroy(RealOrangeSpirit);
        if (choose == 0) RealOrangeSpirit = Instantiate(orangeSpirit, new Vector3(-64.55f, 1.4f, 0f), Quaternion.identity);
        else if (choose == 1) RealOrangeSpirit = Instantiate(orangeSpirit, new Vector3(-67.03f, 3.84f, 0f), Quaternion.identity);
        else if (choose == 2) RealOrangeSpirit = Instantiate(orangeSpirit, new Vector3(-66.05f, 6.43f, 0f), Quaternion.identity);

    }

}
