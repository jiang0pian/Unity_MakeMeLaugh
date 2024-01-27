using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gumdrop : MonoBehaviour
{
    public Rigidbody2D anchor;
    public float releaseTime;


    private Rigidbody2D rb;
    private bool isPressed;
    private float maxDragDistance = 2f;     //����������
    private TrailRenderer trailRenderer;

    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //anchor = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        //trailRenderer = GetComponent<TrailRenderer>();
    }
    private void Update()
    {
        if (isPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(mousePos, anchor.position) > maxDragDistance)
            {
                rb.position = anchor.position + (mousePos - anchor.position).normalized * 2f;
            }
            else
            {
                rb.position = mousePos;
            }

        }
    }
    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);
        GetComponent<SpringJoint2D>().enabled = false;
        this.enabled = false;
    }
    private void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;  //��������Ϊ���˶�ѧ�����䲻���������ã���ֹ����϶��Ĺ����е���
    }
    private void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;
        trailRenderer.enabled = true;
        StartCoroutine(Release());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

}
