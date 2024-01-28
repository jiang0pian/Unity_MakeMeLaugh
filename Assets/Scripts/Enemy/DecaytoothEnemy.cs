using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecaytoothEnemy : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;

    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;

    private bool isFindPlayer;

    private Vector2 lookDirection;

    private float moveTime = 2f;
    private float moveTimer = -1;

    public Collider2D pursuitRange;
    public Collider2D attackRange;

    private float attackTime = 4;
    private float attackTimer = -1;

    private bool isGetAttaack = false;

    public float spurtForce = 100f;

    public float spurtTime = 30f;

    public GameObject enemySprite;

    public bool beginAction = false;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        isFindPlayer = false;
        lookDirection = new Vector2(1, 0);
    }
    private void Start()
    {
        StartCoroutine(BeginAction());
    }


    // Update is called once per frame
    void Update()
    {
        if(beginAction == true)
        {
            if (isGetAttaack == false)
            {
                Move();
                if (attackRange.OverlapPoint(PlayerController.Instance.transform.position))
                {
                    if (attackTimer < 0)
                    {
                        attackTimer = attackTime;
                    }
                    if (attackTimer > 3.5)
                    {
                        rigidbody2D.AddForce(lookDirection * spurtForce);
                    }
                }
                attackTimer -= Time.deltaTime;
            }
        }
    }
    public IEnumerator BeginAction()
    {
        yield return new WaitForSeconds(4f);
        beginAction = true;
    }

    void Move()
    {
        //移动模式
        if (isFindPlayer == true)
        {
            AttackMove();
        }
        else
        {
            //检测玩家
            //RaycastHit2D hitForward = Physics2D.Raycast(rigidbody2D.position, lookDirection, 1000f, LayerMask.GetMask("Player"));
            //Debug.DrawRay(rigidbody2D.position, lookDirection * 1000f, Color.red);
            //RaycastHit2D hitBack = Physics2D.Raycast(rigidbody2D.position, lookDirection * -1, 1000f, LayerMask.GetMask("Player"));
            //Debug.DrawRay(rigidbody2D.position, lookDirection * 1000f, Color.red);
            //if (hitForward.collider != null || hitBack.collider != null)
            //{
            //    isFindPlayer = true;
            //}

            if (pursuitRange.OverlapPoint(PlayerController.Instance.transform.position))
            {
                isFindPlayer = true;
            }

            IdleMove();
        }

        rigidbody2D.velocity = new Vector2(lookDirection.x * moveSpeed * Time.deltaTime * 10, rigidbody2D.velocity.y);
        enemySprite.transform.localScale = new Vector3(lookDirection.x * Mathf.Abs(enemySprite.transform.localScale.x), enemySprite.transform.localScale.y, enemySprite.transform.localScale.z);
    }

    void IdleMove()
    {
        //巡逻移动
        if (moveTimer < 0)
        {
            moveTimer = moveTime;
            lookDirection.x *= -1;
        }
        else
        {
            moveTimer -= Time.deltaTime;
        }
    }

    void AttackMove()
    {
        //攻击移动
        if (transform.position.x < PlayerController.Instance.transform.position.x)
        {
            lookDirection.x = 1;
        }
        else
        {
            lookDirection.x = -1;
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawRay()
    //}

    public void ChangeHealth(float damage, bool isCarbonicAcid)
    {
        if(isCarbonicAcid == true)
        {
            //直接被秒杀
            return;
        }
        currentHealth += damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        if (pc != null)
        {
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            StartCoroutine(MakeAttack());
        }
    }

    public IEnumerator MakeAttack()
    {
        attackTimer = 3f;
        isGetAttaack = true;
        rigidbody2D.AddForce((lookDirection * -1 + new Vector2(0, 1)) * 400);
        yield return new WaitForSeconds(spurtTime * Time.deltaTime);
        isGetAttaack = false;
    }
}
