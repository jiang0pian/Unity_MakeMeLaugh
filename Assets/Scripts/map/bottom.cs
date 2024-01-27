using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottom : MonoBehaviour
{
    public GameObject button; 
    public Collider2D door; 
    public bool isButtonPressed =false; 
    public bool isOpen = false; 
    public Vector2 openSize;
    public Vector2 closedSize; 
    public GameObject doorCollider; 
    public Animator animator;
        private void Start()
    {
        door= doorCollider.GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }


private void OnTriggerEnter2D(Collider2D collision)
{

    if (collision.gameObject.CompareTag("Player"))
    {
        isButtonPressed = true; 
    }
}

private void OnTriggerExit2D(Collider2D collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {      
        isButtonPressed = false;     
    }
}

private void FixedUpdate()

{
     if(isButtonPressed){ 
            animator.SetBool("bottonon", true);
            animator.SetBool("open", true);
        }
        else{
            animator.SetBool("bottonon", false);
            animator.SetBool("open", false);
        }
        
        if (isButtonPressed && !isOpen)
        {
            isOpen = true;
            // 设置碰撞体为打开状态
            door.offset =openSize;
        }
        if (!isButtonPressed && isOpen)
        {
            isOpen = false;
            // 设置碰撞体为关闭状态
            Bounds bounds =door.bounds;
            bounds.size = closedSize;
            door.offset = closedSize;
        }
}
}
