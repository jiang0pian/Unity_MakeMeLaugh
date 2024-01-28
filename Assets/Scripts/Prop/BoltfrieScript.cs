using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoltfrieScript : Prop
{


    private float distance = 0.0f;              // Boltfrie放置距离玩家的距离
    private Rigidbody2D rb;                     // Boltfrie的刚体
    private Collider2D coll;                    // Boltfrie的碰撞器
    private float maxDragDistance = 10.0f;      // 最大持续时间
    private float coefficient = 10.0f;          // Boltfrie的发射力大小
    private float boltfireDamage = 2.0f;        // Boltfrie的伤害大小
    public LayerMask layerMask = 8;             // 在Unity编辑器中设置你想检测的Layer


    // 覆写Prop类中的UseProp方法
    public override void UseProp()
    {
        Debug.Log("使用Boltfrie技能\n");

        // 确保Boltfrie预制体非空
        if (itemPrefab != null)
        {
            Debug.Log("itemPrefab获取成功\n");
            // 获取玩家当前位置
            Vector2 position = PlayerController.Instance.transform.position;

            // 相对玩家偏移生成位置
            Debug.Log("当前玩家位置：" + position.x + "," + position.y + ")\n");
            position.x += distance * PlayerController.Instance.lookDirection.x;
            //position.y -= 1.0f;

            // 在偏移位置实例化Boltfrie对象
            Instantiate(itemPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.Log("itemPrefab == null\n");
        }
    }

    private void Start()
    {
        if(this.GetComponent<Rigidbody2D>()!=null)
        {
            rb = GetComponent<Rigidbody2D>();
            // 确保不受重力影响
            rb.gravityScale = 0;
            // 使薯条碰撞不可用，仅作为触发器
            coll = this.GetComponent<Collider2D>();
            coll.isTrigger = true;
            // 给薯条一个瞬时的朝向人物朝向的力，薯条不受重力影响，薯条不与其他碰撞体碰撞
            rb.AddForce(transform.right * coefficient* PlayerController.Instance.lookDirection.x, ForceMode2D.Impulse);
            // 启动一个协程调用，薯条如果飞出一段时间后还未被销毁，则自行销毁
            StartCoroutine(DestroyAfterTime(maxDragDistance));
        }
    }
    private void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PropEffect(other);
    }

    // 道具生效后的效果实现
    private void PropEffect(Collider2D other)
    {
        // 如果碰到的是怪物，则对怪物造成伤害，但薯条穿过怪物继续飞行。怪物本身含有造成伤害的函数，直接调用。
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
    }
    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
