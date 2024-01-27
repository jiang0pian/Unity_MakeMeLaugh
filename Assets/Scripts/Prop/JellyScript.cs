using UnityEngine;

public class JellyScript : Prop
{
    public GameObject jellyPrefab;              // 果冻预制体
    public Transform playerTransform;           // 玩家的Transform组件
    private GameObject jellyInstance;           // 实例化的果冻对象
    private Rigidbody2D jellyRb;                // 实例化的果冻的刚体组件
    private float jellyForce = 70.0f;            // 果冻的弹力

    // 覆写Prop类中的UseProp方法
    public override void UseProp()
    {
        // 确保果冻预制体
        if (jellyPrefab != null && playerTransform != null)
        {
            // 在玩家当前位置实例化果冻对象
            jellyInstance = Instantiate(jellyPrefab, playerTransform.position, Quaternion.identity);

            // 获取实例化果冻的Rigidbody2D组件
            jellyRb = jellyInstance.GetComponent<Rigidbody2D>();

            // 如果果冻有Rigidbody2D组件，则设置其质量为一个很大的值
            if (jellyRb != null)
            {
                jellyRb.mass = 1000; // 设置一个很大的质量值
                jellyRb.gravityScale = 1; // 确保果冻受到重力影响，可以根据需要调整
            }
        }
    }

    void Start()
    {
        // 在Start方法中可以执行初始化操作
        // 例如：获取组件、设置初始状态等
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
