using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeworkEnemy : MonoBehaviour
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
    public Collider2D escapeRange;

    private float attackTime = 4;
    private float attackTimer = -1;

    private bool isGetAttaack = false;

    public float spurtForce = 100f;

    public float spurtTime = 30f;

    public GameObject enemySprite;

    




    public GameObject bulletPrefab;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        isFindPlayer = false;
        lookDirection = new Vector2(1, 0);
    }

    private void Start()
    {
        Fire();
    }

    void Update()
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



    void Move()
    {
        //移动模式
        if (isFindPlayer == true)
        {
            AttackMove();
        }
        else
        {
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

    public void ChangeHealth(float damage, bool isCarbonicAcid)
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
        isGetAttaack = true;
        rigidbody2D.AddForce((lookDirection * -1 + new Vector2(0, 1)) * 400);
        yield return new WaitForSeconds(spurtTime * Time.deltaTime);
        isGetAttaack = false;
    }

    public void Fire()
    {
        //GameObject bullet = Instantiate(bulletPrefab, rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        GameObject bullet = Instantiate(bulletPrefab, rigidbody2D.position, Quaternion.identity);
        bullet.SetActive(true);
    }
}
