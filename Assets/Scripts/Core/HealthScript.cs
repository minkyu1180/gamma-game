using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    private static int hp;
    public bool IsFirstStage;
    public GameObject HealthBar;

    public void Hit(int damage)
    {
        hp = hp - damage;
        if (hp < 0) hp = 0;
        HealthBar.GetComponent<Slider>().value = hp;
        if (hp <= 0)
        {
            Debug.Log("DIE");
            //go to home (heal anyway)
        }
    }

    public void Heal(int healAmount)
    {
        hp = hp + healAmount;
        if (hp > 1000) hp = 1000;
        HealthBar.GetComponent<Slider>().value = hp;
    }

    void OnDisable()
    {
        PlayerPrefs.SetInt("HP", hp);
    }
    void OnEnable()
    {
        if (IsFirstStage)
        {
            hp = 1000;
            PlayerPrefs.SetInt("HP", 1000);
            HealthBar.GetComponent<Slider>().value = hp;
        }
        else
        {
            if (PlayerPrefs.HasKey("HP"))
            hp = PlayerPrefs.GetInt("HP");
            else
            hp = 1000;
            HealthBar.GetComponent<Slider>().value = hp;
        }
    }   
}
