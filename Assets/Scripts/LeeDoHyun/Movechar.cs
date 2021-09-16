using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Movechar : MonoBehaviour
{
    public enum PlayerState
    {
        p_Idle,
        p_Fit,
        p_Attack,
        p_Move,
        p_Jump
    }
    public PlayerState p_state = PlayerState.p_Move;

    public AudioClip p_audioAttack;
    public AudioClip p_audioAttack2;
    public AudioClip p_jump;

    public float p_movePower = 1.0f;
    public float p_jumpPower = 1.0f;

    Rigidbody2D p_rigid;
    AudioSource p_audioSrc;
    public Animator p_anim;

    public AudioClip p_audioJump;
    //public GameObject Tutorialimg;
    //SpriteRenderer sprite;
    //HP_Enemyslider hphand;
    //Vector3 movement;

    //2단점프
    bool p_isJumping = false;
    int p_JumpCount = 0;

    public float p_curTime;
    public float p_attack_coolTime;

    public Transform pos;
    public Vector2 boxSize;

    //체력
    [SerializeField]
    private Slider p_HP_slider;
    private float p_health = 100;
  


    void Awake()
    {
        p_rigid = GetComponent<Rigidbody2D>();
        //sprite = GetComponent<SpriteRenderer>();
        p_anim = GetComponent<Animator>();
        p_audioSrc = GetComponent<AudioSource>();

    }
    void Start()
    {
        p_HP_slider.value = p_health;
    }
    void Update()
    {
        switch (p_state)
        {
            case PlayerState.p_Idle:

                break;
            case PlayerState.p_Fit:
                p_anim.SetTrigger("myHit");
                p_health -= 10;
                p_HandleHp();

                p_state = PlayerState.p_Move;
                break;
            //case PlayerState.Attack:
            //    if (p_curTime <= 0)
            //    {
            //        if (Input.GetKeyDown(KeyCode.Z))
            //        {
            //            Debug.Log("첫번째 공격 루트");
            //            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
            //            foreach (Collider2D collider in collider2Ds)
            //            {
            //                if (collider.tag == "Enemy")
            //                {
            //                    Debug.Log("플레이어가 적을 때림");
            //                    //collider.GetComponent<EnemyAI>().TakeDamage(5);
            //                    collider.GetComponent<EnemyAI>()._state = EnemyAI.EnemyState.Fit;
            //                }
            //            }
            //            anim.SetTrigger("doAttack");
            //            p_curTime = p_attack_coolTime;
            //            p_state = PlayerState.Move;
            //        }
            //    }
            //    else
            //    {

            //        Debug.Log(p_curTime);
            //        p_curTime -= Time.deltaTime;

            //        if (p_curTime <= 0)
            //            p_state = PlayerState.Move;

            //    }
            //    break;
            case PlayerState.p_Move:
                //이동
                Vector3 moveVelocity = Vector3.zero;
                if (Input.GetAxisRaw("Horizontal") == 0)
                {
                    p_anim.SetBool("isMove", false);
                }

                else if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    moveVelocity = Vector3.left;

                    p_anim.SetBool("isMove", true);
                }
                else if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    moveVelocity = Vector3.right;

                    p_anim.SetBool("isMove", true);
                }
                //if (Input.GetKeyDown("z"))
                //{
                //    p_state = PlayerState.Attack;
                //}
                //플레이어 시점방향
                if (Input.GetAxisRaw("Horizontal") == -1)
                    transform.eulerAngles = new Vector3(0, 180, 0);
                else if (Input.GetAxisRaw("Horizontal") == 1)
                    transform.eulerAngles = new Vector3(0, 0, 0);

                transform.position += moveVelocity * p_movePower * Time.deltaTime;
                break;
        }

        //공격
        if (p_curTime <= 0)
        {
            //Attack
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.tag == "Enemy")
                    {
                        Debug.Log("플레이어가 적을 때림");
                        //collider.GetComponent<EnemyAI>().TakeDamage(5);
                        collider.GetComponent<EnemyAI>().e_state = EnemyAI.EnemyState.e_Fit;

                    }
                }
                p_anim.SetTrigger("doAttack");
                p_curTime = p_attack_coolTime;
                Debug.Log(p_curTime);
            }
        }
        else
        {
            p_curTime -= Time.deltaTime;
        }
        //점프
        if (Input.GetKeyDown("c"))
        {
            if (p_JumpCount < 2)
            {
                if (p_isJumping = true)
                {
                    Debug.Log("점프 확인");
                    p_anim.SetBool("Jumping", true);
                    jumpSound();
                }
                p_JumpCount++;
            }
            else
            {
                p_isJumping = false;
            }
        }
    }

    public void attackSound()
    {
        p_audioSrc.clip = p_audioAttack;
        p_audioSrc.volume = 0.1f;
        p_audioSrc.Play();
    }
    public void attackSound2()
    {
        p_audioSrc.clip = p_audioAttack2;
        p_audioSrc.volume = 0.1f;
        p_audioSrc.Play();
    }

    public void jumpSound()
    {
        p_audioSrc.clip = p_jump;
        p_audioSrc.volume = 0.05f;
        p_audioSrc.Play();
    }

    void Jump()
    {
        if (!p_isJumping)
        {
            p_anim.SetBool("Jumping", false);
            return;
        }

        p_rigid.velocity = Vector2.zero;
        Vector2 jumpVelocity = new Vector2(0, p_jumpPower);
        p_rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
        p_anim.SetBool("Jumping", false);
        p_isJumping = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        p_JumpCount = 0;
    }
    void LateUpdate()
    {
        Jump();
    }
    //void Move()
    //{
    //    //이동
    //    Vector3 moveVelocity = Vector3.zero;
    //    if(Input.GetAxisRaw("Horizontal") == 0)
    //    {
    //        anim.SetBool("isMove", false);
    //    }

    //    else if (Input.GetAxisRaw("Horizontal") < 0)
    //    {
    //        moveVelocity = Vector3.left;

    //        anim.SetBool("isMove", true);    
    //    }
    //    else if (Input.GetAxisRaw("Horizontal") > 0)
    //    {
    //        moveVelocity = Vector3.right;

    //        anim.SetBool("isMove", true);
    //    }
    //    //플레이어 시점방향
    //    if (Input.GetAxisRaw("Horizontal") == -1)
    //        transform.eulerAngles = new Vector3(0, 180, 0);
    //    else if (Input.GetAxisRaw("Horizontal") == 1)
    //        transform.eulerAngles = new Vector3(0, 0, 0);

    //    transform.position += moveVelocity * movePower * Time.deltaTime;
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

    private void p_HandleHp()//HP
    {
        p_HP_slider.value = p_health;
    }
}
