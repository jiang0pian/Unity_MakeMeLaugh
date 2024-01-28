using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{

    private new Rigidbody2D rigidbody2D;

    private bool isFindPlayer;

    private Vector2 lookDirection;

    private float moveTime = 2f;
    private float moveTimer = -1;

    public GameObject enemySprite;

    public EnemyAttribute enemyAttribute;
    public Animator animator;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        enemyAttribute = GetComponent<EnemyAttribute>();
        isFindPlayer = false;
        lookDirection = new Vector2(1, 0);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
            RaycastHit2D hitForward = Physics2D.Raycast(rigidbody2D.position, lookDirection, 4f, LayerMask.GetMask("Player"));
            RaycastHit2D hitBack = Physics2D.Raycast(rigidbody2D.position, lookDirection * -1, 4f, LayerMask.GetMask("Player"));
            if (hitForward.collider != null || hitBack.collider != null)
            {
                isFindPlayer = true;
            }


            IdleMove();
        }

        rigidbody2D.velocity = new Vector2(lookDirection.x * enemyAttribute.moveSpeed * Time.deltaTime * 10, rigidbody2D.velocity.y);
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
        if(transform.position.x < PlayerController.Instance.transform.position.x)
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

    public void ChangeHealth(float damage)
    {
        enemyAttribute.currentHealth += damage;
        if (enemyAttribute.currentHealth < 0)
        {
            enemyAttribute.currentHealth = 0;
        }
        else if(enemyAttribute.currentHealth > enemyAttribute.maxHealth)
        {
            enemyAttribute.currentHealth = enemyAttribute.maxHealth;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("attack"))
        {
            Debug.Log(collision.gameObject.tag);
            Debug.Log("攻击到敌人了");

                Debug.Log("敌人扣血了");
                ChangeHealth(+3); 
        }
    }

}
