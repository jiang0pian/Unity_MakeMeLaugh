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

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        enemyAttribute = GetComponent<EnemyAttribute>();
        isFindPlayer = false;
        lookDirection = new Vector2(1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        //ÒÆ¶¯Ä£Ê½
        if (isFindPlayer == true)
        {
            AttackMove();
        }
        else
        {
            //¼ì²âÍæ¼Ò
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
        //Ñ²ÂßÒÆ¶¯
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
        //¹¥»÷ÒÆ¶¯
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
}
