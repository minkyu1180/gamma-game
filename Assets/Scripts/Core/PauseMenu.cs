using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject Panel; 
    // Start is called before the first frame update
    void Start()
    {
        Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Panel.activeSelf)
            {
                Time.timeScale = 0f;
                Panel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                Panel.SetActive(false);
            }
        }   
    }


    public void quit()
    {
        Application.Quit();
    }

    public void resume()
    {
        Time.timeScale = 1f;
        Panel.SetActive(false);
    }
}
