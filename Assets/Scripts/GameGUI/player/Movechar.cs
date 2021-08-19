using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Movechar : MonoBehaviour
{
    public AudioClip audioAttack;
    public AudioClip audioAttack2;
    public AudioClip jump;
    // public AudioClip audioJump;

    public float movePower = 1.0f;
    public float jumpPower = 1.0f;
    //public GameObject Tutorialimg;
    //bool isPause;
   
    Rigidbody2D rigid;
    AudioSource audioSrc;
    //SpriteRenderer sprite;
    Animator anim;
    //HP_Enemyslider hphand;
    //Vector3 movement;
    
    //2단점프
    bool isJumping = false;
    int JumpCount = 0;
   
    private float curTime;
    public float attack_coolTime = 0.5f;
    
    public Transform pos;
    public Vector2 boxSize;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        //sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
        
    }
    public void attackSound()
    {
        audioSrc.clip = audioAttack;
        audioSrc.volume = 0.1f;
        audioSrc.Play();
    }
    public void attackSound2()
    {
        audioSrc.clip = audioAttack2;
        audioSrc.volume = 0.1f;
        audioSrc.Play();
    }

    public void jumpSound()
    {
        audioSrc.clip = jump;
        audioSrc.volume = 0.05f;
        audioSrc.Play();
    }

    void Start()
    {
        //Time.timeScale = 0;
    }
    void Update()
    {
        if (curTime <= 0)
        {
            //Attack
            if (Input.GetKey(KeyCode.Z))
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.tag == "Enemy")
                    {
                        Debug.Log("맞음");
                        collider.GetComponent<Enemy>().TakeDamage(10);
                    }
                }
                anim.SetTrigger("doAttack");
                curTime = attack_coolTime;
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }
        if (Input.GetKeyDown("c"))
        {
            if (JumpCount < 2)
            {
                if (isJumping = true)
                {
                    anim.SetBool("Jumping", true);
                    jumpSound();
                }
                JumpCount++;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetAxisRaw("Horizontal") == -1)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else if (Input.GetAxisRaw("Horizontal") == 1)
            transform.eulerAngles = new Vector3(0, 0, 0);

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        JumpCount = 0;
    }
    void FixedUpdate()
    {
        Move();
        Jump();
    }
    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        if(Input.GetAxisRaw("Horizontal") == 0)
        {
            anim.SetBool("isMove", false);
        }

        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
            //anim.SetInteger("Direction", -1);
            anim.SetBool("isMove", true);    
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
            //anim.SetInteger("Direction", 1);
            anim.SetBool("isMove", true);
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }
    void Jump()
    {
        if (!isJumping)
        {
            anim.SetBool("Jumping", false);
            return;
        }

        rigid.velocity = Vector2.zero;
        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
        anim.SetBool("Jumping", false);
        isJumping = false;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

    //public void TutorialBtn()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Tutorialimg.SetActive(false);
    //        Time.timeScale = 1;
    //    }
    //    Tutorialimg.SetActive(false);
    //    Time.timeScale = 1;
    //}
    public void TakePlayerDamage(int damage)
    {
        Debug.Log(damage);
    }
    //public GameObject tile;
    //void TileMapOn()
    //{
    //    if (Input.GetKeyDown(KeyCode.H))
    //    {
    //        tile.SetActive(true);
    //    }
    //    else if(Input.GetKeyDown("j"))
    //    {
    //        tile.SetActive(false);
    //    }
    //}
}
