using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColafrielassoScript : Prop
{
    public float attackDurationTime = 1f;
    private float attackDurationTimer = -1f;

    public float lassoDurationTime = 10f;
    private float lassoDurationTimer = -1f;

    private GameObject attackRange;
    private int count = 0;
    private int i = 1;
    private Rigidbody2D rb;

    private void Awake()
    {
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.
        rb.position = PlayerController.Instance.GetComponent<Rigidbody2D>().position + new Vector2(0, 5);
    }
    private void Update()
    {
        count++;
        if (count == 20) 
        { 
            count = 0;
            i = -i;
            this.GetComponent<Transform>().rotation = Quaternion.Euler(0, 90+90*i, 0); ;
        }
        rb.transform.position = PlayerController.Instance.transform.position + new Vector3(0, 5, 0);
        attackDurationTimer -= Time.deltaTime;
        lassoDurationTimer -= Time.deltaTime;
        if(lassoDurationTimer < 0)
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
            if(attackDurationTimer < 0)
            {
                attackDurationTimer = attackDurationTime;
                ec.ChangeHealth(-20, false);
            }
        }
    }
}
