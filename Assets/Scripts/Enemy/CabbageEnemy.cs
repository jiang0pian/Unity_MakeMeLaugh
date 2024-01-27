using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabbageEnemy : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;

    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;

    private bool isFindPlayer;

    private Vector2 lookDirection;

    public Collider2D pursuitRange;
    public Collider2D attackRange;

    public GameObject enemySprite;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        lookDirection = new Vector2(1, 0);
    }



    public void ChangeHealth(float damage)
    {
        currentHealth += damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

}
