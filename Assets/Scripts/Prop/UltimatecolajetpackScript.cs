using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimatecolajetpackScript : Prop
{
    public float riseForce = 20f;
    public float durationTime = 10f;
    public Animator animator;

    public float attackDurationTime = 1f;
    private float attackDurationTimer = -1f;

    private GameObject attackRange;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        attackDurationTimer -= Time.deltaTime;
        if (PlayerController.Instance.isUsingColajetpack)
        {
            animator.SetBool("penqibeibao", true);
        }
        else
        {
            animator.SetBool("penqibeibao", false);
        }
        if(attackRange != null)
        {
            attackRange.GetComponent<Rigidbody2D>().position = PlayerController.Instance.GetComponent<Rigidbody2D>().position;
        }
    }
    public override void UseProp()
    {

        StartCoroutine(useColajetpack());

    }

    public IEnumerator useColajetpack()
    {
        PlayerController.Instance.isUsingColajetpack = true;
        yield return new WaitForSeconds(durationTime);
        PlayerController.Instance.isUsingColajetpack = false;
        PlayerController.Instance.isFly = false;
        PlayerController.Instance.isUltimate = true;
        Destroy(attackRange);
        attackRange = null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        EnemyController ec = collision.gameObject.GetComponent<EnemyController>();
        if (ec != null)
        {
            if (attackDurationTimer < 0)
            {
                attackDurationTimer = attackDurationTime;
                ec.ChangeHealth(-5, false);
                Debug.Log("win");
            }
        }
    }
}
