using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookies : MonoBehaviour

{
    public Collider2D door; // 饼干的碰撞体
    public bool isCookiePressd =false; // 饼干是否被碰到
    public Animator animator;
    public void Start()
    {
        animator = GetComponent<Animator>();
    }
private void OnCollisionEnter2D(Collision2D collision)
{
    
    if (collision.gameObject.CompareTag("Player"))
    {
            animator.SetBool("suilie", true);
            StartCoroutine(DelayedExecution());
        }
}

IEnumerator DelayedExecution()
{
    yield return new WaitForSeconds(1.0f);
    isCookiePressd = true;
}

    private void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            isCookiePressd = false;
        }
    }
    void Update()
    {
        
        if (isCookiePressd)
        {                             
            Destroy(gameObject);
            
        }
        
    }

}
