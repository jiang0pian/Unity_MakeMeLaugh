using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class ColarScript : Item
{
    public static ColarScript Instance { get; private set; }
    private Rigidbody2D playerRigidbody2D;
    private PlayerAttribute playerAttribute;
    public float spurtForce;

    private void Start()
    {
        Instance = this;
        playerRigidbody2D = PlayerController.Instance.GetComponent<Rigidbody2D>();
    }


    public IEnumerator Spurt()
    {
        Debug.Log("Start");
        //playerRigidbody2D.AddForce(PlayerController.Instance.lookDirection * spurtForce * 100);
        float oldSpeed = PlayerController.Instance.playerAttribute.moveSpeed;
        for(int i = 0; i < 20; i++)
        {
            PlayerController.Instance.playerAttribute.moveSpeed *= 1.1f;
            yield return new WaitForSeconds(0.05f);
        }
        PlayerController.Instance.playerAttribute.moveSpeed = oldSpeed;
        Debug.Log("End");
    }
}
