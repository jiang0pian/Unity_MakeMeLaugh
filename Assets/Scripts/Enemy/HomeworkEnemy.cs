using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeworkEnemy : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;

    private new Rigidbody2D rigidbody2D;

    private bool isFindPlayer;

    private Vector2 lookDirection;
}
