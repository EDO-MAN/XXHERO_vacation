using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //사운드
    AudioSource audioSrc;
    public AudioClip hitAudio;
    public AudioClip wieldAudio;
    public AudioClip finger1_Audio;
    public AudioClip finger2_Audio;
    public AudioClip tailAudio;

    //체력
    [SerializeField]
    private Slider Hp_slider;

    private int health = 200;
    private int CurHp = 200;

    public GameObject BlackScreen;
    public GameObject tutorialout;
    public Text talkText;
    public GameObject talkobj;

    void Awake()
    {
        anim = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
        StartCoroutine(Think());
    }
    void Start()
    {
        Hp_slider.maxValue = health;
    }
    private void HandleHp()
    {
        Hp_slider.value = health;
        //Hp_slider.value = Mathf.Lerp(Hp_slider.value, (float)health, Time.deltaTime * 10);
    }
    public void TakeDamage(int damage)
    {
        //StartCoroutine(Enemy_Hit(damage));
        anim.SetTrigger("HitMotion");
        health -= damage;
        HandleHp();

        if (health == 100)
        {
            Time.timeScale = 0;
            talkobj.SetActive(true);
            talkText.text = "살려줘";
        }

        if (health <= 0)
        {
            Time.timeScale = 0;
            BlackScreen.SetActive(true);
            tutorialout.SetActive(false);
        }
    }
    public void HitSound()
    {
        audioSrc.clip = hitAudio;
        audioSrc.Play();
    }
    public void WieldSound()
    {
        audioSrc.clip = wieldAudio;
        audioSrc.volume = 0.2f;
        audioSrc.Play();
    }
    public void Finger1_Sound()
    {
        audioSrc.clip = finger1_Audio;
        audioSrc.volume = 0.2f;
        audioSrc.Play();
    }
    public void Finger2_Sound()
    {
        audioSrc.clip = finger2_Audio;
        audioSrc.volume = 0.2f;
        audioSrc.Play();
    }
    public void tailSound()
    {
        audioSrc.clip = tailAudio;
        audioSrc.volume = 0.2f;
        audioSrc.Play();
    }
    public void Onbtn1()
    { 
        
        Time.timeScale = 1;
        talkobj.SetActive(false);
       
    }
    public void Onbtn2()
    {
        GetComponent<Animator>().SetBool("2Pmoving",true);
        Time.timeScale = 1;
        talkobj.SetActive(false);
    }
    //플레이어 방향
    public Transform player;
    public bool isFlipped = false;

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
   
    // 공격 기즈모
    Animator anim;
    [SerializeField]
    private Transform playerTransform;

    //[SerializeField]
    //private float timer = 0.0f;

    public const float value = 0.38f;
    
    
    //
    public Transform enemy_Tail;
    public Transform enemy_Spear;
    public Transform enemy_Finger;
    public Vector2 TailSize;
    public Vector2 SpearSize;
    public Vector2 FingerSize;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(enemy_Tail.position, TailSize);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(enemy_Spear.position, SpearSize);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(enemy_Finger.position, FingerSize);
    }
    //
    //IEnumerator Spear_Wave()
    //{
    //    SpearEnergy wave = ObjectPool.SpearGetObject();
       

    //    wave.transform.position = new Vector3(0.44f, 0f , 0);
        
    //    //wave = new SpearEnergy();//메모리에 새로 생성.
    //    yield return new WaitForSeconds(1.0f);
    //}
    

    //IEnumerator Tail_Wave()
    //{
    //    Vector2 vec = playerTransform.position;
    //    float x = vec.x;
    //    Tail_Wave wave = ObjectPool.TailGetObject();
    //    if (vec.x <= -5)
    //    {
    //        wave.transform.position = new Vector3(x + 1.238f, -0.66f, 0);
    //    }
    //    else if (vec.x >= 0.35f)
    //    {
    //        wave.transform.position = new Vector3(x + 0.238f - 1.0f, -0.66f, 0);
    //    }
    //    else
    //    {
    //        wave.transform.position = new Vector3(x + 0.238f, -0.66f, 0);
    //    }
    //        yield return new WaitForSeconds(1.0f);
    //}

    //IEnumerator Tail_Effect()
    //{
    //    Vector2 vec = playerTransform.position;
    //    float x = vec.x;

    //    Tail_Effect effect = ObjectPool.EffectGetObject();
    //    if (vec.x <= -5)
    //    {
    //        effect.transform.position = new Vector3(x + 1.1f, -0.56f, 0);
    //    }
    //    else if (vec.x >= 0.35f)
    //    {
    //        effect.transform.position = new Vector3(x - 0.9f, -0.56f, 0);
    //    }
    //    else
    //    {
    //        effect.transform.position = new Vector3(x + 0.1f, -0.56f, 0);
    //    }
    //    StartCoroutine(Tail_Effect_Destory(effect));
    //    yield return new WaitForSeconds(5.0f);
    //}

    //IEnumerator Finger1_Wave()
    //{
    //    Finger_Wave1 wave = ObjectPool.Finger1GetObject();
    //    wave.transform.position = new Vector3(0, -0.5f, 0);
    //    //wave = new SpearEnergy();//메모리에 새로 생성.
    //    yield return new WaitForSeconds(1.0f);
    //}

    //IEnumerator Finger1_Effect()
    //{
    //    Finger1_Effect effect = ObjectPool.f1EffectGetObject();
    //    effect.transform.position = new Vector3(-2.69f, -0.5f, 0);

    //    StartCoroutine(Effect_Destory(effect) );
    //    yield return new WaitForSeconds(1.0f);
    //}

    //IEnumerator Finger2_Wave()
    //{
    //    Finger_Wave2 wave = ObjectPool.Finger2GetObject();
    //    wave.transform.position = new Vector3(-2.55f, -3.58f, 0);
    //    //wave = new SpearEnergy();//메모리에 새로 생성.
    //    yield return new WaitForSeconds(1.0f);
    //}

    //IEnumerator Finger2_Effect()
    //{
    //    Finger2_Effect effect = ObjectPool.f2EffectGetObject();
    //    effect.transform.position = new Vector3(-2.54f, -0.52f, 0);

    //    StartCoroutine(Effect_Destory(effect));
    //    yield return new WaitForSeconds(1.0f);
    //}

    //IEnumerator Tail_Effect_Destory(Tail_Effect war)
    //{
    //    SpriteRenderer spr = war.GetComponent<SpriteRenderer>();

    //    Color temColor = spr.color;
    //    float a = spr.color.a;

    //    yield return null;

    //    while (a > 0.01f)
    //    {
    //        spr.color = new Color(temColor.r, temColor.g, temColor.b, a);
    //        yield return null;
    //        a -= Time.deltaTime;

    //    }
    //    ObjectPool.EnQueueObject(war);
    //}

    //IEnumerator Effect_Destory(Finger1_Effect war)
    //{
    //    SpriteRenderer spr = war.GetComponent<SpriteRenderer>();
    //    Color temColor = spr.color;
    //    float a = spr.color.a;

    //    yield return null;

    //    while (a > 0.01f)
    //    {
    //        spr.color = new Color(temColor.r, temColor.g, temColor.b, a);
    //        yield return null;
    //        a -= Time.deltaTime;

    //    }
    //    ObjectPool.EnQueueObject(war);
    //}

    //IEnumerator Effect_Destory(Finger2_Effect war)
    //{
    //    SpriteRenderer spr = war.GetComponent<SpriteRenderer>();
    //    Color temColor = spr.color;
    //    float a = spr.color.a;

    //    yield return null;

    //    while (a > 0.01f)
    //    {
    //        spr.color = new Color(temColor.r, temColor.g, temColor.b, a);
    //        yield return null;
    //        a -= Time.deltaTime;

    //    }
    //    ObjectPool.EnQueueObject(war);
    //}


    IEnumerator Think()
    {
        yield return new WaitForSeconds(1.5f);

        int ranAction = Random.Range(0, 3);
        switch (ranAction)
        {
            //case 0:
            case 0:
                //손톱 공격 모션
                StartCoroutine(Finger_Mot());
                break;
            case 1:
                //창 공격 모션
                StartCoroutine(SpearWield_Mot());
                break;
            case 2:
                //꼬리 공격 모션
                StartCoroutine(Tail_Mot());
                break;
        }
    }
    IEnumerator Finger_Mot()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(enemy_Finger.position, TailSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player")
            {
                Debug.Log("손가락맞음");
                collider.GetComponent<PlayerStats>().PlayerTakeDamage(0.25f);
            }
        }
        anim.SetTrigger("doFinger");
        
        yield return new WaitForSeconds(1.2f);
        StartCoroutine(Think());
    }
    IEnumerator SpearWield_Mot()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(enemy_Spear.position, TailSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player")
            {
                Debug.Log("창휘둘기맞음");
                collider.GetComponent<PlayerStats>().PlayerTakeDamage(0.25f);
            }
        }
        anim.SetTrigger("doSpear_Wield");
        
        yield return new WaitForSeconds(1.2f);
        StartCoroutine(Think());
    }
    IEnumerator Tail_Mot()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(enemy_Tail.position, TailSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player")
            {
                Debug.Log("꼬리맞음");
                collider.GetComponent<PlayerStats>().PlayerTakeDamage(0.25f);
            }
        }
        anim.SetTrigger("doTail");
       
        yield return new WaitForSeconds(1.2f);
        StartCoroutine(Think());
    }
}
