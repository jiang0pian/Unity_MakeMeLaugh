using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    private bool isMovingToPlayer;
    public float moveSpeed = 5f;
    public float pickupRange = 2.5f;

    private void Update()
    {
        if (PlayerController.Instance != null)
        {
            if (isMovingToPlayer)
            {
                transform.position = Vector3.MoveTowards(transform.position, PlayerController.Instance.transform.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < pickupRange)
                {
                    isMovingToPlayer = true;
                }
            }
        }
    }
}
