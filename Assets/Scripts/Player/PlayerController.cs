using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private new Rigidbody2D rigidbody2D;

    public bool isMoving;
    public float isMovingThreshold;
    public float getHitTimeInterval = 1f;
    public float getHitCounter;

    public bool isUsingColajetpack = false;
    public bool isFly = false;

    public float riseForce;
    public float maxRiseSpeed;
    public float riseForceUltimate;
    public float maxRiseSpeedUltimate;

    public bool isUltimate;

    public bool isSpurt = false;
    public Vector2 chongshengdian;

    //人物朝向
    public Vector2 lookDirection;

    public Animator animator;
    public GameObject playerSprite;
    public PlayerAttribute playerAttribute;

    public GameState gameState;

    public bool isFrenchfrie;
    public bool isColafrie;
    public bool isShiled;
    void Start () {
        animator = GetComponent<Animator>();
      }


    private void Awake()
    {
        Instance = this;
        playerAttribute = GetComponent<PlayerAttribute>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        lookDirection = new Vector2(1, 0);
    }
    
    private void Update()
    {
        GetKeyDown();
        if (gameState == GameState.Gaming)
        {
            Time.timeScale = 1;
            
            if (getHitCounter > 0)
            {   
                getHitCounter -= Time.deltaTime;
            }
        }
        else if (gameState == GameState.Pause)
        {
            Time.timeScale = 0;
        }
        if(isMoving){ 
            animator.SetBool("walk", true);
        }
        else{
            animator.SetBool("walk", false);
        }

        //if (MathF.Abs(rigidbody2D.velocity.x) <= playerAttribute.moveSpeed / 2)
        //{
        //    rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
        //}
        if(isFly == true)
        {
            //ColajetpackScript.instance.Fly();
            //调用道具的Fly（）函数
            if(isUltimate == false)
            {
                Fly(riseForce, maxRiseSpeed);
            }
            else
            {
                Fly(riseForceUltimate, maxRiseSpeedUltimate);
            }
        }
        if (transform.position.y < -70f)
        {
          transform.position= chongshengdian;
        }
    }
    private void FixedUpdate()
    {
        if (gameState == GameState.Gaming)
        {
                Movement();
        }
        else if (gameState == GameState.Pause)
        {

        }

    }
    //Player Movement
    public void Movement()
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        if (rigidbody2D.velocity.magnitude > isMovingThreshold||Mathf.Abs(Input.GetAxisRaw("Horizontal"))>0)
        {
            isMoving = true;
            rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * playerAttribute.moveSpeed * Time.deltaTime,rigidbody2D.velocity.y);
            //rigidbody2D.velocity += new Vector2(Input.GetAxisRaw("Horizontal") * playerAttribute.moveSpeed * Time.deltaTime, 0);
            //rigidbody2D.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * playerAttribute.moveSpeed, 0f), ForceMode2D.Force);
            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                //if (isAPressed == true)
                //{
                //    rigidbody2D.velocity += new Vector2(1 * playerAttribute.moveSpeed, 0);
                //}
                //isAPressed = false;
                //if (isDPressed == false)
                //{
                //    isDPressed = true;
                //    rigidbody2D.velocity += new Vector2(1 * playerAttribute.moveSpeed, 0);
                //}
                playerSprite.transform.localScale = new Vector3(Mathf.Abs(playerSprite.transform.localScale.x), playerSprite.transform.localScale.y, playerSprite.transform.localScale.z);
                //更新人物朝向
                lookDirection.x = 1f;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                //if (isDPressed == true)
                //{
                //    rigidbody2D.velocity += new Vector2(-1 * playerAttribute.moveSpeed, 0);
                //}
                //isDPressed = false;
                //if (isAPressed == false)
                //{
                //    isAPressed = true;
                //    rigidbody2D.velocity += new Vector2(-1 * playerAttribute.moveSpeed, 0);
                //}
                playerSprite.transform.localScale = new Vector3(-1 * Mathf.Abs(playerSprite.transform.localScale.x), playerSprite.transform.localScale.y, playerSprite.transform.localScale.z);
                //更新人物朝向
                lookDirection.x = -1f;
            }

        }
        else
        {
            isMoving = false;
            //if (Input.GetAxisRaw("Horizontal") == 0f)
            //{
            //    if (isAPressed == true)
            //    {
            //        rigidbody2D.velocity += new Vector2(1 * playerAttribute.moveSpeed, 0);
            //    }
            //    if (isDPressed == true)
            //    {
            //        rigidbody2D.velocity += new Vector2(-1 * playerAttribute.moveSpeed, 0);
            //    }
            //    isAPressed = false;
            //    isDPressed = false;

            //}
        }
    }
    public void Fly(float Force, float maxUpSpeed)
    {
        PlayerController.Instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * Force, ForceMode2D.Impulse);
        if (PlayerController.Instance.GetComponent<Rigidbody2D>().velocity.y > maxUpSpeed)
        {
            PlayerController.Instance.GetComponent<Rigidbody2D>().velocity = new Vector2(PlayerController.Instance.GetComponent<Rigidbody2D>().velocity.x, maxUpSpeed);
        }
    }
    public void Jump()
    {
        rigidbody2D.AddForce(transform.up * playerAttribute.jumpForce, ForceMode2D.Impulse);
    }
    public void ChangeHealth(float damage)
    {
        playerAttribute.currentHealth += damage;
        if (playerAttribute.currentHealth < 0)
        {
            playerAttribute.currentHealth = 0;
        }
        else if (playerAttribute.currentHealth > playerAttribute.maxHealth)
        {
            playerAttribute.currentHealth = playerAttribute.maxHealth;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log("被攻击了");
            if (getHitCounter <= 0)
            {
                if (isShiled==true)
                {
                    isShiled = false;
                    return;
                }
                ChangeHealth(-1);
                animator.SetTrigger("shouji");
                getHitCounter = getHitTimeInterval;
            }
        }
    }
    private void OnCollisionrEnter2D(Collider2D collision)

    {
        Debug.Log("1");
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log("被攻击了");
            if (getHitCounter <= 0)
            {
                if (isShiled == true)
                {
                    isShiled = false;
                    return;
                }
                ChangeHealth(-1);
                animator.SetTrigger("shouji");
                getHitCounter = getHitTimeInterval;
            }
            
            
        }
    }
    private void GetKeyDown()
    {
        if(isUsingColajetpack == false)
        {
            if (Input.GetKeyDown(KeyCode.W) && GetComponent<PhysicsCheck>().isOnGround == true)
            {
                Jump();
            }
        }
        else if(isUsingColajetpack == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                isFly = true;
            }
            if(Input.GetKeyUp(KeyCode.W))
            {
                isFly = false;
            }
        }
        //if(Input.GetKeyDown(KeyCode.W) && isUsingColajetpack == false)
        //{
        //    if (GetComponent<PhysicsCheck>().isOnGround==true)
        //    {
        //        Jump();
        //    }
        //}else if(Input.GetKeyDown(KeyCode.W) && isUsingColajetpack == true)
        //{
        //    //ColajetpackScript.instance.Fly();
            
        //}
        if (Input.GetKeyDown(KeyCode.I))
        {
            BagController.Instance.OpenAndCloseBag();
        }
        //按下 F 键
        if (Input.GetKeyDown(KeyCode.F) && isSpurt == false)
        {
            //StartCoroutine(ColarScript.Instance.Spurt());
            //NewBehaviourScript.Instance.UseProp();
            //ColajetpackScript.instance.UseProp();
            //ColarScript.instance.UseProp();
            
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //StartCoroutine(ColarScript.Instance.Spurt());
            //NewBehaviourScript.Instance.UseProp();
            //ColajetpackScript.instance.UseProp();
            //BoltfrieScript.instance.UseProp();
            
        }
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    rigidbody2D.velocity += new Vector2(-1 * playerAttribute.moveSpeed, 0);
        //}
        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    if(rigidbody2D.velocity.x < -playerAttribute.moveSpeed / 2)
        //    {
        //        rigidbody2D.velocity += new Vector2(1 * playerAttribute.moveSpeed, 0);
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    rigidbody2D.velocity += new Vector2(1 * playerAttribute.moveSpeed, 0);
        //}
        //if (Input.GetKeyUp(KeyCode.D))
        //{
        //    if (rigidbody2D.velocity.x > playerAttribute.moveSpeed / 2)
        //    {
        //        rigidbody2D.velocity += new Vector2(-1 * playerAttribute.moveSpeed, 0);
        //    }

        //}

    }

    public void SetGameState(GameState newGameState)
    {
        gameState = newGameState;
    }
}

public enum GameState
{
    Gaming,
    Pause,
}
