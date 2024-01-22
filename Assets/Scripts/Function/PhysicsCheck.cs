using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public bool isOnGround;
    public float checkRadius;
    public Vector2 playerFootPoint;
    public LayerMask groundLayer;

    private void Update()
    {
        CheckIsOnGround();
    }
    public void CheckIsOnGround()
    {
        isOnGround = Physics2D.OverlapCircle((Vector2)transform.position+playerFootPoint, checkRadius, groundLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + playerFootPoint, checkRadius);
    }
}
