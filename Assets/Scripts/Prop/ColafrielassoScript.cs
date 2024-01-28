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

    public float limitMoveTime = 3f;

    private EnemyController ec;

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
        Debug.Log("UseProp()");
        attackRange = Instantiate(itemPrefab, PlayerController.Instance.GetComponent<Rigidbody2D>().position, Quaternion.identity);
        lassoDurationTimer = lassoDurationTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        ec = collision.gameObject.GetComponent<EnemyController>();
        if (ec != null)
        {
            if (attackDurationTimer < 0)
            {
                attackDurationTimer = attackDurationTime;
                ec.ChangeHealth(-15, false);
            }
            StartCoroutine(LimitMove());
        }
    }

    public IEnumerator LimitMove()
    {
        Debug.Log("LimitMove()");
        ec.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(limitMoveTime);
        ec.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Debug.Log("end()");
    }
}
