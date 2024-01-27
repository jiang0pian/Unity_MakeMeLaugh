using UnityEngine;

public class JellyProp : Prop
{
    private GameObject jellyPrefab;              // 果冻预制体
    private float distance = 3.0f;              // 果冻放置距离玩家的距离

    // 覆写Prop类中的UseProp方法
    public override void UseProp()
    {
        Debug.Log("使用果冻技能\n");

        // 使用预制体的路径（相对于Resources文件夹）来加载预制体
        jellyPrefab = Resources.Load<GameObject>("Jelly");

        // 确保果冻预制体
        if (jellyPrefab != null)
        {
            Debug.Log("jellyPrefab获取成功\n");
            // 获取玩家当前位置
            Vector2 position = PlayerController.Instance.transform.position;

            // 相对玩家偏移生成位置
            Debug.Log("当前玩家位置：" + position.x + "," + position.y + ")\n");
            position.x += distance * PlayerController.Instance.lookDirection.x;
            position.y -= 1.0f;

            // 在偏移位置实例化果冻对象
            Instantiate(jellyPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.Log("jellyPrefab == null\n");
        }
    }

    void Start()
    {
        // 在Start方法中可以执行初始化操作
    }
}
