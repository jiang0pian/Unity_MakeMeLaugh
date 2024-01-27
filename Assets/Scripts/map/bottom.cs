using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottom : MonoBehaviour
{
    public GameObject button; // 按钮对象
    public Collider2D door; // 门的碰撞体
    public bool isButtonPressed =false; // 按钮是否被按下
    public bool isOpen = false; // 门的状态，初始为关闭
    public Vector2 openSize; // 门打开时的碰撞体尺寸
    public Vector2 closedSize; // 门关闭时的碰撞体尺寸
    public GameObject doorCollider; // 门对象
    public Animator animator;
        private void Start()
    {
        // 获取门的碰撞体组件
        door= doorCollider.GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }


private void OnTriggerEnter2D(Collider2D collision)
{
    Debug.Log(collision.gameObject.tag);
    if (collision.gameObject.CompareTag("Player"))
    {
        isButtonPressed = true;
        Debug.Log("按下按钮");  
    }
}

private void OnTriggerExit2D(Collider2D collision)
{
    Debug.Log(collision.gameObject.tag);
    if (collision.gameObject.CompareTag("Player"))
    {      
        isButtonPressed = false;     
        Debug.Log("没按下按钮");  
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
            Debug.Log("门打开");
            // 设置碰撞体为打开状态
            door.offset =openSize;
        }
        if (!isButtonPressed && isOpen)
        {
            isOpen = false;
            Debug.Log("门关闭");
            // 设置碰撞体为关闭状态
            Bounds bounds =door.bounds;
            bounds.size = closedSize;
            door.offset = closedSize;
        }
}
}
