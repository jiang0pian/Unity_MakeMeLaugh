using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttribute : MonoBehaviour
{
    public static PlayerAttribute Instance;

    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;    

    private void Awake()
    {
        Instance = this;
        currentHealth = maxHealth;
    }
}
