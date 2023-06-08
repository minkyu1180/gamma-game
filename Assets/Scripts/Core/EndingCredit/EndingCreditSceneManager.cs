using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndingCreditSceneManager : MonoBehaviour, IDataPersistence
{
    public SpriteRenderer BackSceneSprite;
    GameObject dataPersistenceManager;


    public Sprite PreEnding4;
    public Sprite PreEnding3;
    public Sprite PreEnding2;
    public Sprite PreEnding1;

    public Sprite Ending1;
    public Sprite Ending2;
    public Sprite Ending3;
    public Sprite Ending4;

    public Sprite FinalEnding;
    
    SpriteRenderer[] MinkyuSprites;
    SpriteRenderer[] DahyeSprites;
    SpriteRenderer[] SusangSprites;
    SpriteRenderer[] JiheeSprites;
    SpriteRenderer[] ChunbokSprites;

    bool didSeeGlassDoorEvent;
    bool AbleToReturnGame = false;
    bool StopUpdate = false;

    public void LoadData(GameData data)
    {
        this.didSeeGlassDoorEvent = data.didSeeGlassDoorEvent;
    }

    public void SaveData(ref GameData data)
    {
        data.didSeeGlassDoorEvent = this.didSeeGlassDoorEvent;
    }

    void Start()
    {
        BackSceneSprite = GameObject.Find("BackScene").GetComponent<SpriteRenderer>();
        StartCoroutine(ScriptLoader());
        MinkyuSprites = GameObject.Find("Minkyu").GetComponentsInChildren<SpriteRenderer>();
        DahyeSprites = GameObject.Find("Dahye").GetComponentsInChildren<SpriteRenderer>();
        SusangSprites = GameObject.Find("Susang").GetComponentsInChildren<SpriteRenderer>();
        JiheeSprites = GameObject.Find("Jihee").GetComponentsInChildren<SpriteRenderer>();
        ChunbokSprites = GameObject.Find("Chunbok").GetComponentsInChildren<SpriteRenderer>();

        dataPersistenceManager = GameObject.Find("DataPersistenceManager");

    }

    void SpritesOn(SpriteRenderer[] Sprites)
    {
        foreach (SpriteRenderer Sprite in Sprites)
        {
            Sprite.color = new Color(1f,1f,1f,1f);
        }
    }

    void SpritesOff(SpriteRenderer[] Sprites)
    {
        foreach (SpriteRenderer Sprite in Sprites)
        {
            Sprite.color = new Color(1f,1f,1f,0f);
        }
    } 

    IEnumerator ScriptLoader()
    {
        yield return new WaitForSeconds(0.46f);
        BackSceneSprite.sprite = PreEnding3;
        yield return new WaitForSeconds(0.46f);
        BackSceneSprite.sprite = PreEnding2;
        yield return new WaitForSeconds(0.46f);
        BackSceneSprite.sprite = PreEnding1;
        
        yield return new WaitForSeconds(0.46f);
        BackSceneSprite.sprite = Ending1;
        SpritesOn(MinkyuSprites);

        yield return new WaitForSeconds(7.36f);
        SpritesOff(MinkyuSprites);
        BackSceneSprite.sprite = Ending2;
        SpritesOn(ChunbokSprites);
        SpritesOn(SusangSprites);

        yield return new WaitForSeconds(7.36f);
        SpritesOff(ChunbokSprites);
        SpritesOff(SusangSprites);
        BackSceneSprite.sprite = Ending3;
        SpritesOn(JiheeSprites);

        yield return new WaitForSeconds(7.36f);
        SpritesOff(JiheeSprites);
        BackSceneSprite.sprite = Ending4;
        SpritesOn(DahyeSprites);

        yield return new WaitForSeconds(7.36f);
        SpritesOff(DahyeSprites);

        BackSceneSprite.sprite = FinalEnding;

        yield return new WaitForSeconds(5.0f);

        AbleToReturnGame = true;

    }


    void Update()
    {
       if (AbleToReturnGame && !StopUpdate)
       {
            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetMouseButtonDown(0))
            {
                StopUpdate = true;
                StartCoroutine(SaveNReturn());
            }
       } 
    }

    IEnumerator SaveNReturn()
    {
        didSeeGlassDoorEvent = true;
        bool saved = false;
        saved = dataPersistenceManager.GetComponent<DataPersistenceManager>().SaveGame();
        yield return new WaitWhile(() => !saved);

        SceneManager.LoadScene("LobbyScene");
    }

}
