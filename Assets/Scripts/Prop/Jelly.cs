using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    private float jellyForce = 70.0f;           // �����ĵ���
    private Rigidbody2D jellyRb;                // �����ĸ������

    // Start is called before the first frame update
    void Start()
    {
        // ��ȡʵ����������Rigidbody2D���
        jellyRb = this.GetComponent<Rigidbody2D>();

        // ȷ����������ӵ��Rigidbody�����ʹ���ܵ�����Ӱ�첢�ܹ�׹�������档
        if (jellyRb != null)
        {
            jellyRb.mass = 1000000; // ����һ���ܴ������ֵ
            jellyRb.gravityScale = 1; // ȷ�������ܵ�����Ӱ�죬���Ը�����Ҫ����
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // ȷ�����2D��Ϸ��������Ӧ��Collider2D�������BoxCollider2D, CircleCollider2D�ȣ�
    // ������ײ�Ķ���Ҳ��Ҫ��Collider2D���
    // ��������һ�������Rigidbody2D����Ϊ��Kinematic�����ܼ�⵽��ײ������OnCollisionEnter2D������
    void OnCollisionEnter2D(Collision2D collision)
    {
        // ��ȡ��ײ�����ĸ���
        Rigidbody2D otherRb = collision.collider.GetComponent<Rigidbody2D>();
        if (otherRb != null)
        {
            Debug.Log("otherRb��ȡ�ɹ�\n");

            // ʩ��˲ʱ��
            otherRb.AddForce(new Vector2(0, jellyForce), ForceMode2D.Impulse);

            //// ��ȡ�����ٶ�����
            //Vector2 incomingVector = otherRb.velocity;
            //Debug.Log("incomingVector = (" + incomingVector.x + "," + incomingVector.y + ")\n");

            //// ��ȡ��ײ��ķ���
            //Vector2 normal = collision.contacts[0].normal;
            //Debug.Log("normal = (" + normal.x + "," + normal.y + ")\n");

            //// ���㷴���ٶ����������㷴�䷽�򣺷��䷽�� = ���䷽�� - 2 * (���䷽�� �� ����) * ����
            //Vector2 reflectVector = Vector2.Reflect(incomingVector, normal);

            // �������ٶȳ��Է���������Ϊ�����ٶ�
            //float reflectionSpeed = incomingVector.magnitude * reflectionSpeedMultiple;

            //// �������˶���������Ϊ���䵥λ�������Է����ٶ�
            //otherRb.velocity = reflectVector.normalized * reflectionSpeed;
            //Debug.Log("reflectionVelocity = (" + otherRb.velocity.x + "," + otherRb.velocity.y + ")\n");
        }
        else
        {
            Debug.Log("otherRb==null\n");
        }
    }
}
