using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColafrielassoScript : Prop
{
    private int count = 0;
    private int i = 1;
    private Rigidbody2D rb;                     // Colafrielasso�ĸ���
    private Collider2D coll;                    // Colafrielasso����ײ��
    private float maxDragDistance = 10.0f;      // ������ʱ��

    public override void UseProp()
    {
        Debug.Log("ʹ��Colafrielasso����\n");

        // ȷ��ColafrielassoԤ����ǿ�
        if (itemPrefab != null)
        {
            Debug.Log("itemPrefab��ȡ�ɹ�\n");
            // ��ȡ��ҵ�ǰλ��
            Vector2 position = PlayerController.Instance.transform.position;

            // ������ƫ������λ��
            position.y += 3.0f;

            // ��ƫ��λ��ʵ����Colafrielasso����
            Instantiate(itemPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.Log("itemPrefab == null\n");
        }
    }

    private void Start()
    {
        
        rb.position = PlayerController.Instance.GetComponent<Rigidbody2D>().position + new Vector2(0, 5);
        if (this.GetComponent<Rigidbody2D>() != null)
        {
            rb = GetComponent<Rigidbody2D>();
            // ȷ����������Ӱ��
            rb.gravityScale = 0;
            // ��С��С
            rb.transform.localScale = new Vector3(2, 2, 2);
            // ʹColafrielasso��ײ�����ã�����Ϊ������
            coll = this.GetComponent<Collider2D>();
            coll.isTrigger = true;
            // ����һ��Э�̵��ã���������ɳ�һ��ʱ���δ�����٣�����������
            StartCoroutine(DestroyAfterTime(maxDragDistance));
        }
    }
    private void Update()
    {
        count++;
        if (count == 20) 
        { 
            count = 0;
            i = -i;
            rb.transform.rotation = Quaternion.Euler(0, 90+90*i, 0); ;
        }
        rb.transform.position = PlayerController.Instance.transform.position + new Vector3(0, 3, 0);
    }
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        PropEffect(collision);
    }

    // ������Ч���Ч��ʵ��
    private void PropEffect(Collider2D other)
    {
        if (other.gameObject.GetComponent<EnemyController>() != null)
        {
            // ���ù���ļ�ŭ����
            other.gameObject.GetComponent<EnemyController>().isFindPlayer = false;
        }
    }

    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
