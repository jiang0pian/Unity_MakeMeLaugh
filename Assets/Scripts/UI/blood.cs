using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blood : MonoBehaviour
{
    public PlayerAttribute playerAttribute;
    public GameObject player;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerAttribute = player.GetComponent<PlayerAttribute>();
    }


    // Update is called once per frame
    void Update()
    {
        if (playerAttribute.currentHealth == 2)
        {
            animator.SetFloat("Blend", 0.6f);
        }
        if (playerAttribute.currentHealth == 1)
        {
            animator.SetFloat("Blend", 0.9f);
        }
    }
}
