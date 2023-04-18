using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SusangPatternScript : MonoBehaviour
{
    public SpiritManagerScript spiritManagerScript;

    public Animator animator;
    public GameObject Line;
    public GameObject PlusSun;
    public GameObject PlusBundong;
    public GameObject MultiplySun;
    public GameObject Laser;
    public GameObject LaserX;

    public Sprite NormalSprite;
    public Sprite AttackedSprite;
    private GameObject HealthBar;

    public GameObject minkyuHitbox;
    public CapsuleCollider2D minkyuCapsuleCollider2D;
    public bool flipStop = false;
    public bool sunGo;

    private AudioSource audioSource;
    private AudioClip KnifePrepareAudioClip;
    private AudioClip KnifeAttackAudioClip;
    private AudioClip SunMakeAudioClip;
    private AudioClip SusangAttackedAudioClip;
    private AudioClip RockAudioClip;
    private AudioClip FullPowerAttackAudioClip;

    public int tickDamage = 3;
    Coroutine BossPatternCoroutine = null;

    private bool IsFainted;


    public bool sufferingByDamage = false;
    public int hp = 1000;

    private Vector3 swordEndPoint = new Vector3(1.05f, -0.9f, 0f);

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        minkyuHitbox = GameObject.Find("hitbox");
        minkyuCapsuleCollider2D = minkyuHitbox.GetComponent<CapsuleCollider2D>();
        spiritManagerScript = GameObject.Find("SpiritUI").GetComponent<SpiritManagerScript>();
        HealthBar = GameObject.Find("BossHealthBar");
        audioSource = gameObject.GetComponent<AudioSource>();
        KnifePrepareAudioClip = Resources.Load("Sound/Voice/knifePrepareSound") as AudioClip;
        KnifeAttackAudioClip= Resources.Load("Sound/Voice/knifeAttackSound") as AudioClip;
        SunMakeAudioClip = Resources.Load("Sound/Voice/sunMakeSound") as AudioClip;
        RockAudioClip = Resources.Load("Sound/Voice/rockSound") as AudioClip;
        SusangAttackedAudioClip = Resources.Load("Sound/Voice/suSangAttackedSound") as AudioClip;
        FullPowerAttackAudioClip = Resources.Load("Sound/Voice/FullPowerAttackSound") as AudioClip;
    }

    // Update is called once per frame
    void Update()
    {
        if (!flipStop)
        {
            if (minkyuHitbox.transform.position.x > transform.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                swordEndPoint = new Vector3(-1.05f, -0.9f, 0f);
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                swordEndPoint = new Vector3(1.05f, -0.9f, 0f);
            }
        }

        /*
        if (Input.GetKeyDown(KeyCode.F))
        {
            SwordCrossPattern();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            SunFlowerPattern();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            BundongBuildPattern();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            FullPowerLaserPattern();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            FullPowerLaserXPattern();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            BossPatternManager();
        }
        */
        
        /*
        //CHEAT//
        if (Input.GetKeyDown(KeyCode.T))
        {
            hp = -1;
        }
        //CHEAT//
        */
        if (hp <= 0)
        {
            if (BossPatternCoroutine != null) {
                StopCoroutine(BossPatternCoroutine);
            }
        }
    }


    public void BossPatternManager()
    {
        BossPatternCoroutine = StartCoroutine(BossPattern());

    }

    IEnumerator BossPattern()
    {
        while (true)
        {
            for (int i = 0; i < 15; i ++) yield return StartCoroutine(SwordCross());
            for (int i = 0; i < 10; i ++) yield return StartCoroutine(SunFlowerPlus());
            for (int i = 0; i < 10; i ++) yield return StartCoroutine(SwordCross());
            for (int i = 0; i < 1; i ++)  yield return StartCoroutine(BundongBuildPlus());
            for (int i = 0; i < 5; i ++)  yield return StartCoroutine(SwordCross());
            for (int i = 0; i < 1; i ++)  yield return StartCoroutine(FullPowerLaser());
            yield return new WaitForSeconds(0.2f);

            for (int i = 0; i < 10; i ++) yield return StartCoroutine(SwordCross());
            for (int i = 0; i < 1; i ++)  yield return StartCoroutine(BundongBuildPlus());
            for (int i = 0; i < 10; i ++) yield return StartCoroutine(SunFlowerPlus());
            for (int i = 0; i < 10; i ++) yield return StartCoroutine(SwordCross());
            for (int i = 0; i < 1; i ++)  yield return StartCoroutine(BundongBuildPlus());
            for (int i = 0; i < 3; i ++)  yield return StartCoroutine(SwordCross());
            for (int i = 0; i < 1; i ++)  yield return StartCoroutine(BundongBuildPlus());
            for (int i = 0; i < 1; i ++)  yield return StartCoroutine(FullPowerLaserX());
            yield return new WaitForSeconds(0.2f);

            for (int i = 0; i < 7; i ++) yield return StartCoroutine(SwordCross());
            for (int i = 0; i < 10; i ++) yield return StartCoroutine(SunFlowerPlus());
            for (int i = 0; i < 1; i ++)  yield return StartCoroutine(BundongBuildPlus());
            for (int i = 0; i < 7; i ++) yield return StartCoroutine(SwordCross());
            for (int i = 0; i < 1; i ++)  yield return StartCoroutine(BundongBuildPlus());
            for (int i = 0; i < 5; i ++)  yield return StartCoroutine(SwordCross());
            for (int i = 0; i < 1; i ++)  yield return StartCoroutine(FullPowerLaser());
            yield return new WaitForSeconds(0.2f);

            for (int i = 0; i < 1; i ++)  yield return StartCoroutine(BundongBuildPlus());
            for (int i = 0; i < 7; i ++) yield return StartCoroutine(SwordCross());
            for (int i = 0; i < 1; i ++)  yield return StartCoroutine(BundongBuildPlus());
            for (int i = 0; i < 10; i ++) yield return StartCoroutine(SunFlowerPlus());
            for (int i = 0; i < 1; i ++)  yield return StartCoroutine(BundongBuildPlus());
            for (int i = 0; i < 13; i ++) yield return StartCoroutine(SwordCross());
            for (int i = 0; i < 1; i ++)  yield return StartCoroutine(FullPowerLaserX());
            yield return new WaitForSeconds(0.2f);
        }
        //yield return new WaitForSeconds(1f);
    }

    void SwordCrossPattern()
    {
        StartCoroutine(SwordCross());
    }

    IEnumerator SwordCross() //1.1666667f
    {
        animator.SetBool("IsSwordCross", true);
        flipStop = true;
        yield return new WaitForSeconds(0.5f);
        Vector3 movePosition;

        var RealLine = Instantiate(Line, Vector3.zero, Quaternion.identity);
        RealLine.transform.parent = gameObject.transform;
        Vector3 offset = minkyuCapsuleCollider2D.offset;
        Vector3 start = transform.position + swordEndPoint;
        Vector3 end = minkyuHitbox.transform.position + offset;
        movePosition = end;
        end = (end - start).normalized * 50;
        end = start + end;
        RealLine.GetComponent<LineRenderer>().SetPosition(0, start);
        RealLine.GetComponent<LineRenderer>().SetPosition(1, end);
        List <Vector2> setpoint = new List<Vector2>();

        setpoint.Add(new Vector2(start.x, start.y));
        setpoint.Add(new Vector2(end.x, end.y));
        RealLine.GetComponent<EdgeCollider2D>().SetPoints(setpoint);

        audioSource.PlayOneShot(KnifePrepareAudioClip);
        yield return new WaitForSeconds(0.25f);


        var RealLine2 = Instantiate(Line, Vector3.zero, Quaternion.identity);
        RealLine2.transform.parent = gameObject.transform;
        offset = minkyuCapsuleCollider2D.offset;
        start = transform.position + swordEndPoint;
        end = minkyuHitbox.transform.position + offset;
        end = (end - start).normalized * 50;
        end = start + end;
        RealLine2.GetComponent<LineRenderer>().SetPosition(0, start);
        RealLine2.GetComponent<LineRenderer>().SetPosition(1, end);
        setpoint.Clear();

        setpoint.Add(new Vector2(start.x, start.y));
        setpoint.Add(new Vector2(end.x, end.y));
        RealLine2.GetComponent<EdgeCollider2D>().SetPoints(setpoint);

        LineHitScript RealLineScript = transform.GetChild(0).GetComponent<LineHitScript>();
        LineHitScript RealLine2Script = transform.GetChild(1).GetComponent<LineHitScript>();
        audioSource.PlayOneShot(KnifePrepareAudioClip);
        yield return new WaitForSeconds(0.25f);


        RealLine.GetComponent<LineRenderer>().startColor = Color.red;
        RealLine.GetComponent<LineRenderer>().endColor = Color.magenta;
        RealLine2.GetComponent<LineRenderer>().startColor = Color.red;
        RealLine2.GetComponent<LineRenderer>().endColor = Color.magenta;
        
        RealLineScript.active = true;
        RealLine2Script.active = true;
        audioSource.PlayOneShot(KnifeAttackAudioClip);
        yield return new WaitForSeconds(0.1666667f);

        flipStop = false;

        animator.SetBool("IsSwordCross", false);
        transform.position = movePosition;

        yield return new WaitForSeconds(0.23f);
    }

    void SunFlowerPattern()
    {
        StartCoroutine(SunFlowerPatternActForNtime(10));
    }

    IEnumerator SunFlowerPatternActForNtime(int n)
    {
        for (int i = 0 ; i < n; i++){
            StartCoroutine(SunFlowerPlus());
            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator SunFlowerPlus()
    {
        audioSource.PlayOneShot(SunMakeAudioClip);
        animator.SetBool("IsSunFlower", true);
        GameObject RealPlusSun = Instantiate(PlusSun, Vector3.zero, Quaternion.identity);
        
        yield return new WaitForSeconds(1.3f);
        animator.SetBool("IsSunFlower", false);

        yield return new WaitForSeconds(0.2f);    
        //

        //minkyuHitbox.transform.position;
    }
    


    void BundongBuildPattern()
    {
        StartCoroutine(BundongBuildPatternActForNtime(1));
    }

    IEnumerator BundongBuildPatternActForNtime(int n)
    {
        for (int i = 0 ; i < n; i++){
            StartCoroutine(BundongBuildPlus());            
            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator BundongBuildPlus()
    {
        animator.SetBool("IsBundongBuild", true);
        GameObject RealPlusBundong = Instantiate(PlusBundong, Vector3.zero, Quaternion.identity);
        
        yield return new WaitForSeconds(1.3f);
        audioSource.PlayOneShot(RockAudioClip);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("IsBundongBuild", false);       
    }

    void FullPowerLaserPattern()
    {
        StartCoroutine(FullPowerLaser());
    }

    void FullPowerLaserXPattern()
    {
        StartCoroutine(FullPowerLaserX());
    }

    IEnumerator FullPowerLaser() // 4sec
    {
        animator.SetBool("IsRedEraser", true);
        var RealLaser = Instantiate(Laser, Vector3.zero, Quaternion.identity);
        RealLaser.transform.parent = transform.parent;
        yield return new WaitForSeconds(3.5f);
        audioSource.PlayOneShot(FullPowerAttackAudioClip);
        yield return new WaitForSeconds(0.5f);

        animator.SetBool("IsRedEraser", false);
        spiritManagerScript.getRidOfAll();
        spiritManagerScript.spiritSpread();
    }

    IEnumerator FullPowerLaserX()
    {
        animator.SetBool("IsBlueEraser", true);
        var RealLaserX = Instantiate(LaserX, Vector3.zero, Quaternion.identity);
        RealLaserX.transform.parent = transform.parent;
        yield return new WaitForSeconds(3.5f);
        audioSource.PlayOneShot(FullPowerAttackAudioClip);
        yield return new WaitForSeconds(0.5f);

        animator.SetBool("IsBlueEraser", false);
        spiritManagerScript.getRidOfAll();
        spiritManagerScript.spiritSpread();
    }


    public void Hit(int damage)
    {
        if (IsFainted) return;
        hp = hp - damage;
        audioSource.PlayOneShot(SusangAttackedAudioClip);
        if (hp < 0) hp = 0;
        HealthBar.GetComponent<Slider>().value = hp;
        if (hp <= 0)
        {
            IsFainted = true;
        }
        else
        {
            StartCoroutine(FacialExpression());
        }
    }
    
    IEnumerator FacialExpression()
    {
        HealthBar.GetComponentInChildren<SpriteRenderer>().sprite = AttackedSprite;
        gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1.0f, 0.6f, 0.6f, 1.0f);

        yield return new WaitForEndOfFrame();
        HealthBar.GetComponentInChildren<SpriteRenderer>().sprite = NormalSprite;
        gameObject.GetComponentInParent<SpriteRenderer>().color = Color.white;
    }

    void FixedUpdate()
    {
        if (sufferingByDamage) Hit(tickDamage);
    }
}
