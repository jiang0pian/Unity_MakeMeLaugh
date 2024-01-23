using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private new Rigidbody2D rigidbody2D;

    public bool isMoving;
    public float isMovingThreshold;
    public bool isOnAir;
    public float getHitTimeInterval = 0.5f;
    public float getHitCounter;

    //public Animator animator;
    public GameObject playerSprite;
    public PlayerAttribute playerAttribute;

    public GameState gameState;

    private void Awake()
    {
        Instance = this;
        playerAttribute = GetComponent<PlayerAttribute>();
        rigidbody2D = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        GetKeyDown();
    }
    private void FixedUpdate()
    {
        if (gameState == GameState.Gaming)
        {
            Time.timeScale = 1;            
            Movement();
            if (getHitCounter > 0)
            {
                getHitCounter -= Time.deltaTime;
            }
        }
        else if (gameState == GameState.Pause)
        {
            Time.timeScale = 0;            
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

            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                playerSprite.transform.localScale = new Vector3(Mathf.Abs(playerSprite.transform.localScale.x), playerSprite.transform.localScale.y, playerSprite.transform.localScale.z);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                playerSprite.transform.localScale = new Vector3(-1 * Mathf.Abs(playerSprite.transform.localScale.x), playerSprite.transform.localScale.y, playerSprite.transform.localScale.z);
            }
        }
        else
        {
            isMoving = false;
        }
    }
    public void Jump()
    {
        rigidbody2D.AddForce(transform.up * playerAttribute.jumpForce, ForceMode2D.Impulse);
    }
    private void GetKeyDown()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if (GetComponent<PhysicsCheck>().isOnGround==true)
            {
                Jump();
            }
        }
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
