using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public new Rigidbody2D rigidbody2D;

    private bool isMoving;
    public float isMovingThreshold;
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
        gameState = GameState.Pause;
    }
    private void FixedUpdate()
    {
        Movement();
    }
    //½ÇÉ«ÒÆ¶¯
    public void Movement()
    {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        //moveInput.x = Input.GetAxisRaw("Horizontal");
        //moveInput.y = Input.GetAxisRaw("Vertical");
        //moveInput.Normalize();
        if (rigidbody2D.velocity.magnitude > isMovingThreshold||Mathf.Abs(Input.GetAxisRaw("Horizontal"))>0)
        {
            isMoving = true;
            rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * playerAttribute.moveSpeed * Time.deltaTime,rigidbody2D.velocity.y);
            //animator.SetBool("isMoving", isMoving);
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
            //animator.SetBool("isMoving", isMoving);
        }
    }
}

public enum GameState
{
    Gaming,
    Pause,

}
