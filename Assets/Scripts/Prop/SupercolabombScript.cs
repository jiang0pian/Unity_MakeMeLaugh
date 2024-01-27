using UnityEngine;

public class SupercolabombScript : Prop
{
    private float distance = 3.0f;              // Supercolabomb放置距离玩家的距离
    private float jellyForce = 100.0f;          // Supercolabomb的弹力
    private float SupercolabombDamage = 5.0f;   // Supercolabomb的伤害
    private Rigidbody2D jellyRb;                // Supercolabomb的刚体组件
    public float maxHealth;                     // 最大生命值
    public float currentHealth;                 // 当前生命值

    // 覆写Prop类中的UseProp方法
    public override void UseProp()
    {
        Debug.Log("使用Supercolabomb技能\n");

        //// 使用预制体的路径（相对于Resources文件夹）来加载预制体
        //itemPrefab = Resources.Load<GameObject>("Jelly");

        // 确保Supercolabomb预制体
        if (itemPrefab != null)
        {
            Debug.Log("itemPrefab获取成功\n");
            // 获取玩家当前位置
            Vector2 position = PlayerController.Instance.transform.position;

            // 相对玩家偏移生成位置
            Debug.Log("当前玩家位置：" + position.x + "," + position.y + ")\n");
            position.x += distance * PlayerController.Instance.lookDirection.x;
            position.y -= 1.0f;

            // 在偏移位置实例化Supercolabomb对象
            Instantiate(itemPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.Log("itemPrefab == null\n");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // 获取实例化Supercolabomb的Rigidbody2D组件
        jellyRb = this.GetComponent<Rigidbody2D>();
        maxHealth = 1;
        currentHealth = 1;

        // 确保Supercolabomb对象拥有Rigidbody组件以使其受到重力影响并能够坠落至地面。
        if (jellyRb != null)
        {
            jellyRb.mass = 1000000; // 设置一个很大的质量值
            jellyRb.gravityScale = 1; // 确保Supercolabomb受到重力影响，可以根据需要调整
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
        // 获取碰撞Supercolabomb的刚体
        Rigidbody2D otherRb = collision.collider.GetComponent<Rigidbody2D>();
        if (otherRb != null)
        {
            Debug.Log("otherRb获取成功\n");

            // 判断是否是怪物，是的话爆炸，造成伤害和击飞
            //otherRb.AddForce(new Vector2(0, jellyForce), ForceMode2D.Impulse);
            // ...
        }
        else
        {
            Debug.Log("otherRb==null\n");
        }
    }

    public void ChangeHealth(float damage, bool isCarbonicAcid)
    {
        currentHealth += damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
