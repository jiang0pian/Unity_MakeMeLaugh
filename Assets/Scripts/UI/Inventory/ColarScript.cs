using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class ColarScript : Prop
{
    public static ColarScript Instance { get; private set; }
    public float spurtForce = 1f;
    public float spurtTime = 1;

    private void Start()
    {
        Instance = this;
    }
    public override void UseProp()
    {
        
        StartCoroutine(Spurt());
        
    }
    public IEnumerator Spurt()
    {
        PlayerController.Instance.isSpurt = true;
        float oldSpeed = PlayerController.Instance.playerAttribute.moveSpeed;
        for(int i = 0; i < 20 * spurtTime; i++)
        {
            PlayerController.Instance.playerAttribute.moveSpeed *= 1.0f + spurtForce * 0.1f;
            yield return new WaitForSeconds(0.05f);
        }
        PlayerController.Instance.playerAttribute.moveSpeed = oldSpeed;
        PlayerController.Instance.isSpurt = false;
        //Debug.Log("End");
    }
}
