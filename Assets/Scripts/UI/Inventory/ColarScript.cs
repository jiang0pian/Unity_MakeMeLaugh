using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class ColarScript : Item
{
    public static ColarScript Instance { get; private set; }
    public float spurtForce = 1f;
    public int spurtTime = 1;

    private void Start()
    {
        Instance = this;
    }


    public IEnumerator Spurt()
    {
        //Debug.Log("Start");
        //playerRigidbody2D.AddForce(PlayerController.Instance.lookDirection * spurtForce * 100);
        float oldSpeed = PlayerController.Instance.playerAttribute.moveSpeed;
        for(int i = 0; i < 20 * spurtTime; i++)
        {
            PlayerController.Instance.playerAttribute.moveSpeed *= 1.0f + spurtForce * 0.1f;
            yield return new WaitForSeconds(0.05f);
        }
        PlayerController.Instance.playerAttribute.moveSpeed = oldSpeed;
        //Debug.Log("End");
    }
}
