using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimatecolajetpackScript : Prop
{
    public float riseForce = 20f;
    public float durationTime = 10f;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (PlayerController.Instance.isUsingColajetpack)
        {
            animator.SetBool("penqibeibao", true);
        }
        else
        {
            animator.SetBool("penqibeibao", false);
        }
    }
    public override void UseProp()
    {

        StartCoroutine(useColajetpack());

    }

    public IEnumerator useColajetpack()
    {
        PlayerController.Instance.isUsingColajetpack = true;
        yield return new WaitForSeconds(durationTime);
        PlayerController.Instance.isUsingColajetpack = false;
        PlayerController.Instance.isFly = false;
        PlayerController.Instance.isUltimate = true;
    }
}
