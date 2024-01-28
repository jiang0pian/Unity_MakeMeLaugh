using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SupercolaboltScript : Prop
{
    private float distance = 2.0f;              // Supercolabolt放置距离玩家的距离
    private Rigidbody2D rb;                     // Supercolabolt的刚体
    private Collider2D coll;                    // Supercolabolt的碰撞器
    private float maxDragDistance = 1.0f;       // 最大持续时间
    private float speed = 50.0f;                // Supercolabolt的持续速度大小
    private float direction;                    // Supercolabolt的发射方向
    public LayerMask layerMask = 8;             // 在Unity编辑器中设置你想检测的Layer


    // 覆写Prop类中的UseProp方法
    public override void UseProp()
    {
        Debug.Log("使用Supercolabolt技能\n");

        // 确保Supercolabolt预制体非空
        if (itemPrefab != null)
        {
            Debug.Log("itemPrefab获取成功\n");
            // 获取玩家当前位置和方向
            Vector2 position = PlayerController.Instance.transform.position;
            direction = PlayerController.Instance.lookDirection.x;
            Debug.Log("Direction=" + direction + "\n");

            // 相对玩家偏移生成位置
            Debug.Log("当前玩家位置：" + position.x + "," + position.y + ")\n");
            position.x += distance * direction;
            //position.y -= 1.0f;

            // 在偏移位置实例化Supercolabolt对象
            Instantiate(itemPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.Log("itemPrefab == null\n");
        }
    }

    private void Start()
    {
        direction = PlayerController.Instance.lookDirection.x;
        if (GetComponent<Rigidbody2D>() != null)
        {
            // 限制旋转和y轴变化
            rb = GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            // 确保不受重力影响，质量无穷大
            rb.gravityScale = 0;
            rb.mass = Mathf.Infinity;
            // 使可乐碰撞可用
            coll = this.GetComponent<Collider2D>();
            coll.isTrigger = false;
            // 给可乐一个固定的速度，可乐不受重力影响
            rb.velocity=new Vector2(speed*direction, 0);
            // 启动一个协程调用，薯条如果飞出一段时间后还未被销毁，则自行销毁
            StartCoroutine(DestroyAfterTime(maxDragDistance));
        }
    }

    private void FixedUpdate()
    {
        // 每帧更新刚体速度以保持固定速度
        rb.velocity = new Vector2(speed*direction, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PropEffect(other);
    }

    // 道具生效后的效果实现
    private void PropEffect(Collider2D other)
    {
        Debug.Log("Supercolabolt触碰到" + other.gameObject.name + "\n");
        // 如果碰到的是怪物，则对怪物造成推动。
        
        // 如果触碰到tilemap，则主动销毁。
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // 调用怪物的受伤害方法
            // 例如：other.GetComponent<Enemy>().TakeDamage(boltfireDamage);
        }
        else if (other.gameObject.layer == 6)
        {
            // 触碰到环境，销毁薯条
            Destroy(gameObject);
        }
        else if(other.gameObject.name=="Player")
        {
            // 触碰到玩家，销毁薯条
            Destroy(gameObject);
        }
    }
    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
