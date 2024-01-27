using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    private float jellyForce = 70.0f;           // 果冻的弹力
    private Rigidbody2D jellyRb;                // 果冻的刚体组件

    // Start is called before the first frame update
    void Start()
    {
        // 获取实例化果冻的Rigidbody2D组件
        jellyRb = this.GetComponent<Rigidbody2D>();

        // 确保果冻对象拥有Rigidbody组件以使其受到重力影响并能够坠落至地面。
        if (jellyRb != null)
        {
            jellyRb.mass = 1000000; // 设置一个很大的质量值
            jellyRb.gravityScale = 1; // 确保果冻受到重力影响，可以根据需要调整
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 确保你的2D游戏对象有相应的Collider2D组件（如BoxCollider2D, CircleCollider2D等）
    // 并且碰撞的对象也需要有Collider2D组件
    // 其中至少一个对象的Rigidbody2D设置为非Kinematic，才能检测到碰撞并触发OnCollisionEnter2D方法。
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 获取碰撞果冻的刚体
        Rigidbody2D otherRb = collision.collider.GetComponent<Rigidbody2D>();
        if (otherRb != null)
        {
            Debug.Log("otherRb获取成功\n");

            // 施加瞬时力
            otherRb.AddForce(new Vector2(0, jellyForce), ForceMode2D.Impulse);

            //// 获取入射速度向量
            //Vector2 incomingVector = otherRb.velocity;
            //Debug.Log("incomingVector = (" + incomingVector.x + "," + incomingVector.y + ")\n");

            //// 获取碰撞点的法线
            //Vector2 normal = collision.contacts[0].normal;
            //Debug.Log("normal = (" + normal.x + "," + normal.y + ")\n");

            //// 计算反射速度向量，计算反射方向：反射方向 = 入射方向 - 2 * (入射方向 · 法线) * 法线
            //Vector2 reflectVector = Vector2.Reflect(incomingVector, normal);

            // 将入射速度乘以反弹倍数设为反射速度
            //float reflectionSpeed = incomingVector.magnitude * reflectionSpeedMultiple;

            //// 将刚体运动向量设置为反射单位向量乘以反射速度
            //otherRb.velocity = reflectVector.normalized * reflectionSpeed;
            //Debug.Log("reflectionVelocity = (" + otherRb.velocity.x + "," + otherRb.velocity.y + ")\n");
        }
        else
        {
            Debug.Log("otherRb==null\n");
        }
    }
}
