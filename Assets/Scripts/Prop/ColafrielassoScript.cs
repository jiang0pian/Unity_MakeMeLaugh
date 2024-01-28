using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColafrielassoScript : Prop
{
    private int count = 0;
    private int i = 1;
    private Rigidbody2D rb;                     // Colafrielasso的刚体
    private Collider2D coll;                    // Colafrielasso的碰撞器
    private float maxDragDistance = 10.0f;      // 最大持续时间

    public override void UseProp()
    {
        Debug.Log("使用Colafrielasso技能\n");

        // 确保Colafrielasso预制体非空
        if (itemPrefab != null)
        {
            Debug.Log("itemPrefab获取成功\n");
            // 获取玩家当前位置
            Vector2 position = PlayerController.Instance.transform.position;

            // 相对玩家偏移生成位置
            position.y += 3.0f;

            // 在偏移位置实例化Colafrielasso对象
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
            // 确保不受重力影响
            rb.gravityScale = 0;
            // 缩小大小
            rb.transform.localScale = new Vector3(2, 2, 2);
            // 使Colafrielasso碰撞不可用，仅作为触发器
            coll = this.GetComponent<Collider2D>();
            coll.isTrigger = true;
            // 启动一个协程调用，薯条如果飞出一段时间后还未被销毁，则自行销毁
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

    // 道具生效后的效果实现
    private void PropEffect(Collider2D other)
    {
        if (other.gameObject.GetComponent<EnemyController>() != null)
        {
            // 调用怪物的激怒方法
            other.gameObject.GetComponent<EnemyController>().isFindPlayer = false;
        }
    }

    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
