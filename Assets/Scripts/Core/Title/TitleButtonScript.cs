using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleButtonScript : MonoBehaviour
{
    AudioSource audioSource;
    public GameObject dataPersistenceManager;
    public GameObject resetUI;
    public bool IsresetUIPopped = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dataPersistenceManager = GameObject.Find("DataPersistenceManager");
    }
	public void NewGameClick(){
        audioSource.Play();
        if (dataPersistenceManager.GetComponent<DataPersistenceManager>().CheckGameDataExist())
        {
            if (!IsresetUIPopped)
            {
                IsresetUIPopped = true;
                var resetUIInstace = Instantiate(resetUI, new Vector3(0f,1f,1f), Quaternion.identity);
                //resetUIInstace.transform.parent = gameObject.transform;
            }
            return;
        }
        StartCoroutine(DelayOpeningSceneLoad());

	}
    public IEnumerator DelayOpeningSceneLoad()
    {
        yield return new WaitForSeconds(0.2f);
        dataPersistenceManager.GetComponent<DataPersistenceManager>().NewGame();
        bool saved = false;
        saved = dataPersistenceManager.GetComponent<DataPersistenceManager>().SaveGame();
        yield return new WaitWhile(() => !saved);
        SceneManager.LoadScene("OpeningScene");        
    }

    public void ContinueGameClick(){
        audioSource.Play();
        StartCoroutine(DelayLobbySceneLoad());
    }

    IEnumerator DelayLobbySceneLoad()
    {
        yield return new WaitForSeconds(0.2f);

        SceneManager.LoadScene("LobbyScene");
    }

    public void EndGameScript(){
        dataPersistenceManager.GetComponent<DataPersistenceManager>().saveOnQuit = false;
        Application.Quit();
    }
}
