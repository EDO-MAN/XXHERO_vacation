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
    //private int CurHp = 200;

    //public GameObject BlackScreen;
    //public GameObject tutorialout;
    //public Text talkText;
    //public GameObject talkobj;

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

        //if (health == 100)
        //{
        //    Time.timeScale = 0;
        //    talkobj.SetActive(true);
        //    talkText.text = "살려줘";
        //}

        //if (health <= 0)
        //{
        //    Time.timeScale = 0;
        //    BlackScreen.SetActive(true);
        //    tutorialout.SetActive(false);
        //}
    }
    public void HitSound()
    {
        audioSrc.clip = hitAudio;
        audioSrc.Play();
    }
    public void WieldSound()
    {
        audioSrc.clip = wieldAudio;
        audioSrc.volume = 0.5f;
        audioSrc.Play();
    }
    public void Finger1_Sound()
    {
        audioSrc.clip = finger1_Audio;
        audioSrc.volume = 0.5f;
        audioSrc.Play();
    }
    public void Finger2_Sound()
    {
        audioSrc.clip = finger2_Audio;
        audioSrc.volume = 0.5f;
        audioSrc.Play();
    }
    public void tailSound()
    {
        audioSrc.clip = tailAudio;
        audioSrc.volume = 0.5f;
        audioSrc.Play();
    }
    //public void Onbtn1()
    //{ 
        
    //    Time.timeScale = 1;
    //    talkobj.SetActive(false);
       
    //}
    //public void Onbtn2()
    //{
    //    GetComponent<Animator>().SetBool("2Pmoving",true);
    //    Time.timeScale = 1;
    //    talkobj.SetActive(false);
    //}
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
