using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SupercolafrieScript : Prop
{


    private float distance = 0.0f;              // Supercolafrie放置距离玩家的距离
    private Rigidbody2D rb;                     // Supercolafrie的刚体
    private Collider2D coll;                    // Supercolafrie的碰撞器
    private float maxDragDistance = 10.0f;      // 最大持续时间
    private float coefficient = 40.0f;          // Supercolafrie的发射力大小
    private float SupercolafrieDamage = 2.0f;   // Supercolafrie的伤害大小
    public LayerMask layerMask = 8;             // 在Unity编辑器中设置你想检测的Layer
    public float spurtForce = 1f;
    public float spurtTime = 1;


    // 覆写Prop类中的UseProp方法
    public override void UseProp()
    {
        Debug.Log("使用Supercolafrie技能\n");
        StartCoroutine(Spurt());
    }

    private void Start()
    {

        if (GetComponent<Rigidbody2D>() != null)
        {
            rb = GetComponent<Rigidbody2D>();
            // 确保不受重力影响
            rb.gravityScale = 0;
            // 使薯条碰撞不可用，仅作为触发器
            coll = this.GetComponent<Collider2D>();
            coll.isTrigger = true;
            // 给薯条一个瞬时的朝向人物朝向的力，薯条不受重力影响，薯条不与其他碰撞体碰撞
            rb.AddForce(-1*transform.right * coefficient * PlayerController.Instance.lookDirection.x, ForceMode2D.Impulse);
            // 旋转薯条朝向飞行方向
            rb.transform.rotation = Quaternion.Euler(0, 90-90* PlayerController.Instance.lookDirection.x, 47);
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
        else if (other.gameObject.layer == LayerMask.NameToLayer("Environment"))
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

    public IEnumerator Spurt()
    {
        PlayerController.Instance.isSpurt = true;
        float oldSpeed = PlayerController.Instance.playerAttribute.moveSpeed;
        for (int i = 0; i < 20 * spurtTime; i++)
        {
            PlayerController.Instance.playerAttribute.moveSpeed *= 1.0f + spurtForce * 0.1f;
            yield return new WaitForSeconds(0.05f);
        }
        PlayerController.Instance.playerAttribute.moveSpeed = oldSpeed;
        PlayerController.Instance.isSpurt = false;
        // 确保Supercolafrie预制体非空
        if (itemPrefab != null)
        {
            Debug.Log("itemPrefab获取成功\n");
            // 获取玩家当前位置
            Vector2 position = PlayerController.Instance.transform.position;

            // 相对玩家偏移生成位置
            Debug.Log("当前玩家位置：" + position.x + "," + position.y + ")\n");
            position.x += distance * PlayerController.Instance.lookDirection.x;
            //position.y -= 1.0f;

            // 在偏移位置实例化Supercolafrie对象
            Instantiate(itemPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.Log("itemPrefab == null\n");
        }
    }
}
