using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleButtonScript : MonoBehaviour
{
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
	public void NewGameClick(){
        audioSource.Play();
        StartCoroutine(DelaySceneLoad());
	}
    IEnumerator DelaySceneLoad()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("OpeningScene");        
    }

    public void ContinueGameClick(){
        audioSource.Play();
        StartCoroutine(DelaySceneLoad());
    }

    public void EndGameScript(){
        Application.Quit();
    }
}
