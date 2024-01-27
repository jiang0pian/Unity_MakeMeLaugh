using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float followSpeed = 0.2f; // 跟随玩家的移动速度，这里设置为 1/5

    private Vector3 previousPlayerPosition; // 上一帧玩家的位置

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player"); // 根据实际情况获取玩家对象

        if (player != null)
        {
            previousPlayerPosition = player.transform.position;
        }
    }

    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player"); // 根据实际情况获取玩家对象

        if (player != null)
        {
            Vector3 currentPlayerPosition = player.transform.position;
            float playerMoveAmount = currentPlayerPosition.x - previousPlayerPosition.x;
            float objectMoveAmount = playerMoveAmount * followSpeed;

            transform.position += new Vector3(objectMoveAmount, 0f, 0f);

            previousPlayerPosition = currentPlayerPosition;
        }
    }
}
