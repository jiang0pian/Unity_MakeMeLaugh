using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabbageEnemy : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;

    public float maxHealth;
    public float currentHealth;
    public float jumpForce = 50f;

    public float jumpDurationTime = 2f;

    private int jumpNum = 0;

    private bool isFindPlayer;

    private Vector2 lookDirection;

    public Collider2D pursuitRange;
    public Collider2D attackRange;

    public GameObject enemySprite;

    private void Awake()
    {
        lookDirection = new Vector2(1, 0);
    }

    private void Start()
    {
        StartCoroutine(jump());
    }

    private void Update()
    {
        if (pursuitRange.OverlapPoint(PlayerController.Instance.transform.position))
        {
            isFindPlayer = true;
        }
        if(isFindPlayer == true)
        {
            if (transform.position.x < PlayerController.Instance.transform.position.x)
            {
                lookDirection.x = 1;
                enemySprite.transform.localScale = new Vector3(lookDirection.x, enemySprite.transform.localScale.y, enemySprite.transform.localScale.z);
            }
            else
            {
                lookDirection.x = -1;
                enemySprite.transform.localScale = new Vector3(lookDirection.x, enemySprite.transform.localScale.y, enemySprite.transform.localScale.z);
            }
        }
    }

    public IEnumerator jump()
    {
        if(isFindPlayer == false)
        {
            if (jumpNum > 1)
            {
                lookDirection = -lookDirection;
                enemySprite.transform.localScale = new Vector3(lookDirection.x, enemySprite.transform.localScale.y, enemySprite.transform.localScale.z);
                jumpNum = 0;
            }
            jumpNum++;
        }
        rigidbody2D.AddForce(new Vector2(lookDirection.x, 3) * jumpForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(jumpDurationTime);
        StartCoroutine(jump());
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(GetComponent<PhysicsCheck>().isOnGround == true && attackRange.OverlapPoint(PlayerController.Instance.transform.position))
        {
            //对玩家造成伤害...................
            Debug.Log("对玩家造成了伤害");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("attack"))
        {
            Debug.Log("攻击到敌人了");
            ChangeHealth(+3);
        }
    }

}
