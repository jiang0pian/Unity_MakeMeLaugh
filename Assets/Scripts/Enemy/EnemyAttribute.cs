using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttribute : MonoBehaviour
{
    public static EnemyAttribute Instance { get; private set; }
    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;

    private void Awake()
    {
        Instance = this;
        currentHealth = maxHealth;
    }

}
