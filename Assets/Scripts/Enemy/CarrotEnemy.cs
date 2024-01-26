using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotEnemy : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;

    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;

    private bool isFindPlayer;

    private Vector2 lookDirection;

    private float moveTime = 2f;
    private float moveTimer = -1;

    public GameObject enemySprite;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
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
        //�ƶ�ģʽ
        if (isFindPlayer == true)
        {
            AttackMove();
        }
        else
        {
            //������
            RaycastHit2D hitForward = Physics2D.Raycast(rigidbody2D.position, lookDirection, 4f, LayerMask.GetMask("Player"));
            RaycastHit2D hitBack = Physics2D.Raycast(rigidbody2D.position, lookDirection * -1, 4f, LayerMask.GetMask("Player"));
            if (hitForward.collider != null || hitBack.collider != null)
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
        //Ѳ���ƶ�
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
        //�����ƶ�
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

    public void ChangeHealth(float damage)
    {
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
}
