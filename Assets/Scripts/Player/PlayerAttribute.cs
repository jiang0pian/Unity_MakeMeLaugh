using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerAttribute : MonoBehaviour
{
    public static PlayerAttribute Instance;
    [Header("Basic Attribute")]
    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;
    public float jumpForce;
    public Animator animator;

    private void Awake()
    {
        Instance = this;
        currentHealth = maxHealth;
    }
}
