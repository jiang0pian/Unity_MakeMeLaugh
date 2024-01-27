using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frenchfrie : Prop
{
    public static Frenchfrie instance;
    public Animator animator;

    private void Start()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }

    public override void UseProp()
    {
        StartCoroutine(useshutiao());
        Debug.Log("11");
    }
    public IEnumerator useshutiao()
    {
        animator.SetTrigger("shutiao");
        yield return null; 
        yield return new WaitForSeconds(0); 
        yield return true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log(collision.gameObject.tag);
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.ChangeHealth(3);
            }
        }
    }
}