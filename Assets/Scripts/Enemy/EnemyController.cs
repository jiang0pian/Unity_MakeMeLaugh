using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class EnemyController : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public bool beginAction = false;
    public bool moveSpeed;

    public abstract void ChangeHealth(float damage, bool isCarbonicAcid);

    

}
