using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GumdropScript : Prop
{
    

    private float distance = 0.0f;              // 橡皮糖圈放置距离玩家的距离
    public Rigidbody2D anchor;                  // 锚点的刚体
    private Rigidbody2D rb;                     // 橡皮糖圈的刚体
    private Collider2D coll;                    // 橡皮糖圈的碰撞器
    private bool isPressed;                     // 鼠标左键是否按下
    private bool isCatapult;                    // 橡皮糖圈是否被发射
    private float maxDragDistance = 5.0f;       // 最大拉伸距离
    private Vector2 mousePos;                   // 鼠标游戏内坐标
    private float coefficient = 10.0f;          // 橡皮糖圈的弹射系数
    public LayerMask layerMask = 8;             // 在Unity编辑器中设置你想检测的Layer
    public float detectionRadius = 5.0f;        // 检测半径


    // 覆写Prop类中的UseProp方法
    public override void UseProp()
    {
        Debug.Log("使用橡皮糖圈技能\n");

        // 确保橡皮糖圈预制体非空
        if (itemPrefab != null)
        {
            Debug.Log("itemPrefab获取成功\n");
            // 获取玩家当前位置
            Vector2 position = PlayerController.Instance.transform.position;

            // 相对玩家偏移生成位置
            Debug.Log("当前玩家位置：" + position.x + "," + position.y + ")\n");
            position.x += distance * PlayerController.Instance.lookDirection.x;
            //position.y -= 1.0f;

            // 在偏移位置实例化橡皮糖圈对象
            Instantiate(itemPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.Log("itemPrefab == null\n");
        }
    }

    private void Start()
    {
        if(this.GetComponent<Rigidbody2D>()!=null) rb = this.GetComponent<Rigidbody2D>();
        coll = this.GetComponent<Collider2D>();
        anchor = PlayerController.Instance.GetComponent<Rigidbody2D>();
        //trailRenderer = this.GetComponent<TrailRenderer>();
        rb.isKinematic = true;                          // 使橡皮糖圈运动状态变为不受外力
        coll.isTrigger = true;                          // 使橡皮糖圈碰撞不可用，仅作为触发器
        Cursor.visible = true;                          // 使光标可用
        isPressed = false;
        isCatapult = false;
    }
    private void Update()
    {
        if(rb!=null)
        {
            // 每帧更新锚点
            anchor = PlayerController.Instance.GetComponent<Rigidbody2D>();
            if (Input.GetMouseButtonDown(0)) // 0 代表鼠标左键
            {
                // 当鼠标左键被按下时，执行一些操作
                Debug.Log("鼠标在游戏内点击");
                isPressed = true;
                if (rb != null)
                {
                    rb.isKinematic = true;  //将球体设为仅运动学，让其不受外力作用，防止鼠标拖动的过程中弹飞
                }
            }
            if (Input.GetMouseButtonUp(0)) // 0 代表鼠标左键
            {
                // 当鼠标左键被释放时，执行一些操作
                Debug.Log("鼠标左键在游戏内被释放");
                isPressed = false;
                if (rb != null)
                {
                    rb.isKinematic = false;
                    //trailRenderer.enabled = true;
                    isCatapult = true;
                    Catapult(rb);
                    Cursor.visible = false;
                }
            }

            if (!isPressed && !isCatapult)
            {
                // 开始瞄准前橡皮圈跟随玩家
                //Debug.Log(gameObject.name+"未开始瞄准\n");
                rb.position = anchor.position;
            }
            else if (isPressed && !isCatapult)
            {
                //Debug.Log(gameObject.name + "开始瞄准\n");
                // 瞄准时始终在锚点范围内
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Vector2.Distance(mousePos, anchor.position) > maxDragDistance)
                {
                    rb.position = (mousePos - anchor.position).normalized * maxDragDistance + anchor.position;
                }
                else
                {
                    rb.position = mousePos;
                }
                drawParabola();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(rb!=null && isCatapult)
        {
            // 当其他非玩家物体进入触发器时，这个方法会被调用
            if (Vector2.Distance(other.gameObject.transform.position,anchor.position)>maxDragDistance)
            {
                Debug.Log("触碰到其他触发器:" + other.gameObject.name + "\n");
                PropEffect(rb);
                Destroy(this.gameObject);
            }
        }
    }


    // 根据鼠标坐标和玩家坐标绘制预测抛物线
    private void drawParabola()
    {
        //GameObject ball = new GameObject("DynamicBall");
        //ball.layer = 9;
        //// 添加 Rigidbody2D 组件
        //Rigidbody2D temprb = ball.AddComponent<Rigidbody2D>();
        //// 设置 Rigidbody2D 属性
        //temprb.gravityScale = 1; // 根据需要调整重力影响
        //// 添加 SpriteRenderer 组件以可视化小球
        //Texture2D texture = new Texture2D(1, 1);
        //texture.SetPixel(0, 0, Color.red);
        //texture.Apply();
        //Sprite newSprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        //SpriteRenderer renderer = ball.AddComponent<SpriteRenderer>();
        //renderer.sprite = newSprite;
        //// 设置小球的初始位置
        //ball.transform.position = rb.position;
        //Catapult(temprb);
    }


    // 发射rb
    private void Catapult(Rigidbody2D rb_)
    {
        rb_.isKinematic = false; // 确保物体不是运动学的
        rb_.gravityScale = 8;    // 确保重力影响开启
        // 根据瞄准向量计算施加的力
        Vector2 force = Mathf.Min(Vector2.Distance(mousePos, anchor.position), maxDragDistance) * coefficient * (anchor.position - mousePos).normalized;
        rb_.AddForce(force,ForceMode2D.Impulse);
    }

    // 道具生效后的效果实现
    private void PropEffect(Rigidbody2D rb_)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(rb_.transform.position, detectionRadius, layerMask);
        foreach (Collider2D collider in colliders)
        {
            GameObject detectedObject = collider.gameObject;
            // 在这里处理检测到的对象，此处应为改变敌人的状态，使得他们向玩家聚集
            // ...
        }
    }
}
