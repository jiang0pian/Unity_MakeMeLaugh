using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColafriementosScript : Prop
{
    public float attackDurationTime = 1f;
    private float attackDurationTimer = -1f;

    public float lassoDurationTime = 10f;
    private float lassoDurationTimer = -1f;

    private GameObject attackRange;


    private void Update()
    {
        attackDurationTimer -= Time.deltaTime;
        lassoDurationTimer -= Time.deltaTime;
        if (lassoDurationTimer < 0)
        {
            Destroy(attackRange);
        }
    }
    public override void UseProp()
    {
        attackRange = Instantiate(itemPrefab, PlayerController.Instance.GetComponent<Rigidbody2D>().position, Quaternion.identity);
        lassoDurationTimer = lassoDurationTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        EnemyController ec = collision.gameObject.GetComponent<EnemyController>();
        if (ec != null)
        {
            if (attackDurationTimer < 0)
            {
                attackDurationTimer = attackDurationTime;
                ec.ChangeHealth(-30, false);
            }
        }
    }
}