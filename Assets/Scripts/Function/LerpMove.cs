using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LerpMove : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public float moveTime;

    public bool isMoving;
    private float timeCounter;
    private float movePercent;

    public void BeginMove(Transform startTransform, Transform endTransform)
    {
        this.start = startTransform;
        this.end = endTransform;
        transform.position = start.position;
        isMoving = true;
    }
    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, end.position, 0.02f);
            if ((transform.position - end.position).magnitude < 1)
            {
                transform.position = end.position;
                isMoving = false;
            }            
        }
    }

}
