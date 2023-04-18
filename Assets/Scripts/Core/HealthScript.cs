using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HealthScript : MonoBehaviour, IDataPersistence
{
    public Sprite AttackedSprite;
    public Sprite NormalSprite;
    public Sprite FaintedSprite;
    private static int hp;
    public bool IsFainted;
    public bool IsFirstStage;
    public GameObject HealthBar;
    private static GameObject DialogBoxTextObject;
    private GameObject GameElements;
    AudioSource audioSource;
    private AudioClip hitSound;
    private int dayCount;
    public GameObject dataPersistenceManager;


    public void LoadData(GameData data)
    {
        this.dayCount = data.dayCount;
    }

    public void SaveData(ref GameData data)
    {
        data.dayCount = this.dayCount;
    }

    void Start()
    {
        DialogBoxTextObject = GameObject.Find("DialogBoxText");
        GameElements = GameObject.Find("GAME_Elements");
        IsFainted = false;
        audioSource = gameObject.GetComponent<AudioSource>();
        hitSound = Resources.Load("Sound/Voice/hitSound") as AudioClip;
        dataPersistenceManager = GameObject.Find("DataPersistenceManager");
    }
    public void Hit(int damage)
    {
        if (IsFainted) return;
        hp = hp - damage;
        audioSource.PlayOneShot(hitSound);
        if (hp < 0) hp = 0;
        HealthBar.GetComponent<Slider>().value = hp;
        if (hp <= 0)
        {
            IsFainted = true;
            PlayerPrefs.SetInt("HP", 1000);
            HealthBar.GetComponentInChildren<SpriteRenderer>().sprite = FaintedSprite;
            StartCoroutine(Fainted());
        }
        else
        {
            StartCoroutine(FacialExpression());
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

    IEnumerator FacialExpression()
    {
        HealthBar.GetComponentInChildren<SpriteRenderer>().sprite = AttackedSprite;
        gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1.0f, 0.6f, 0.6f, 1.0f);

        yield return new WaitForSecondsRealtime(0.35f);
        HealthBar.GetComponentInChildren<SpriteRenderer>().sprite = NormalSprite;
        gameObject.GetComponentInParent<SpriteRenderer>().color = Color.white;
    }

    IEnumerator Fainted() // save data
    {

        InputDecoder.isGameInScript = true;
        InputDecoder.InterfaceElements.SetActive(true);
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().LoadScript("Text/System/Fainted");
        yield return new WaitWhile(() => InputDecoder.isGameInScript);

        dayCount++;
        bool saved = false;
        saved = dataPersistenceManager.GetComponent<DataPersistenceManager>().SaveGame();
        yield return new WaitWhile(() => !saved);
        
        SceneManager.LoadScene("PreLobbyScene");
    }
}
