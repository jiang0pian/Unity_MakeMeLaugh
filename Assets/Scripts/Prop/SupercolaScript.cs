using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupercolaScript : Prop
{
    public float spurtForce = 10f;
    private Vector2 force;


    public override void UseProp()
    {
        force.y = 0f;
        if (PlayerController.Instance.playerSprite.transform.localScale.x > 0)
        {
            force.x = 1f;
        }
        else
        {
            force.x = -1f;
        }
        PlayerController.Instance.GetComponent<Rigidbody2D>().AddForce(force * spurtForce,ForceMode2D.Force);
    }
}
