using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GumdropProp : Prop
{

    private GameObject GumdropPrefab;               // 橡皮圈预制体
    private float distance = 0.0f;                  // 橡皮圈生成距离玩家的距离
    // 覆写Prop类中的UseProp方法
    public override void UseProp()
    {
        Debug.Log("使用橡皮圈技能");

        // 使用预制体的路径（相对于Resources文件夹）来加载预制体
        GumdropPrefab = Resources.Load<GameObject>("Gamdrop");

        // 确保果冻预制体
        if (GumdropPrefab != null)
        {
            Debug.Log("GumdropPrefab获取成功\n");
            // 获取玩家当前位置
            Vector2 position = PlayerController.Instance.transform.position;

            // 相对玩家偏移生成位置
            position.x += distance * PlayerController.Instance.lookDirection.x;
            //position.y -= 1.0f;

            // 在偏移位置实例化橡皮圈对象
            Instantiate(GumdropPrefab, position, Quaternion.identity);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
