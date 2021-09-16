using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        e_Idle,
        e_Fit,
        e_Spear_Throw,
        e_FingerAttack,
        e_TailAttack,
        e_ArmorBreak,
        e_Move,
    }


    public Transform enemy_Tail;
    public Transform enemy_Spear;
    public Transform enemy_Finger;

    public Vector2 TailSize;
    public Vector2 SpearSize;
    public Vector2 FingerSize;   

    public float e_speed;
    public float e_distance;
    public float e_jumpPower;

    //bool fit = false;
    //bool isMove = true;

    Transform player;
    Animator e_anim;
    public GameObject spearG;
    public GameObject balpan;

    //체력
    [SerializeField]
    private Slider e_Hp_slider;
    private int e_health = 100;


    void Awake()
    {
        e_Hp_slider.maxValue = e_health;
        player = GameObject.Find("Player").transform;
        e_anim = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(9, 10);
    }

    //float curTime = 0f;
    float e_deltaTime = 0f;

    public EnemyState e_state = EnemyState.e_Idle;

    void Update()
    {
        switch (e_state)
        {

            case EnemyState.e_Idle:

                e_deltaTime += Time.deltaTime;

                if (e_deltaTime >= 1.5f)
                {
                    e_deltaTime = 0f;

                    spearG.SetActive(false);
                    balpan.SetActive(false);
                    e_anim.SetBool("isMoving", false);
                    e_anim.SetBool("finger", false);
                    e_anim.SetBool("throw", false);
                    e_state = EnemyState.e_Move;
                }

                break;
            case EnemyState.e_Fit:

                e_anim.SetTrigger("fitMotion");
                e_health -= 10;
                HandleHp();

                if (e_health >= 35 && e_health < 50)
                    e_state = EnemyState.e_Spear_Throw;

                e_state = EnemyState.e_Idle;

                break;

            case EnemyState.e_Spear_Throw:

                spearG.SetActive(true);
                balpan.SetActive(true);

                e_speed = 0;
                Direction();
                e_anim.SetBool("isMoving", false);
                e_anim.SetBool("throw", true);

                if (e_health <= 35)
                {
                    e_state = EnemyState.e_Idle;
                }
                else
                    e_state = EnemyState.e_Spear_Throw;

                break;

            case EnemyState.e_TailAttack:

                break;

            case EnemyState.e_FingerAttack:
                e_speed = 0;
                Direction();
                e_anim.SetBool("isMoving", false);
                e_anim.SetBool("finger", true);

                //Collider2D collider2D = Physics2D.OverlapBox(enemy_Finger.position, FingerSize, 0, 1 << 9);
                //if ( collider2D.tag.Equals( "Player" ) )
                //{
                //    Debug.Log("적이 플레이어를 떄림");
                //    player.GetComponent<Movechar>().anim.SetTrigger("myHit");
                //}

                e_state = EnemyState.e_Idle;


                break;

            case EnemyState.e_Move:


                if (Mathf.Abs(transform.position.x - player.position.x) <= e_distance)
                    e_state = EnemyState.e_FingerAttack;

                if (e_health >= 35 && e_health < 50)
                    e_state = EnemyState.e_Spear_Throw;


                Direction();
                transform.Translate(new Vector2(-1, 0) * Time.deltaTime * e_speed);
                e_anim.SetBool("isMoving", true);
                e_speed = 6;

                break;

            case EnemyState.e_ArmorBreak:

                e_anim.SetTrigger("armorBreak");


                e_state = EnemyState.e_Idle;

                break;
        }
    }


    public void Finger_Attack()//애니메이션 이벤트용 타격 함수
    {
        Collider2D collider2D = Physics2D.OverlapBox(enemy_Finger.position, FingerSize, 0, 1 << 9);
        if (collider2D.tag.Equals("Player"))
        {
            Debug.Log("적이 플레이어를 떄림");
            player.GetComponent<Movechar>().p_state = Movechar.PlayerState.p_Fit;
        }
    }
  
    //public void Spear_Attack()
    //{
    //    spearG.SetActive(true);
    //    //Collider2D collider2D = Physics2D.OverlapBox(enemy_Spear.position, SpearSize, 0, 1 << 9);
    //    //if (collider2D.tag.Equals("Player"))
    //    //{
    //    //    Debug.Log("적이 플레이어를 떄림");
    //    //    player.GetComponent<Movechar>().p_state = Movechar.PlayerState.p_Fit;
    //    //}
    //}


    private void HandleHp()//HP
    {
        e_Hp_slider.value = e_health;
        //Hp_slider.value = Mathf.Lerp(Hp_slider.value, (float)health, Time.deltaTime * 10);
    }

    void Direction() //바라보는 방향
    {

        if (transform.position.x - player.position.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(enemy_Tail.position, TailSize);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(enemy_Spear.position, SpearSize);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(enemy_Finger.position, FingerSize);
    }
}
    //public void TakeDamage(int damage)//피격 시 데미지 계산 함수
    //{
    //    anim.SetTrigger("fitMotion");
    //    health -= damage;
    //    HandleHp();
        
    //    //fit = true;
    //    //while (stop < 1)
    //    //{
    //    //    StopAllCoroutines();
    //    //    stop += 0.1f * Time.deltaTime;
    //    //}
    //}
    /*
    //void FingerAttack()
    //{
    //    Collider2D[] Finger_collider2Ds = Physics2D.OverlapBoxAll(enemy_Finger.position, FingerSize, 0);
    //    foreach (Collider2D collider in Finger_collider2Ds)
    //    {
    //        if (collider.tag == "Player")
    //        {
    //            Debug.Log("손톱공격");

    //            //anim.SetTrigger("finger");
    //        }
    //        else
    //        {
    //            return;
    //        }
    //    }
    //}

    //IEnumerator Move()//움직이는 함수
    //{
    //    if (health >= 100)
    //    {

    //        if (fit)
    //        {
    //            yield return new WaitForSeconds(1.0f);
    //            fit = false;
    //        }
    //        else
    //        {

    //            // 일정 거리가됬을때 즉시 공격 (애니메이션이 바로 실행되야한다.)
    //            if ( Mathf.Abs(transform.position.x - player.position.x) > distance  )
    //            {
    //                transform.Translate(new Vector2(-1, 0) * Time.deltaTime * speed);
    //                anim.SetBool("isMoving", true);
    //                speed = 6;
    //                Direction();

    //                //장애물 앞에 있을 시 점프
    //                //RaycastHit2D hit =  Physics2D.Raycast(transform.position, transform.right * -1f, 2f, groundLayer);

    //                //if (hit)
    //                //{
    //                //    rigid.velocity = Vector2.up * jumpPower;
    //                //}
    //            }
    //            else
    //            {
    //                anim.SetBool("isMoving", false);
    //                anim.SetTrigger("finger");
    //                speed = 0;
    //                yield return new WaitForSeconds(1f);

    //            }

    //        }
    //    }
    //}

    //StartCoroutine(Move());
    //FingerAttack();


    //if ( isMove == false )
    //    deltaTime += Time.deltaTime;

    //if ( deltaTime >= 1.5f )
    //{
    //    deltaTime = 0f;
    //    isMove = true;
    //}


    //if (health >= 100)
    //{

    //    if (fit)
    //    {
    //        //yield return new WaitForSeconds(1.0f);
    //        fit = false;
    //    }
    //    else
    //    {

    //        // 일정 거리가됬을때 즉시 공격 (애니메이션이 바로 실행되야한다.)
    //        if (Mathf.Abs(transform.position.x - player.position.x) > distance && isMove)
    //        {
    //            transform.Translate(new Vector2(-1, 0) * Time.deltaTime * speed);
    //            anim.SetBool("isMoving", true);
    //            speed = 6;
    //            Direction();

    //            //장애물 앞에 있을 시 점프
    //            //RaycastHit2D hit =  Physics2D.Raycast(transform.position, transform.right * -1f, 2f, groundLayer);

    //            //if (hit)
    //            //{
    //            //    rigid.velocity = Vector2.up * jumpPower;
    //            //}
    //        }
    //        else 
    //        {

    //            if ( isMove )
    //            {
    //                isMove = false;
    //                Direction();
    //                anim.SetBool("isMoving", false);
    //                anim.SetTrigger("finger");
    //                speed = 0;

    //            }





    //        }

    //    }
    //}

    // Gizmos.DrawWireCube(enemy_Finger.position, FingerSize);

    //if (curTime <= 0)
    //{

    //    Collider2D collider2D = Physics2D.OverlapBox( enemy_Finger.position, FingerSize, 0 , 1 << 9 );
    //    Debug.Log("맞음");




    //}
    //else
    //{
    //    curTime -= Time.deltaTime;
    //}
}*/
